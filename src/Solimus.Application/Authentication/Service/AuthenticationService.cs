using System.IdentityModel.Tokens.Jwt;
using Solimus.Application.Authentication.DTO_s;
using Solimus.Application.Common;
using Solimus.Application.Common.ServiceErrors;
using Solimus.Application.JWT;
using Solimus.Domain.Entities;
using Solimus.Domain.Interfaces;

namespace Solimus.Application.Authentication.Service;

public class AuthenticationService(IUnitOfWork unitOfWork, IJwtService jwtService) : IAuthenticationService
{
    public async Task<Result<LoginResponse>> Login(LoginRequest request, CancellationToken cancellationToken = default)
    {
        var user = await unitOfWork.Users.GetByEmail(request.Email, cancellationToken);
        if (user is null)
            return AuthenticationErrors.InvalidCredentials;

        if(user.LockOutTime.HasValue && user.LockOutTime > DateTime.UtcNow)
            return AuthenticationErrors.UserLockoutBan(user.LockOutTime.Value);

        if (user.LockOutTime < DateTime.UtcNow)
        {
            user.LockOutTime = null;
            unitOfWork.Users.Update(user);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
        
        if (user.IncorrectPasswordCount == 5)
        {
            user.LockOutTime = DateTime.UtcNow.AddMinutes(10);
            user.IncorrectPasswordCount = 0;
            unitOfWork.Users.Update(user);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return AuthenticationErrors.UserLockoutBan(user.LockOutTime.Value);
        }
        
        var isPasswordCorrect = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);
        
        if (!isPasswordCorrect && user.IncorrectPasswordCount < 5)
        {
            user.IncorrectPasswordCount++;
            unitOfWork.Users.Update(user);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return AuthenticationErrors.InvalidCredentialsWithIncorrectPassword(6 - user.IncorrectPasswordCount);
        }
        
        var accessToken = jwtService.GenerateJwtToken(user);
        var refreshToken = jwtService.GenerateRefreshToken();
        
        var refreshTokenEntity = await unitOfWork.RefreshTokens.GetRefreshTokenByUserId(user.UserId, cancellationToken);
        if (refreshTokenEntity is null)
        {
            var refreshTokenEntityToAdd = new RefreshToken
            {
                Token = refreshToken,
                User = user
            };
            await unitOfWork.RefreshTokens.AddAsync(refreshTokenEntityToAdd, cancellationToken);            
        }
        else
        {
            refreshTokenEntity.Token = refreshToken;
            refreshTokenEntity.User = user;
            unitOfWork.RefreshTokens.Update(refreshTokenEntity);    
        }
        
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<LoginResponse>.Success(new LoginResponse(accessToken, refreshToken));
    }

    public async Task<Result<LoginResponse>> Register(RegistrationRequest request, CancellationToken cancellationToken = default)
    {
        var userExists = await unitOfWork.Users.GetByEmail(request.Email, cancellationToken);
        if (userExists is not null)
            return AuthenticationErrors.AlreadyRegistered;

        var newUserToAdd = new User
        {
            Email = request.Email,
            UserName = request.UserName,
            FirstName = request.FirstName,
            LastName = request.LastName,
            JoinedDate = DateTime.UtcNow,
            UpdateTime = DateTime.UtcNow,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password)
        };

        await unitOfWork.Users.AddAsync(newUserToAdd, cancellationToken);
        var userRole = await unitOfWork.Roles.GetRoleByName("User", cancellationToken);
        var userRoleEntityToAdd = new UserRole
        {
            RoleId = userRole.RoleId,
            UserId = newUserToAdd.UserId
        };
        await unitOfWork.UserRoles.AddAsync(userRoleEntityToAdd, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        var newUserToAccess = await unitOfWork.Users.GetByEmail(newUserToAdd.Email, cancellationToken);
        if (newUserToAccess is null)
            return AuthenticationErrors.InvalidCredentials;
        
        var accessToken = jwtService.GenerateJwtToken(newUserToAccess);
        var refreshToken = jwtService.GenerateRefreshToken();
        
        var refreshTokenEntityToAdd = new RefreshToken
        {
            Token = refreshToken,
            User = newUserToAdd
        };
        await unitOfWork.RefreshTokens.AddAsync(refreshTokenEntityToAdd, cancellationToken); 
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        var result = new LoginResponse(accessToken, refreshToken);
        return Result<LoginResponse>.Success(result);
    }

    public async Task<Result<LoginResponse>> RefreshToken(RefreshTokenRequest request, CancellationToken cancellationToken = default)
    {
        var principal = jwtService.GetPrincipalFromExpiredToken(request.AccessToken);
        if (principal is null)
            return AuthenticationErrors.InvalidToken;

        var userIdClaim = principal.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
        if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            return AuthenticationErrors.InvalidToken;

        var user = await unitOfWork.Users.GetByIdAsync(userId, cancellationToken);
        if(user is null)
            return AuthenticationErrors.InvalidToken;
        
        var refreshToken = await unitOfWork.RefreshTokens.GetRefreshTokenByUserId(user.UserId, cancellationToken);
        if(refreshToken is null || refreshToken.Token != request.RefreshToken)
            return AuthenticationErrors.InvalidToken;

        var newAccessToken = jwtService.GenerateJwtToken(user);
        var newRefreshToken = jwtService.GenerateRefreshToken();

        refreshToken.Token = newRefreshToken;
        refreshToken.User = user;
        unitOfWork.RefreshTokens.Update(refreshToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result<LoginResponse>.Success(new LoginResponse(newAccessToken, newRefreshToken));
    }

    public async Task<Result<string>> Logout(Guid requestUserId, Guid userId, CancellationToken cancellationToken = default)
    {
        if (requestUserId != userId)
            return AuthenticationErrors.InvalidCredentials;
        
        var user = await unitOfWork.Users.GetByIdAsync(userId, cancellationToken);
        if (user is null)
            return AuthenticationErrors.UserNotFound;

        var refreshTokenEntity = await unitOfWork.RefreshTokens.GetRefreshTokenByUserId(user.UserId, cancellationToken);
        if (refreshTokenEntity is null)
            return AuthenticationErrors.UserAlreadyLoggedOut;
        
        refreshTokenEntity.Token = string.Empty;
        unitOfWork.RefreshTokens.Update(refreshTokenEntity);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<string>.Success("Успешно вышли из своей уч. записи.");
    }
}
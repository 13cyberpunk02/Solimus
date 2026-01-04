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

        var isPasswordCorrect = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);
        if (!isPasswordCorrect)
            return AuthenticationErrors.InvalidCredentials;
        
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

        refreshTokenEntity.Token = refreshToken;
        refreshTokenEntity.User = user;
        unitOfWork.RefreshTokens.Update(refreshTokenEntity);
        
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
        
        var accessToken = jwtService.GenerateJwtToken(newUserToAdd);
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
}
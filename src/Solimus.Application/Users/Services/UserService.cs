using Solimus.Application.Common;
using Solimus.Application.Common.ServiceErrors;
using Solimus.Application.Users.DTO_s;
using Solimus.Domain.Entities;
using Solimus.Domain.Interfaces;

namespace Solimus.Application.Users.Services;

public class UserService(IUnitOfWork unitOfWork) : IUserService
{
    public async Task<Result<GetUserResponse>> GetUserByEmail(string email, CancellationToken ct = default)
    {
        var user = await unitOfWork.Users.GetByEmail(email, ct);
        if(user is null)
            return UserErrors.NotFound;

        return Result<GetUserResponse>.Success(new GetUserResponse(
            UserId: user.UserId,
            UserName: user.UserName,
            Email: user.Email,
            FirstName: user.FirstName,
            LastName: user.LastName,
            RoleNames: user.UserRoles.Select(x => x.Role.RoleName).ToList(),
            JoinedDate: user.JoinedDate,
            LastUpdate: user.UpdateTime));
    }

    public async Task<Result<GetUserResponse>> GetUserById(Guid id, CancellationToken ct = default)
    {
        var user = await unitOfWork.Users.GetByIdAsync(id, ct);
        if(user is null)
            return UserErrors.NotFound;
        
        return Result<GetUserResponse>.Success(new GetUserResponse(
            UserId: user.UserId,
            UserName: user.UserName,
            Email: user.Email,
            FirstName: user.FirstName,
            LastName: user.LastName,
            RoleNames: user.UserRoles.Select(x => x.Role.RoleName).ToList(),
            JoinedDate: user.JoinedDate,
            LastUpdate: user.UpdateTime));
    }

    public async Task<Result<List<GetUserResponse>>> GetAllUsers(CancellationToken ct = default)
    {
        var users = await unitOfWork.Users.GetAllUsersWithRoles(ct);
        if(users is null)
            return UserErrors.NotFound;
        
        var response = users.Select(user => new GetUserResponse(
            UserId: user.UserId,
            UserName: user.UserName,
            Email: user.Email,
            FirstName: user.FirstName,
            LastName: user.LastName,
            RoleNames: user.UserRoles.Select(x => x.Role.RoleName).ToList(),
            JoinedDate: user.JoinedDate,
            LastUpdate: user.UpdateTime)).ToList();
        
        return Result<List<GetUserResponse>>.Success(response);
    }

    public async Task<Result<string>> CreateUser(CreateUserRequest request, CancellationToken cancellationToken = default)
    {
        var user = await unitOfWork.Users.GetByEmail(request.Email, cancellationToken);
        if(user is not null)
            return UserErrors.EmailIsReserved(request.Email);

        var newUser = new User
        {
            UserName = request.UserName,
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            JoinedDate = DateTime.UtcNow,
            UpdateTime = DateTime.UtcNow,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password)
        };
        
        var userRole = await unitOfWork.Roles.GetRoleByName("User", cancellationToken);
        var userRoleEntityToAdd = new UserRole
        {
            RoleId = userRole.RoleId,
            UserId = newUser.UserId
        };
        await unitOfWork.Users.AddAsync(newUser, cancellationToken);
        await unitOfWork.UserRoles.AddAsync(userRoleEntityToAdd, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<string>.Success("Новый пользователь успешно добавлен.");
    }

    public async Task<Result<string>> UpdateUser(UpdateUserRequest request, Guid userId, CancellationToken cancellationToken = default)
    {
        var user = await unitOfWork.Users.GetByEmail(request.Email, cancellationToken);
        if (user is null || user.UserId != userId)
            return UserErrors.NotFound;

        user.Email = request.Email;
        user.UserName = request.UserName;
        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.UpdateTime = DateTime.UtcNow;
        
        unitOfWork.Users.Update(user);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<string>.Success("Информация о пользователе успешно обновлена");
    }

    public async Task<Result<string>> ChangePassword(ChangeUserPasswordRequest request, Guid userId, CancellationToken cancellationToken = default)
    {
        var user = await unitOfWork.Users.GetByIdAsync(userId, cancellationToken);
        if (user is null)
            return UserErrors.NotFound;
        
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
        user.UpdateTime = DateTime.UtcNow;
        unitOfWork.Users.Update(user);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<string>.Success("Пароль успешно обновлен.");
    }

    public async Task<Result<string>> BanUser(BanUserRequest request, Guid userId, CancellationToken cancellationToken = default)
    {
        var user = await unitOfWork.Users.GetByIdAsync(userId, cancellationToken);
        if (user is null)
            return UserErrors.NotFound;
        
        user.LockOutTime = request.BanUntilDate;
        unitOfWork.Users.Update(user);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<string>.Success("Пользователь забанен.");
    }

    public async Task<Result<string>> UnBanUser(Guid userId, CancellationToken cancellationToken = default)
    {
        var user = await unitOfWork.Users.GetByIdAsync(userId, cancellationToken);
        if (user is null)
            return UserErrors.NotFound;

        user.LockOutTime = null;
        unitOfWork.Users.Update(user);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<string>.Success("Пользователь разблокирован.");
    }

    public async Task<Result<string>> AssignOrRemoveRole(Guid userId, AssignOrRemoveRoleRequest request, CancellationToken cancellationToken = default)
    {
        var user = await unitOfWork.Users.GetByIdAsync(userId, cancellationToken);
        if (user is null)
            return UserErrors.NotFound;

        var currentUserRoles = await unitOfWork.UserRoles.GetUserRolesByUserId(user.UserId, cancellationToken);
        var currentRoleIds = currentUserRoles.Select(x => x.RoleId).ToList();
        var targetUserRoleIds = request.RoleIds.ToHashSet();

        var rolesToAdd = targetUserRoleIds.Except(currentRoleIds).ToList();
        var rolesToRemove = currentUserRoles
            .Where(ur => !targetUserRoleIds.Contains(ur.RoleId))
            .ToList();

        if (rolesToRemove.Count > 0)
        {
            foreach (var role in rolesToRemove)
                unitOfWork.UserRoles.Remove(role);
        }

        if (rolesToAdd.Count > 0)
        {
            foreach (var roleId in rolesToAdd)
            {
                await unitOfWork.UserRoles.AddAsync(new UserRole
                {
                    RoleId = roleId,
                    UserId = user.UserId
                }, cancellationToken);
            }
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result<string>.Success("Роли успешно назначены.");
    }
}
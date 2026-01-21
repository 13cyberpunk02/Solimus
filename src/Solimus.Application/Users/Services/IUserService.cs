using Solimus.Application.Common;
using Solimus.Application.Users.DTO_s;

namespace Solimus.Application.Users.Services;

public interface IUserService
{
    Task<Result<GetUserResponse>> GetUserByEmail(string email, CancellationToken ct = default);
    Task<Result<GetUserResponse>> GetUserById(Guid id, CancellationToken ct = default);
    Task<Result<List<GetUserResponse>>> GetAllUsers(CancellationToken ct = default);
    Task<Result<string>> CreateUser(CreateUserRequest request, CancellationToken cancellationToken = default);
    Task<Result<string>> UpdateUser(UpdateUserRequest request, Guid userId, CancellationToken cancellationToken = default);
    Task<Result<string>> ChangePassword(ChangeUserPasswordRequest request, Guid userId, CancellationToken cancellationToken = default);
    Task<Result<string>> BanUser(BanUserRequest request, Guid userId, CancellationToken cancellationToken = default);
    Task<Result<string>> UnBanUser(Guid userId, CancellationToken cancellationToken = default);
    Task<Result<string>> AssignOrRemoveRole(Guid userId, AssignOrRemoveRoleRequest request, CancellationToken cancellationToken = default);
}
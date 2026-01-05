using Solimus.Application.Authentication.DTO_s;
using Solimus.Application.Common;

namespace Solimus.Application.Authentication.Service;

public interface IAuthenticationService
{
    Task<Result<LoginResponse>> Login(LoginRequest request, CancellationToken cancellationToken = default);
    Task<Result<LoginResponse>> Register(RegistrationRequest request, CancellationToken cancellationToken = default);
    Task<Result<LoginResponse>> RefreshToken(RefreshTokenRequest request, CancellationToken cancellationToken = default);
    Task<Result<string>> Logout(Guid requestUserId, Guid userId, CancellationToken cancellationToken = default);
}
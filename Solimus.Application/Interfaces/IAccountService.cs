using Solimus.Application.Common.Results;
using Solimus.Application.Models.Request.User;

namespace Solimus.Application.Interfaces;

public interface IAccountService
{
    Result GetAllUsers();
    Task<Result> GetUserById(string id);        
    Task<Result> GetUserByEmailAsync(string email);
    Task<Result> DeleteUser(string id);
    Task<Result> UpdateUser(UserUpdateRequest user);
}

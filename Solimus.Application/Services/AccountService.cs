using Microsoft.AspNetCore.Identity;
using Solimus.Application.Common.Results;
using Solimus.Application.DTO_s.User;
using Solimus.Application.Error.AccountErrors;
using Solimus.Application.Interfaces;
using Solimus.Application.Models.Request.User.Request;
using Solimus.Application.Validators.User;
using Solimus.Domain.Entities;

namespace Solimus.Application.Services;

public class AccountService(
    UserManager<SolimusUser> userManager,
    UpdateUserRequestValidator updateUserValidator) : IAccountService
{
    public async Task<Result> DeleteUser(string id)
    {
        var userToDelete = await userManager.FindByIdAsync(id);
        if (userToDelete is null)
            return Result.Failure(AccountErrors.UserNotFound);        
        var result = await userManager.DeleteAsync(userToDelete);

        if (!result.Succeeded)
            return Result.Failure(AccountErrors.ListOfErrors(result.Errors.Select(x => x.Description)));

        return Result.Success($"Пользователь {userToDelete.UserName} успешно удален");        
    }

    public Result GetAllUsers()
    {
        var users = userManager.Users;

        if(users is null)
            return Result.Failure(AccountErrors.UserNotFound);

        var reponse = users.Select(x => new UserResponseDto
        {
            Firstname = x.Firstname,
            Lastname = x.Lastname,
            UserName = x.UserName!,
            Birthday = x.Birthday,
            Email = x.Email!,
            PhoneNumber = x.PhoneNumber!,
            JoinedDate = x.JoinedDate            
        }).ToList();

        return Result.Success(reponse);
    }

    public async Task<Result> GetCurrentUserDetails(string userId)
    {
        var user = await userManager.FindByIdAsync(userId);
        if (user is null)
            return Result.Failure(AccountErrors.UserNotFound);

        var response = new UserResponseDto
        {
            Firstname = user.Firstname,
            Lastname = user.Lastname,
            UserName = user.UserName!,
            Birthday = user.Birthday,
            Email = user.Email!,
            PhoneNumber = user.PhoneNumber!,
            JoinedDate = user.JoinedDate,
            Roles = (userManager.GetRolesAsync(user).GetAwaiter().GetResult()).ToList()
        };
        return Result.Success(response);
    }

    public async Task<Result> GetUserByEmailAsync(string email)
    {
        if (string.IsNullOrEmpty(email))
            return Result.Failure(AccountErrors.UserNotFound);

        var user = await userManager.FindByIdAsync(email);
        return Result.Success(user);
    }

    public async Task<Result> GetUserById(string id)
    {
        var user = await userManager.FindByIdAsync(id);

        if (user is null)
            return Result.Failure(AccountErrors.UserNotFound);

        return Result.Success(new 
        {
            Firtsname = user.Firstname,
            Lastname = user.Lastname,
            UserName = user.UserName!,
            Birthday = user.Birthday,
            Email = user.Email!,
            PhoneNumber = user.PhoneNumber!,
            JoinedDate = user.JoinedDate,
            Roles = (userManager.GetRolesAsync(user).GetAwaiter().GetResult()).ToList()
        });
    }

    public async Task<Result> UpdateUser(UserUpdateRequest userToUpdate)
    {
        var validModel = await updateUserValidator.ValidateAsync(userToUpdate);
        if (!validModel.IsValid)
            return Result.Failure(AccountErrors.ListOfErrors(validModel.Errors.Select(error => error.ErrorMessage)));

        var user = await userManager.FindByIdAsync(userToUpdate.Id);

        if (user is null)
            return Result.Failure(AccountErrors.UserNotFound);

        user.PhoneNumber = userToUpdate.PhoneNumber;
        user.Address = userToUpdate.Address;
        user.Firstname = userToUpdate.Firstname;
        user.Lastname = userToUpdate.Lastname;
        user.Birthday = userToUpdate.Birthday;
        var result = await userManager.UpdateAsync(user);

        if (!result.Succeeded)
            return Result.Failure(AccountErrors.ListOfErrors(result.Errors.Select(x => x.Description)));

        return Result.Success("Информация обновлена о пользователе обновлена успешно.");
    }
}

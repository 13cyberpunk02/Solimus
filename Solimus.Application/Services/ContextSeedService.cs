using Microsoft.AspNetCore.Identity;
using Solimus.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Solimus.Application.Services;

public class ContextSeedService(UserManager<SolimusUser> userManager, 
    RoleManager<IdentityRole> roleManager)
{
    private readonly IdentityRole _defaultRole = new IdentityRole
    {
        Name = "Администратор",
    };
    private readonly SolimusUser _defaultUser = new SolimusUser
    {
        UserName = "admin",
        Email = "admin@domain.ru",
        EmailConfirmed = true,
    };
    private readonly string _defaultPassword = "P@ssword1";

    public async Task InitializeContextAsync()
    {
        if (!roleManager.Roles.Any())
            await roleManager.CreateAsync(_defaultRole);

        if(!userManager.Users.Any())
        {
            var userCreated = await userManager.CreateAsync(_defaultUser, _defaultPassword);           
            var userAssignedToRole = await userManager.AddToRoleAsync(_defaultUser, _defaultRole.Name);
            await userManager.AddClaimsAsync(_defaultUser, new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Email, _defaultUser.Email),
                new Claim(JwtRegisteredClaimNames.NameId, _defaultUser.UserName)
            });
        }
    }
}

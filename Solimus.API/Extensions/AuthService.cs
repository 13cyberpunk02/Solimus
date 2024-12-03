using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Solimus.Application.Models.Options;
using Solimus.Domain.Entities;
using Solimus.Infrastructure.Persistence.Context;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text;

namespace Solimus.API.Extensions;

public static class AuthService
{
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "Solimus TV API", Version = "v1" });
            options.SwaggerGeneratorOptions.DocumentFilters.Add(new LowerCaseDocumentFilter());
            
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer"
            });

            options.AddSecurityRequirement(
                new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        []
                    }
                });
        });

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(options => 
            {
                var jwtConfig = new JwtConfig(configuration);
                var key = Encoding.UTF8.GetBytes(jwtConfig.Key);
                options.SaveToken = true;
                options.TokenValidationParameters = new()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtConfig.Issuer,
                    ValidAudience = jwtConfig.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ClockSkew = TimeSpan.Zero
                };

            });

        services.AddIdentityCore<SolimusUser>(options =>
        {
            options.Password.RequiredLength = 6;
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.SignIn.RequireConfirmedEmail = true;
        })
            .AddRoles<IdentityRole>()
            .AddRoleManager<RoleManager<IdentityRole>>()
            .AddEntityFrameworkStores<SolimusContext>()
            .AddSignInManager<SignInManager<SolimusUser>>()
            .AddUserManager<UserManager<SolimusUser>>()
            .AddDefaultTokenProviders();
        return services;
    }
}
public class LowerCaseDocumentFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {        
        var paths = swaggerDoc.Paths.ToDictionary(
            path => path.Key.ToLowerInvariant(),
            path => swaggerDoc.Paths[path.Key]);
 
        swaggerDoc.Paths = new OpenApiPaths();
        foreach (var pathItem in paths)
        {
            swaggerDoc.Paths.Add(pathItem.Key, pathItem.Value);
        }
    }
}

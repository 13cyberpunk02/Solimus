using FluentValidation;
using Solimus.Application.Users.DTO_s;

namespace Solimus.Application.Users.Validators;

public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
{
    public UpdateUserRequestValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("Имя пользователя обязательно")
            .MinimumLength(3).WithMessage("Имя пользователя должно содержать не менее 3 символов")
            .MaximumLength(50).WithMessage("Имя пользователя не должно превышать 50 символов")
            .Matches("^[a-zA-Z0-9_]*$").WithMessage("Имя пользователя может содержать только буквы, цифры и символ подчёркивания");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Электронная почта обязательна")
            .NotNull().WithMessage("Электронная почта обязательна")
            .EmailAddress().WithMessage("Неверный формат электронной почты");
        
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("Имя обязательно")
            .MinimumLength(3).WithMessage("Имя должно содержать не менее 3 букв")
            .MaximumLength(50).WithMessage("Имя не должно превышать 50 букв");
        
        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Фамилия обязательна")
            .MinimumLength(3).WithMessage("Фамилия должна содержать не менее 3 букв")
            .MaximumLength(50).WithMessage("Фамилия не должна превышать 50 букв");
    }
}
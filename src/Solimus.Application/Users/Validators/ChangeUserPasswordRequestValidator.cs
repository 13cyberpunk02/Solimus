using FluentValidation;
using Solimus.Application.Users.DTO_s;

namespace Solimus.Application.Users.Validators;

public class ChangeUserPasswordRequestValidator : AbstractValidator<ChangeUserPasswordRequest>
{
    public ChangeUserPasswordRequestValidator()
    {
        RuleFor(x => x.NewPassword)
            .NotEmpty().WithMessage("Пароль обязателен")
            .MinimumLength(6).WithMessage("Пароль должен содержать минимум 6 символов")
            .MaximumLength(100).WithMessage("Пароль слишком длинный")
            .Matches(@"[A-Z]").WithMessage("Пароль должен содержать хотя бы одну заглавную букву")
            .Matches(@"[a-z]").WithMessage("Пароль должен содержать хотя бы одну строчную букву")
            .Matches(@"[0-9]").WithMessage("Пароль должен содержать хотя бы одну цифру")
            .Matches(@"[!@#$%^&*(),.?"":{}|<>]").WithMessage("Пароль должен содержать хотя бы один специальный символ");

        RuleFor(x => x.ConfirmNewPassword)
            .NotEmpty().WithMessage("Пароль подтверждения обязателен")
            .Equal(x => x.NewPassword).WithMessage("Пароли не совпадают");
    }
}
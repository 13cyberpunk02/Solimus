using FluentValidation;
using Solimus.Application.Authentication.DTO_s;

namespace Solimus.Application.Authentication.Validators;

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(loginRequest => loginRequest.Email)
            .NotNull().WithMessage("Эл. почта обязательна к заполнению")
            .NotEmpty().WithMessage("Эл. почта обязательна к заполнению")
            .EmailAddress().WithMessage("Эл. почта неправильно заполнена");
        
        RuleFor(LoginRequest => LoginRequest.Password)
            .NotEmpty().WithMessage("Пароль обязателен")
            .MinimumLength(6).WithMessage("Пароль должен содержать минимум 6 символов")
            .MaximumLength(100).WithMessage("Пароль слишком длинный")
            .Matches(@"[A-Z]").WithMessage("Пароль должен содержать хотя бы одну заглавную букву")
            .Matches(@"[a-z]").WithMessage("Пароль должен содержать хотя бы одну строчную букву")
            .Matches(@"[0-9]").WithMessage("Пароль должен содержать хотя бы одну цифру")
            .Matches(@"[!@#$%^&*(),.?"":{}|<>]").WithMessage("Пароль должен содержать хотя бы один специальный символ");
    }
}
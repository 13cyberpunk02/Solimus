using FluentValidation;
using Solimus.Application.Models.Request.Authentication;

namespace Solimus.Application.Validators.Authentication;

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Эл. почта обязательно к заполнению.")
            .EmailAddress().WithMessage("Эл. почта заполнена неправильно.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Пароль обязателен к заполнению.")
            .MinimumLength(6).WithMessage("Пароль должен состоять минимально из от 6 символов.")
            .MaximumLength(30).WithMessage("Максимальное количество символов в пароле 30.");
    }
}

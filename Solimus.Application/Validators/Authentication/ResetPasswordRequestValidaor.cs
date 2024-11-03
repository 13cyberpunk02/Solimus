using FluentValidation;
using Solimus.Application.Models.Request.Authentication;

namespace Solimus.Application.Validators.Authentication;

public class ResetPasswordRequestValidaor : AbstractValidator<ResetPasswordRequest>
{
    public ResetPasswordRequestValidaor()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Эл. почта обязательно к заполнению.")
            .EmailAddress().WithMessage("Эл. почта заполнена неправильно.");
        RuleFor(x => x.Token)
            .NotEmpty().WithMessage("Токен пустой.")
            .NotNull().WithMessage("Токен пустой.");
        RuleFor(x => x.NewPassword)
            .NotEmpty().WithMessage("Пароль обязателен к заполнению.")
            .MinimumLength(6).WithMessage("Пароль должен состоять минимум от 6 символов.")
            .MaximumLength(30).WithMessage("Максимальное количество символов в пароле 30.");
    }
}

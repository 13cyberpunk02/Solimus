using FluentValidation;
using Solimus.Application.Models.Request.Authentication;

namespace Solimus.Application.Validators.Authentication;

public class RegistrationRequestValidator : AbstractValidator<RegistrationRequest>
{
    public RegistrationRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Эл. почта обязательно к заполнению.")
            .EmailAddress().WithMessage("Эл. почта заполнена неправильно.");

        RuleFor(p => p.Password).NotEmpty().WithMessage("Пароль обязателен к заполнению.")
            .MinimumLength(6).WithMessage("Пароль должен состоять минимум от 6 символов.")
            .MaximumLength(30).WithMessage("Максимальное количество символов в пароле 30.")
            .Matches(@"[A-Z]+").WithMessage("Пароль должен содержать хотя бы одну большую букву.")
            .Matches(@"[a-z]+").WithMessage("Пароль должен содержать хотя бы одну маленькую букву.")
            .Matches(@"[0-9]+").WithMessage("Пароль должен содержать хотя бы одну цифру.")
            .Matches(@"[\@\!\?\*\.]+").WithMessage("Пароль должен содержать хотя бы одну из этих символов: [@, !, ?, *].");
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Имя пользователя обязательно нужно заполнить.")
            .MinimumLength(3).WithMessage("Имя пользователя должен состоять минимум от 3 символов.")
            .MaximumLength(30).WithMessage("Максимальное количество символов в имени пользователя должно быть 30.");
    }
}

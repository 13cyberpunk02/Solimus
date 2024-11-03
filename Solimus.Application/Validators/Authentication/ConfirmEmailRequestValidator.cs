using FluentValidation;
using Solimus.Application.Models.Request.Authentication;

namespace Solimus.Application.Validators.Authentication;

public class ConfirmEmailRequestValidator : AbstractValidator<ConfirmEmailRequest>
{
    public ConfirmEmailRequestValidator()
    {
        RuleFor(x => x.Token)
            .NotEmpty().WithMessage("Токен пустой.")
            .NotNull().WithMessage("Токен пустой.");
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Эл. почта обязательно к заполнению.")
            .EmailAddress().WithMessage("Эл. почта заполнена неправильно.");
    }
}

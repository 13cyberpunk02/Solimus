using FluentValidation;
using Solimus.Application.Models.Request.Authentication;

namespace Solimus.Application.Validators.Authentication;

public class RefreshTokenRequestValidator :AbstractValidator<RefreshTokenRequest>
{
    public RefreshTokenRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Эл. почта обязательно к заполнению.")
            .EmailAddress().WithMessage("Эл. почта заполнена неправильно.");

        RuleFor(x => x.Token)
            .NotEmpty().WithMessage("Токен доступа не может быть пустым");

        RuleFor(x => x.RefreshToken)
            .NotEmpty().WithMessage("Токен обновления токена доступа не может быть пустым");
    }    
}

using FluentValidation;
using Solimus.Application.Users.DTO_s;

namespace Solimus.Application.Users.Validators;

public class BanUserRequestValidator : AbstractValidator<BanUserRequest>
{
    public BanUserRequestValidator()
    {
        RuleFor(request => request.BanUntilDate)
            .NotEmpty().WithMessage("Дата блокировки обязательна к заполнению")
            .NotNull().WithMessage("Дата блокировки обязательна к заполнению")
            .Must(x => !x.Equals(default)).WithMessage("Нужно указать правильно дату");
    }
}
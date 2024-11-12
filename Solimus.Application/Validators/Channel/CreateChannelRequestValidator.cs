using FluentValidation;
using Solimus.Application.Models.Request.Channel;

namespace Solimus.Application.Validators.Channel;

public class CreateChannelRequestValidator : AbstractValidator<CreateChannelRequest>
{
    public CreateChannelRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("")
            .NotNull().WithMessage("")
            .MaximumLength(30).WithMessage("")
            .MinimumLength(1).WithMessage("");
    }
}

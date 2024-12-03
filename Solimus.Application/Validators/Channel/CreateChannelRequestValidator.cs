using FluentValidation;
using Solimus.Application.Models.Request.Channel;

namespace Solimus.Application.Validators.Channel;

public class CreateChannelRequestValidator : AbstractValidator<CreateChannelRequest>
{
    public CreateChannelRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Имя канала обязательна для заполнения")
            .NotNull().WithMessage("Имя канала не может быть пустым")
            .MaximumLength(30).WithMessage("Имя канала может содержать не более 30 символов")
            .MinimumLength(1).WithMessage("Имя канала может содержать не менее 1 символа");
    }
}

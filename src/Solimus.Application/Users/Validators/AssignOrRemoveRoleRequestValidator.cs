using FluentValidation;
using Solimus.Application.Users.DTO_s;

namespace Solimus.Application.Users.Validators;

public class AssignOrRemoveRoleRequestValidator : AbstractValidator<AssignOrRemoveRoleRequest>
{
    public AssignOrRemoveRoleRequestValidator()
    {
        RuleFor(x => x.RoleIds)
            .NotNull().WithMessage("Id роля обязателен к заполнению")
            .NotEmpty().WithMessage("Id роля обязателен к заполнению");
    }
}
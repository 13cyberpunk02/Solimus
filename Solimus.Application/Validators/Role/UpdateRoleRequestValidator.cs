using FluentValidation;
using Solimus.Application.Models.Request.Role;

namespace Solimus.Application.Validators.Role;

public class UpdateRoleRequestValidator : AbstractValidator<UpdateRoleRequest>
{
    public UpdateRoleRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotNull().WithMessage("Id роли не может быть пустым");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Имя роли не может быть пустым")
            .NotNull().WithMessage("Имя роли не заполнено")
            .MinimumLength(3).WithMessage("Минимальное название роли может быть от 3 букв")
            .MaximumLength(30).WithMessage("Максимальное название роли может содержать 30 букв");
    }
}

using FluentValidation;
using Solimus.Application.Models.Request.User.Request;

namespace Solimus.Application.Validators.User;

public class UpdateUserRequestValidator : AbstractValidator<UserUpdateRequest>
{
    public UpdateUserRequestValidator()
    {
        RuleFor(user => user.Firstname)
            .NotEmpty().WithMessage("Имя обязательно к заполнению.")
            .MinimumLength(3).WithMessage("Имя должно содержать минимально 3 буквы.")
            .MaximumLength(15).WithMessage("Имя должно содержать максимально 15 букв.");
        RuleFor(user => user.Lastname)
            .NotEmpty().WithMessage("Фамилия обязательно к заполнению.")
            .MinimumLength(3).WithMessage("Фамилия должно содержать минимально 3 буквы.")
            .MaximumLength(15).WithMessage("Фамилия должно содержать максимально 15 букв.");
        RuleFor(user => user.Birthday)
            .NotEmpty().WithMessage("День рождения не может быть пустым.");
        RuleFor(user => user.PhoneNumber)
            .NotEmpty().WithMessage("Номер телефона обязательно нужно заполнить.")
            .NotNull().WithMessage("Номер телефона не может быть пустым.");
        RuleFor(user => user.Address)
            .NotEmpty().WithMessage("Адрес обязателен к заполнению");
    }
}

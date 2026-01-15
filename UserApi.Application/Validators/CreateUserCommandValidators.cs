using FluentValidation;
using UserApi.Application.Commands.CreateUser;

namespace UserApi.Application.Validators;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("The name is mandatory")
            .MaximumLength(150);

        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("The phone is mandatory")
            .MaximumLength(20);

        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("The address is mandatory")
            .MaximumLength(200);

        RuleFor(x => x.CityId)
            .GreaterThan(0).WithMessage("Municipality is mandatory");
    }
}

using FluentValidation;

namespace WarehouseManagement.Application.Units.Commands;

public class CreateUnitValidator : AbstractValidator<CreateUnitCommand>
{
    public CreateUnitValidator()
    {
        RuleFor(x => x.Name).NotNull().NotEmpty();
    }
}
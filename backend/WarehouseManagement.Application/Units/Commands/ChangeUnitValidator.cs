using FluentValidation;

namespace WarehouseManagement.Application.Units.Commands;

public class ChangeUnitValidator : AbstractValidator<ChangeUnitCommand>
{
    public ChangeUnitValidator()
    {   
        RuleFor(x => x.Name).NotNull().NotEmpty().MaximumLength(50);
    }
}
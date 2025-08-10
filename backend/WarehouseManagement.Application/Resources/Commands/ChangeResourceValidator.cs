using FluentValidation;

namespace WarehouseManagement.Application.Resources.Commands;

public class ChangeResourceValidator : AbstractValidator<ChangeResourceCommand>
{
    public ChangeResourceValidator()
    {
        RuleFor(x => x.Name).NotNull().NotEmpty().MaximumLength(50);
    }
}
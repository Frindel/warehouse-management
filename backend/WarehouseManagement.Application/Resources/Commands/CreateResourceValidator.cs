using FluentValidation;

namespace WarehouseManagement.Application.Resources.Commands;

public class CreateResourceValidator : AbstractValidator<CreateResourceCommand>
{
    public CreateResourceValidator()
    {
        RuleFor(x => x.Name).NotNull().NotEmpty();
    }
}
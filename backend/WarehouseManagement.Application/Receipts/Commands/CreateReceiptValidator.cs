using FluentValidation;

namespace WarehouseManagement.Application.Receipts.Commands;

public class CreateReceiptValidator : AbstractValidator<CreateReceiptCommand>
{
    public CreateReceiptValidator()
    {
        RuleFor(x => x.Number).NotNull().NotEmpty();
        RuleForEach(x => x.Resources)
            .ChildRules(validator =>
            {
                validator.RuleFor(x => x.Quantity).GreaterThan(0);
            })
            .When(x => x.Resources != null);
    }
}
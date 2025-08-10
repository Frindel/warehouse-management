using FluentValidation;

namespace WarehouseManagement.Application.Receipts.Queries;

public class FindReceiptsValidator : AbstractValidator<FindReceiptsQuery>
{
    public FindReceiptsValidator()
    {
        RuleFor(x => x.From)
            .NotNull()
            .When(x => x.To != null);

        RuleFor(x => x.To)
            .NotNull()
            .When(x => x.From != null);
    }
}
using FluentValidation;

namespace WarehouseManagement.Application.Receipts.Commands;

public class ChangeReceiptValidator : AbstractValidator<ChangeReceiptCommand>
{
    public ChangeReceiptValidator()
    {
        RuleFor(x => x.Number)
            .NotNull()
            .NotEmpty();

        RuleForEach(x => x.Resources)
            .ChildRules(validator =>
            {
                validator.RuleFor(x => x.Quantity)
                    .GreaterThan(0);
            })
            .When(x => x.Resources != null);

        RuleFor(x => x.Resources)
            .Must(resources =>
            {
                if (resources == null) return true;

                return resources
                    .Select(r => (r.ResourceId, r.UnitId))
                    .Distinct()
                    .Count() == resources.Count;
            })
            .WithMessage("Список ресурсов содержит дубликаты по resource и unit");
    }
}
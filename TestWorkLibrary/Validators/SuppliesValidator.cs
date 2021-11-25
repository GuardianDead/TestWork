using FluentValidation;
using TestWorkLibrary.Models;

namespace TestWorkLibrary.Validators
{
    public class SuppliesValidator : AbstractValidator<Supplies>
    {
        public SuppliesValidator()
        {
            RuleFor(p => p.Items)
                .NotNull().WithMessage("Параметр 'Items' для 'Supplies' не может быть null");
            RuleFor(p => p.Includes)
                .NotNull().WithMessage("Параметр 'Includes' для 'Supplies' не может быть null");
        }
    }
}

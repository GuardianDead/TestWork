using FluentValidation;
using TestWorkLibrary.Models;

namespace TestWorkLibrary.Validators
{
    public class SupplyValidator : AbstractValidator<Supply>
    {
        public SupplyValidator()
        {
            RuleFor(p => p.Name)
                .NotNull().WithMessage("Параметр 'Name' для 'Supply' не может быть null")
                .NotEmpty().WithMessage("Параметр 'Name' для 'Supply' не может быть пустым");
            RuleFor(p => p.Count)
                .GreaterThanOrEqualTo(1).WithMessage("Параметр 'Count' для 'Supply' должен быть больше или равен 1");
            RuleFor(p => p.Probability)
                .InclusiveBetween(0.0, 1.0).WithMessage("Параметр 'Probability' для 'Supply' должен быть в диапазоне от 0.0 до 1.0");
            RuleFor(p => p.Condition)
                .InclusiveBetween(0.0, 1.0).WithMessage("Параметр 'Condition' для 'Supply' должен быть в диапазоне от 0.0 до 1.0");
        }
    }
}

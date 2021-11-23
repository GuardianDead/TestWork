using FluentValidation;
using TestWorkLibrary.Models;

namespace TestWorkLibrary.Validators
{
    public class CharacterValidator : AbstractValidator<Character>
    {
        public CharacterValidator()
        {
            RuleFor(p => p.Id)
                .NotNull().WithMessage("Параметр 'Id' для 'Character' не может быть null")
                .NotEmpty().WithMessage("Параметр 'Id' для 'Character' не может быть пустым");
            RuleFor(p => p.Supplies)
                .NotNull().WithMessage("Параметр 'Supplies' для 'Character' не может быть null");
        }
    }
}

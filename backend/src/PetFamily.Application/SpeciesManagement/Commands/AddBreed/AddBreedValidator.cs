using FluentValidation;
using PetFamily.Application.Validators;
using PetFamily.Domain.ValueObjects.String;

namespace PetFamily.Application.SpeciesManagement.Commands.AddBreed;

public class AddBreedValidator
    : AbstractValidator<AddBreedCommand>
{
    public AddBreedValidator()
    {
        RuleFor(x => x.BreedName).MustBeValueObject(NotEmptyString.Create);
    }
}
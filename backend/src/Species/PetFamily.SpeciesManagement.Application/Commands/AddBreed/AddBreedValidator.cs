using FluentValidation;
using PetFamily.Core.Validators;
using PetFamily.SharedKernel.ValueObjects.String;

namespace PetFamily.SpeciesManagement.Application.Commands.AddBreed;

public class AddBreedValidator
    : AbstractValidator<AddBreedCommand>
{
    public AddBreedValidator()
    {
        RuleFor(x => x.BreedName).MustBeValueObject(NotEmptyString.Create);
    }
}
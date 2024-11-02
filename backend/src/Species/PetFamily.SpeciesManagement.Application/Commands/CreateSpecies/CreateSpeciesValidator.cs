using FluentValidation;
using PetFamily.Core.Validators;
using PetFamily.SharedKernel.ValueObjects.String;

namespace PetFamily.SpeciesManagement.Application.Commands.CreateSpecies;

public class CreateSpeciesValidator
    : AbstractValidator<CreateSpeciesCommand>
{
    public CreateSpeciesValidator()
    {
        RuleFor(x => x.Name).MustBeValueObject(NotEmptyString.Create);
    }
}
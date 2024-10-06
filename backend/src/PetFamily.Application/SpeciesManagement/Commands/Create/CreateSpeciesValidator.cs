using FluentValidation;
using PetFamily.Application.Validators;
using PetFamily.Domain.ValueObjects.String;

namespace PetFamily.Application.SpeciesManagement.Commands.Create;

public class CreateSpeciesValidator
    : AbstractValidator<CreateSpeciesCommand>
{
    public CreateSpeciesValidator()
    {
        RuleFor(x => x.Name).MustBeValueObject(NotEmptyString.Create);
    }
}
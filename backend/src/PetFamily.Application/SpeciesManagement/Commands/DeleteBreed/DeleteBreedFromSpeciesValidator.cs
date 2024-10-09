using FluentValidation;
using PetFamily.Application.Validators;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.SpeciesManagement.Commands.DeleteBreed;

public class DeleteBreedFromSpeciesValidator
    : AbstractValidator<DeleteBreedFromSpeciesCommand>
{
    public DeleteBreedFromSpeciesValidator()
    {
        RuleFor(x => x.SpeciesId).NotEmpty().WithError(Errors.General.ValueIsRequired("SpeciesId"));
        RuleFor(x => x.BreedId).NotEmpty().WithError(Errors.General.ValueIsRequired("BreedId"));
    }
}
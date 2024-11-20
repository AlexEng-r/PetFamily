using FluentValidation;
using PetFamily.Core.Validators;
using PetFamily.SharedKernel;

namespace PetFamily.SpeciesManagement.Application.Commands.DeleteBreed;

public class DeleteBreedFromSpeciesValidator
    : AbstractValidator<DeleteBreedFromSpeciesCommand>
{
    public DeleteBreedFromSpeciesValidator()
    {
        RuleFor(x => x.SpeciesId).NotEmpty().WithError(Errors.General.ValueIsRequired("SpeciesId"));
        RuleFor(x => x.BreedId).NotEmpty().WithError(Errors.General.ValueIsRequired("BreedId"));
    }
}
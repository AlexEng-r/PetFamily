using FluentValidation;
using PetFamily.Application.Validators;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.SpeciesManagement.Commands.DeleteSpecies;

public class DeleteSpeciesValidator
    : AbstractValidator<DeleteSpeciesCommand>
{
    public DeleteSpeciesValidator()
    {
        RuleFor(x => x.SpeciesId).NotEmpty().WithError(Errors.General.ValueIsRequired("SpeciesId"));
    }
}
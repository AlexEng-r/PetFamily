using FluentValidation;
using PetFamily.Core.Validators;
using PetFamily.SharedKernel;

namespace PetFamily.SpeciesManagement.Application.Commands.DeleteSpecies;

public class DeleteSpeciesValidator
    : AbstractValidator<DeleteSpeciesCommand>
{
    public DeleteSpeciesValidator()
    {
        RuleFor(x => x.SpeciesId).NotEmpty().WithError(Errors.General.ValueIsRequired("SpeciesId"));
    }
}
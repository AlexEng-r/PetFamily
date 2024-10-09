using FluentValidation;
using PetFamily.Application.Validators;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.SpeciesManagement.Queries.GetBreedsBySpeciesId;

public class GetBreedsBySpeciesIdValidator
    : AbstractValidator<GetBreedsBySpeciesIdQuery>
{
    public GetBreedsBySpeciesIdValidator()
    {
        RuleFor(x => x.SpeciesId).NotEmpty().WithError(Errors.General.ValueIsRequired("SpeciesId"));
    }
}
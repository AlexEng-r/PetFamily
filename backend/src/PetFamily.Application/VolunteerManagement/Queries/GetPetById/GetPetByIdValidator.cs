using FluentValidation;
using PetFamily.Application.Validators;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.VolunteerManagement.Queries.GetPetById;

public class GetPetByIdValidator
    : AbstractValidator<GetPetByIdQuery>
{
    public GetPetByIdValidator()
    {
        RuleFor(x => x.PetId).NotEmpty().WithError(Errors.General.ValueIsRequired("PetId"));
    }
}
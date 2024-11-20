using FluentValidation;
using PetFamily.Core.Validators;
using PetFamily.SharedKernel;

namespace PetFamily.VolunteerManagement.Application.Queries.GetPetById;

public class GetPetByIdValidator
    : AbstractValidator<GetPetByIdQuery>
{
    public GetPetByIdValidator()
    {
        RuleFor(x => x.PetId).NotEmpty().WithError(Errors.General.ValueIsRequired("PetId"));
    }
}
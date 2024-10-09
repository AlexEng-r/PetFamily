using FluentValidation;
using PetFamily.Application.Validators;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.VolunteerManagement.Queries.GetVolunteerById;

public class GetVolunteerByIdValidator
    : AbstractValidator<GetVolunteerByIdQuery>
{
    public GetVolunteerByIdValidator()
    {
        RuleFor(x => x.VolunteerId).NotEmpty().WithError(Errors.General.ValueIsRequired("VolunteerId"));
    }
}
using FluentValidation;
using PetFamily.Application.Validators;
using PetFamily.Domain.SeedWork;

namespace PetFamily.Application.Volunteers.Delete;

public class DeleteVolunteerRequestValidator
    : AbstractValidator<DeleteVolunteerRequest>
{
    public DeleteVolunteerRequestValidator()
    {
        RuleFor(x => x.VolunteerId).NotEmpty().WithError(Errors.General.ValueIsRequired());
    }
}
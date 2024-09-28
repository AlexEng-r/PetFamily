using FluentValidation;
using PetFamily.Application.Validators;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.VolunteerManagement.Commands.ChangePetPosition;

public class ChangePetPositionValidator
    : AbstractValidator<ChangePetPositionCommand>
{
    public ChangePetPositionValidator()
    {
        RuleFor(x => x.VolunteerId)
            .NotEmpty()
            .WithError(Errors.General.ValueIsRequired("VolunteerId"));

        RuleFor(x => x.PetId)
            .NotEmpty()
            .WithError(Errors.General.ValueIsRequired("PetId"));

        RuleFor(x => x.PetPosition)
            .GreaterThanOrEqualTo(1)
            .WithError(Errors.General.ValueIsInvalid("PetPosition"));
    }
}
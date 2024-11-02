using FluentValidation;
using PetFamily.Core.Validators;
using PetFamily.SharedKernel;

namespace PetFamily.VolunteerManagement.Application.Commands.ChangePetPosition;

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
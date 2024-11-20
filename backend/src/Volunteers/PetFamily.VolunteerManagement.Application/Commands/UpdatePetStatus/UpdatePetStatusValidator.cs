using FluentValidation;
using PetFamily.Core.Enums;
using PetFamily.Core.Validators;
using PetFamily.SharedKernel;

namespace PetFamily.VolunteerManagement.Application.Commands.UpdatePetStatus;

public class UpdatePetStatusValidator
    : AbstractValidator<UpdatePetStatusCommand>
{
    public UpdatePetStatusValidator()
    {
        RuleFor(x => x.VolunteerId).NotEmpty().WithError(Errors.General.ValueIsRequired("VolunteerId"));
        RuleFor(x => x.PetId).NotEmpty().WithError(Errors.General.ValueIsRequired("PetId"));
        RuleFor(x => x.Status)
            .Must(x => x is StatusType.LookingForAHome or StatusType.FoundAHome)
            .WithError(Errors.General.ValueIsInvalid("Status"));
    }
}
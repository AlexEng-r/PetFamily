using FluentValidation;
using PetFamily.Application.Validators;
using PetFamily.Domain.Shared;
using PetFamily.Domain.VolunteerManagement.Enums;

namespace PetFamily.Application.VolunteerManagement.Commands.UpdatePetStatus;

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
using FluentValidation;
using PetFamily.Application.Validators;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.VolunteerManagement.Commands.DeletePhoto;

public class DeletePhotoFromPetValidator
    : AbstractValidator<DeletePhotoFromPetCommand>
{
    public DeletePhotoFromPetValidator()
    {
        RuleFor(x => x.VolunteerId).NotEmpty().WithError(Errors.General.ValueIsRequired("VolunteerId"));
        RuleFor(x => x.PetId).NotEmpty().WithError(Errors.General.ValueIsRequired("PetId"));
    }
}
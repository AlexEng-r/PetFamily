using FluentValidation;
using PetFamily.Application.Validators;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.Volunteers.AddPetPhoto;

public class AddPetPhotoValidator
    : AbstractValidator<AddPetPhotoCommand>
{
    public AddPetPhotoValidator()
    {
        RuleFor(p => p.VolunteerId)
            .NotEmpty()
            .WithError(Errors.General.ValueIsRequired("VolunteerId"));

        RuleFor(p => p.PetId)
            .NotEmpty()
            .WithError(Errors.General.ValueIsRequired("PetId"));

        RuleForEach(p => p.Files)
            .NotEmpty()
            .WithError(Errors.General.ValueIsRequired("file"));
    }
}
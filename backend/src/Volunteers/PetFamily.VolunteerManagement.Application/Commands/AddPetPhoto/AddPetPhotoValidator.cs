using FluentValidation;
using PetFamily.Core.Dtos;
using PetFamily.Core.Validators;
using PetFamily.SharedKernel;

namespace PetFamily.VolunteerManagement.Application.Commands.AddPetPhoto;

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
            .SetValidator(new UploadFileDtoValidator());
    }
}

public class UploadFileDtoValidator
    : AbstractValidator<UploadFileDto>
{
    public UploadFileDtoValidator()
    {
        RuleFor(x => x.ObjectName).NotEmpty().WithError(Errors.General.ValueIsRequired());
        RuleFor(x => x.Stream).Must(x => x.Length < 5000000);
    }
}
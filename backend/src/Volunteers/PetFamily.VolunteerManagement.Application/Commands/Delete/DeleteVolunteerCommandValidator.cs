using FluentValidation;
using PetFamily.Core.Validators;
using PetFamily.SharedKernel;

namespace PetFamily.VolunteerManagement.Application.Commands.Delete;

public class DeleteVolunteerCommandValidator
    : AbstractValidator<DeleteVolunteerCommand>
{
    public DeleteVolunteerCommandValidator()
    {
        RuleFor(x => x.VolunteerId).NotEmpty().WithError(Errors.General.ValueIsRequired());
    }
}
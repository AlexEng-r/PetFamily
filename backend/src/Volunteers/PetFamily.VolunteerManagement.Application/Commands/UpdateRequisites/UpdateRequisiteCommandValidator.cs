using FluentValidation;
using PetFamily.Core.Validators;
using PetFamily.SharedKernel;
using PetFamily.SharedKernel.ValueObjects.Requisites;

namespace PetFamily.VolunteerManagement.Application.Commands.UpdateRequisites;

public class UpdateRequisiteCommandValidator
    : AbstractValidator<UpdateRequisiteCommand>
{
    public UpdateRequisiteCommandValidator()
    {
        RuleFor(x => x.VolunteerId).NotEmpty().WithError(Errors.General.ValueIsRequired());
        RuleForEach(x => x.Requisites).MustBeValueObject(x => Requisite.Create(x.Name, x.Description));
    }
}
using FluentValidation;
using PetFamily.Application.Validators;
using PetFamily.Domain.Shared;
using PetFamily.Domain.ValueObjects.Requisites;

namespace PetFamily.Application.VolunteerManagement.Commands.UpdateRequisites;

public class UpdateRequisiteCommandValidator
    : AbstractValidator<UpdateRequisiteCommand>
{
    public UpdateRequisiteCommandValidator()
    {
        RuleFor(x => x.VolunteerId).NotEmpty().WithError(Errors.General.ValueIsRequired());
        RuleForEach(x => x.Requisites).MustBeValueObject(x => Requisite.Create(x.Name, x.Description));
    }
}
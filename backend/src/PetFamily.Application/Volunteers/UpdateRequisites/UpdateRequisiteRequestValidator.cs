using FluentValidation;
using PetFamily.Application.Validators;
using PetFamily.Domain.Requisites;
using PetFamily.Domain.SeedWork;

namespace PetFamily.Application.Volunteers.UpdateRequisites;

public class UpdateRequisiteRequestValidator
    : AbstractValidator<UpdateRequisiteRequest>
{
    public UpdateRequisiteRequestValidator()
    {
        RuleFor(x => x.VolunteerId).NotEmpty().WithError(Errors.General.ValueIsRequired());
    }
}

public class UpdateRequisiteDtoValidator
    : AbstractValidator<UpdateRequisiteDto>
{
    public UpdateRequisiteDtoValidator()
    {
        RuleForEach(x => x.Requisites).MustBeValueObject(x => Requisite.Create(x.Name, x.Description));
    }
}
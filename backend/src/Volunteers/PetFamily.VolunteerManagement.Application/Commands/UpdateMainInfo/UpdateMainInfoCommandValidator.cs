using FluentValidation;
using PetFamily.Core.Validators;
using PetFamily.SharedKernel;
using PetFamily.SharedKernel.ValueObjects.Contacts;
using PetFamily.SharedKernel.ValueObjects.FullNames;
using PetFamily.SharedKernel.ValueObjects.String;

namespace PetFamily.VolunteerManagement.Application.Commands.UpdateMainInfo;

public class UpdateMainInfoCommandValidator
    : AbstractValidator<UpdateMainInfoCommand>
{
    public UpdateMainInfoCommandValidator()
    {
        RuleFor(x => x.VolunteerId).NotEmpty().WithError(Errors.General.ValueIsRequired());
        RuleFor(x => x.FullName).MustBeValueObject(x => FullName.Create(x.FirstName, x.Surname, x.Patronymic));
        RuleFor(x => x.Description).MustBeValueObject(NotEmptyString.Create);
        RuleFor(x => x.Experience).GreaterThanOrEqualTo(0).WithError(Errors.General.ValueIsInvalid("Experience"));
        RuleFor(x => x.Phone).MustBeValueObject(ContactPhone.Create);
    }
}
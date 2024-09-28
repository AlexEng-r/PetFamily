using FluentValidation;
using PetFamily.Application.Validators;
using PetFamily.Domain.Shared;
using PetFamily.Domain.ValueObjects.Contacts;
using PetFamily.Domain.ValueObjects.FullNames;
using PetFamily.Domain.ValueObjects.String;

namespace PetFamily.Application.VolunteerManagement.Commands.UpdateMainInfo;

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
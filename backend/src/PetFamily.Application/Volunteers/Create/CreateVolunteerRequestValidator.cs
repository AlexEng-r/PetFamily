using FluentValidation;
using PetFamily.Application.Validators;
using PetFamily.Domain.Shared;
using PetFamily.Domain.ValueObjects.Contacts;
using PetFamily.Domain.ValueObjects.FullNames;
using PetFamily.Domain.ValueObjects.Requisites;
using PetFamily.Domain.ValueObjects.SocialNetworks;
using PetFamily.Domain.ValueObjects.String;

namespace PetFamily.Application.Volunteers.Create;

public class CreateVolunteerRequestValidator
    : AbstractValidator<CreateVolunteerRequest>
{
    public CreateVolunteerRequestValidator()
    {
        RuleFor(x => x.FullName).MustBeValueObject(x => FullName.Create(x.FirstName, x.Surname, x.Patronymic));
        RuleFor(x => x.Description).MustBeValueObject(NotEmptyString.Create);
        RuleFor(x => x.Experience).GreaterThanOrEqualTo(0).WithError(Errors.General.ValueIsInvalid("Experience"));
        RuleFor(x => x.Phone).MustBeValueObject(ContactPhone.Create);

        RuleForEach(x => x.Requisites).MustBeValueObject(x => Requisite.Create(x.Name, x.Description));
        RuleForEach(x => x.SocialNetworks).MustBeValueObject(x => SocialNetwork.Create(x.Name, x.Link));
    }
}
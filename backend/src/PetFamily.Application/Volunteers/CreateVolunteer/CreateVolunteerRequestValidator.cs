using FluentValidation;
using PetFamily.Application.Validators;
using PetFamily.Domain.Contacts;
using PetFamily.Domain.FullNames;
using PetFamily.Domain.Requisites;
using PetFamily.Domain.SeedWork;
using PetFamily.Domain.SocialNetworks;
using PetFamily.Domain.String;

namespace PetFamily.Application.Volunteers.CreateVolunteer;

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
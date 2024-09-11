using FluentValidation;
using PetFamily.Application.Validators;
using PetFamily.Domain.ValueObjects.Addresses;
using PetFamily.Domain.ValueObjects.Contacts;
using PetFamily.Domain.ValueObjects.Requisites;
using PetFamily.Domain.ValueObjects.String;

namespace PetFamily.Application.Volunteers.AddPets;

public class AddPetValidator
    : AbstractValidator<AddPetCommand>
{
    public AddPetValidator()
    {
        RuleFor(x => x.NickName).MustBeValueObject(NotEmptyString.Create);
        RuleFor(x => x.AnimalType).MustBeValueObject(NotEmptyString.Create);
        RuleFor(x => x.Color).MustBeValueObject(NotEmptyString.Create);
        RuleFor(x => x.AddressDto).MustBeValueObject(x => Address.Create(x.City, x.House, x.Flat));
        RuleFor(x => x.Phone).MustBeValueObject(ContactPhone.Create);
        RuleForEach(x => x.Requisites).MustBeValueObject(x => Requisite.Create(x.Name, x.Description));
    }
}
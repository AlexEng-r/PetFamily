using FluentValidation;
using PetFamily.Core.Validators;
using PetFamily.SharedKernel;
using PetFamily.SharedKernel.ValueObjects.Addresses;
using PetFamily.SharedKernel.ValueObjects.Contacts;
using PetFamily.SharedKernel.ValueObjects.Requisites;
using PetFamily.SharedKernel.ValueObjects.String;

namespace PetFamily.VolunteerManagement.Application.Commands.UpdatePet;

public class UpdatePetValidator
    : AbstractValidator<UpdatePetCommand>
{
    public UpdatePetValidator()
    {
        RuleFor(x => x.VolunteerId).NotEmpty().WithError(Errors.General.ValueIsRequired());
        RuleFor(x => x.PetId).NotEmpty().WithError(Errors.General.ValueIsRequired());
        RuleFor(x => x.NickName).MustBeValueObject(NotEmptyString.Create);
        RuleFor(x => x.AnimalType).MustBeValueObject(NotEmptyString.Create);
        RuleFor(x => x.Color).MustBeValueObject(NotEmptyString.Create);
        RuleFor(x => x.AddressDto).MustBeValueObject(x => Address.Create(x.City, x.House, x.Flat));
        RuleFor(x => x.Phone).MustBeValueObject(ContactPhone.Create);
        RuleForEach(x => x.Requisites).MustBeValueObject(x => Requisite.Create(x.Name, x.Description));
    }
}
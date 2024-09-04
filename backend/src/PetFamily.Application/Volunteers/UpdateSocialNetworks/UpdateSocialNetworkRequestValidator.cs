using FluentValidation;
using PetFamily.Application.Validators;
using PetFamily.Domain.Shared;
using PetFamily.Domain.ValueObjects.SocialNetworks;

namespace PetFamily.Application.Volunteers.UpdateSocialNetworks;

public class UpdateSocialNetworkRequestValidator
    : AbstractValidator<UpdateSocialNetworkRequest>
{
    public UpdateSocialNetworkRequestValidator()
    {
        RuleFor(x => x.VolunteerId).NotEmpty().WithError(Errors.General.ValueIsRequired());
    }
}

public class UpdateSocialNetworkDtoValidator
    : AbstractValidator<UpdateSocialNetworkDto>
{
    public UpdateSocialNetworkDtoValidator()
    {
        RuleForEach(x => x.SocialNetworks).MustBeValueObject(x => SocialNetwork.Create(x.Name, x.Link));
    }
}
using FluentValidation;
using PetFamily.Application.Validators;
using PetFamily.Domain.Shared;
using PetFamily.Domain.ValueObjects.SocialNetworks;

namespace PetFamily.Application.VolunteerManagement.Commands.UpdateSocialNetworks;

public class UpdateSocialNetworkCommandValidator
    : AbstractValidator<UpdateSocialNetworkCommand>
{
    public UpdateSocialNetworkCommandValidator()
    {
        RuleFor(x => x.VolunteerId).NotEmpty().WithError(Errors.General.ValueIsRequired());
        RuleForEach(x => x.SocialNetworks).MustBeValueObject(x => SocialNetwork.Create(x.Name, x.Link));
    }
}
using FluentValidation;
using PetFamily.Core.Validators;
using PetFamily.SharedKernel;
using PetFamily.SharedKernel.ValueObjects.SocialNetworks;

namespace PetFamily.VolunteerManagement.Application.Commands.UpdateSocialNetworks;

public class UpdateSocialNetworkCommandValidator
    : AbstractValidator<UpdateSocialNetworkCommand>
{
    public UpdateSocialNetworkCommandValidator()
    {
        RuleFor(x => x.VolunteerId).NotEmpty().WithError(Errors.General.ValueIsRequired());
        RuleForEach(x => x.SocialNetworks).MustBeValueObject(x => SocialNetwork.Create(x.Name, x.Link));
    }
}
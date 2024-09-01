using CSharpFunctionalExtensions;
using PetFamily.Application.Repositories.Volunteers;
using PetFamily.Domain.Contacts;
using PetFamily.Domain.FullNames;
using PetFamily.Domain.Requisites;
using PetFamily.Domain.SeedWork;
using PetFamily.Domain.SocialNetworks;
using PetFamily.Domain.String;
using PetFamily.Domain.Volunteers;

namespace PetFamily.Application.Volunteers.CreateVolunteer;

public class CreateVolunteersService
{
    private readonly IVolunteersRepository _volunteersRepository;

    public CreateVolunteersService(IVolunteersRepository volunteersRepository)
    {
        _volunteersRepository = volunteersRepository;
    }

    public async Task<Result<Guid, Error>> Create(CreateVolunteerRequest request, CancellationToken cancellationToken)
    {
        var phone = ContactPhone.Create(request.Phone);
        if (phone.IsFailure)
        {
            return phone.Error;
        }

        if (await _volunteersRepository.AnyByPhone(phone.Value, cancellationToken))
        {
            return Errors.Model.AlreadyExist("Volunteer");
        }

        var fullName = FullName.Create(request.FirstName, request.Surname, request.Patronymic);
        if (fullName.IsFailure)
        {
            return fullName.Error;
        }

        var description = NotEmptyString.Create(request.Description);
        if (description.IsFailure)
        {
            return description.Error;
        }

        var volunteer = new Volunteer(VolunteerId.NewVolunteerId(), fullName.Value, description.Value,
            request.Experience, phone.Value);

        if (request.Requisites is { Count: > 0 })
        {
            var requisites = request.Requisites
                .Select(x => Requisite.Create(x.Name, x.Description))
                .ToArray();
            if (requisites.Any(x => x.IsFailure))
            {
                return Errors.General.ValueIsInvalid("requisites");
            }

            volunteer.SetRequisites(requisites.Select(x => x.Value).ToArray());
        }

        if (request.SocialNetworks is { Count: > 0 })
        {
            var socialNetworks = request.SocialNetworks
                .Select(x => SocialNetwork.Create(x.Name, x.Link))
                .ToArray();
            if (socialNetworks.Any(x => x.IsFailure))
            {
                return Errors.General.ValueIsInvalid("Social networks");
            }

            volunteer.SetSocialNetworks(socialNetworks.Select(x => x.Value).ToArray());
        }

        return await _volunteersRepository.Add(volunteer, cancellationToken);
    }
}
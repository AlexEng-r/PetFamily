using CSharpFunctionalExtensions;
using PetFamily.Application.Repositories.Volunteers;
using PetFamily.Domain.Shared;
using PetFamily.Domain.ValueObjects.Contacts;
using PetFamily.Domain.ValueObjects.FullNames;
using PetFamily.Domain.ValueObjects.Requisites;
using PetFamily.Domain.ValueObjects.SocialNetworks;
using PetFamily.Domain.ValueObjects.String;
using PetFamily.Domain.VolunteerManagement.Volunteers;

namespace PetFamily.Application.Volunteers.Create;

public class CreateVolunteersHandler
{
    private readonly IVolunteersRepository _volunteersRepository;

    public CreateVolunteersHandler(IVolunteersRepository volunteersRepository)
    {
        _volunteersRepository = volunteersRepository;
    }

    public async Task<Result<Guid, Error>> Handle(CreateVolunteerRequest request, CancellationToken cancellationToken)
    {
        var phone = ContactPhone.Create(request.Phone);

        if (await _volunteersRepository.AnyByPhone(phone.Value, cancellationToken))
        {
            return Errors.Model.AlreadyExist("Volunteer");
        }

        var fullName = FullName.Create(request.FullName.FirstName, request.FullName.Surname,
            request.FullName.Patronymic);
        var description = NotEmptyString.Create(request.Description);

        var volunteer = new Volunteer(VolunteerId.NewVolunteerId(), fullName.Value, description.Value,
            request.Experience, phone.Value);

        if (request.Requisites is { Count: > 0 })
        {
            var requisites = request.Requisites
                .Select(x => Requisite.Create(x.Name, x.Description))
                .ToArray();

            volunteer.SetRequisites(requisites.Select(x => x.Value).ToArray());
        }

        if (request.SocialNetworks is { Count: > 0 })
        {
            var socialNetworks = request.SocialNetworks
                .Select(x => SocialNetwork.Create(x.Name, x.Link))
                .ToArray();

            volunteer.SetSocialNetworks(socialNetworks.Select(x => x.Value).ToArray());
        }

        return await _volunteersRepository.Add(volunteer, cancellationToken);
    }
}
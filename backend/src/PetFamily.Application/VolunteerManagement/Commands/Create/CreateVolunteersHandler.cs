using CSharpFunctionalExtensions;
using FluentValidation;
using PetFamily.Application.Abstractions;
using PetFamily.Application.Extensions;
using PetFamily.Application.Repositories.Volunteers;
using PetFamily.Domain.Shared;
using PetFamily.Domain.ValueObjects.Contacts;
using PetFamily.Domain.ValueObjects.FullNames;
using PetFamily.Domain.ValueObjects.Requisites;
using PetFamily.Domain.ValueObjects.SocialNetworks;
using PetFamily.Domain.ValueObjects.String;
using PetFamily.Domain.VolunteerManagement.Volunteers;

namespace PetFamily.Application.VolunteerManagement.Commands.Create;

public class CreateVolunteersHandler
    : ICommandHandler<CreateVolunteerCommand, Guid>
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly IValidator<CreateVolunteerCommand> _validator;

    public CreateVolunteersHandler(IVolunteersRepository volunteersRepository,
        IValidator<CreateVolunteerCommand> validator)
    {
        _volunteersRepository = volunteersRepository;
        _validator = validator;
    }

    public async Task<Result<Guid, ErrorList>> Handle(CreateVolunteerCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
        {
            return validationResult.ToErrorList();
        }
        
        var phone = ContactPhone.Create(command.Phone);

        if (await _volunteersRepository.AnyByPhone(phone.Value, cancellationToken))
        {
            return Errors.Model.AlreadyExist("Volunteer").ToErrorList();
        }

        var fullName = FullName.Create(command.FullName.FirstName, command.FullName.Surname,
            command.FullName.Patronymic);
        var description = NotEmptyString.Create(command.Description);

        var volunteer = new Volunteer(VolunteerId.NewVolunteerId(), fullName.Value, description.Value,
            command.Experience, phone.Value);

        if (command.Requisites is { Count: > 0 })
        {
            var requisites = command.Requisites
                .Select(x => Requisite.Create(x.Name, x.Description))
                .Select(x => x.Value)
                .ToList();

            volunteer.SetRequisites(requisites);
        }

        if (command.SocialNetworks is { Count: > 0 })
        {
            var socialNetworks = command.SocialNetworks
                .Select(x => SocialNetwork.Create(x.Name, x.Link))
                .Select(x => x.Value)
                .ToList();

            volunteer.SetSocialNetworks(socialNetworks);
        }

        return await _volunteersRepository.Add(volunteer, cancellationToken);
    }
}
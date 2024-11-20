using CSharpFunctionalExtensions;
using FluentValidation;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Extensions;
using PetFamily.SharedKernel;
using PetFamily.SharedKernel.ValueObjects.Contacts;
using PetFamily.SharedKernel.ValueObjects.FullNames;
using PetFamily.SharedKernel.ValueObjects.Ids;
using PetFamily.SharedKernel.ValueObjects.Requisites;
using PetFamily.SharedKernel.ValueObjects.SocialNetworks;
using PetFamily.SharedKernel.ValueObjects.String;
using PetFamily.VolunteerManagement.Application.Repositories;
using PetFamily.VolunteerManagement.Domain;

namespace PetFamily.VolunteerManagement.Application.Commands.Create;

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
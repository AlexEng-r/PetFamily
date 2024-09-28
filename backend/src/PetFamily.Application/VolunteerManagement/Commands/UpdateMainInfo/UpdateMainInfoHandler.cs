using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Abstractions;
using PetFamily.Application.Database;
using PetFamily.Application.Extensions;
using PetFamily.Application.Repositories.Volunteers;
using PetFamily.Domain.Shared;
using PetFamily.Domain.ValueObjects.Contacts;
using PetFamily.Domain.ValueObjects.FullNames;
using PetFamily.Domain.ValueObjects.String;

namespace PetFamily.Application.VolunteerManagement.Commands.UpdateMainInfo;

public class UpdateMainInfoHandler
    : ICommandHandler<UpdateMainInfoCommand, Guid>
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UpdateMainInfoCommand> _logger;
    private readonly IValidator<UpdateMainInfoCommand> _validator;

    public UpdateMainInfoHandler(
        IVolunteersRepository volunteersRepository,
        ILogger<UpdateMainInfoCommand> logger,
        IUnitOfWork unitOfWork,
        IValidator<UpdateMainInfoCommand> validator)
    {
        _volunteersRepository = volunteersRepository;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<Result<Guid, ErrorList>> Handle(UpdateMainInfoCommand command,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
        {
            return validationResult.ToErrorList();
        }
        
        var volunteer = await _volunteersRepository.GetById(command.VolunteerId, cancellationToken);
        if (volunteer.IsFailure)
        {
            return volunteer.Error.ToErrorList();
        }

        var fullName = FullName.Create(command.FullName.FirstName, command.FullName.Surname,
            command.FullName.Patronymic).Value;
        var description = NotEmptyString.Create(command.Description).Value;
        var phone = ContactPhone.Create(command.Phone).Value;

        volunteer.Value.UpdateMainInfo(fullName, description, command.Experience, phone);

        await _unitOfWork.SaveChanges(cancellationToken);

        _logger.LogInformation("Successfully updated volunteer #{Id}", command.VolunteerId);

        return volunteer.Value.Id.Value;
    }
}
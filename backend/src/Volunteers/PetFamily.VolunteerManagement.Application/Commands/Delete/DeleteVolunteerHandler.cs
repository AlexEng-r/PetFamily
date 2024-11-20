using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Extensions;
using PetFamily.SharedKernel;
using PetFamily.VolunteerManagement.Application.Repositories;

namespace PetFamily.VolunteerManagement.Application.Commands.Delete;

public class DeleteVolunteerHandler
    : ICommandHandler<DeleteVolunteerCommand, Guid>
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly IVolunteerUnitOfWork _volunteerUnitOfWork;
    private readonly ILogger<DeleteVolunteerHandler> _logger;
    private readonly IValidator<DeleteVolunteerCommand> _validator;

    public DeleteVolunteerHandler(
        IVolunteersRepository volunteersRepository,
        ILogger<DeleteVolunteerHandler> logger,
        IVolunteerUnitOfWork volunteerUnitOfWork,
        IValidator<DeleteVolunteerCommand> validator)
    {
        _volunteersRepository = volunteersRepository;
        _logger = logger;
        _volunteerUnitOfWork = volunteerUnitOfWork;
        _validator = validator;
    }

    public async Task<Result<Guid, ErrorList>> Handle(DeleteVolunteerCommand command,
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

        await _volunteersRepository.Delete(volunteer.Value, cancellationToken);
        await _volunteerUnitOfWork.SaveChanges(cancellationToken);

        _logger.LogInformation("Successfully deleted volunteer #{Id}", command.VolunteerId);

        return volunteer.Value.Id.Value;
    }
}
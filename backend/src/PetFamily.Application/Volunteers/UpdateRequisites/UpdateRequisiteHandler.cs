using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Database;
using PetFamily.Application.Extensions;
using PetFamily.Application.Repositories.Volunteers;
using PetFamily.Domain.Shared;
using PetFamily.Domain.ValueObjects.Requisites;

namespace PetFamily.Application.Volunteers.UpdateRequisites;

public class UpdateRequisiteHandler
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UpdateRequisiteHandler> _logger;
    private readonly IValidator<UpdateRequisiteCommand> _validator;

    public UpdateRequisiteHandler(
        IVolunteersRepository volunteersRepository,
        IUnitOfWork unitOfWork,
        ILogger<UpdateRequisiteHandler> logger,
        IValidator<UpdateRequisiteCommand> validator)
    {
        _volunteersRepository = volunteersRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _validator = validator;
    }

    public async Task<Result<Guid, ErrorList>> Handle(UpdateRequisiteCommand command,
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

        var requisites = command.Requisites
            .Select(x => Requisite.Create(x.Name, x.Description))
            .Select(x => x.Value)
            .ToList();

        volunteer.Value.SetRequisites(requisites);

        await _unitOfWork.SaveChanges(cancellationToken);

        _logger.LogInformation("Successfully updated volunteer #{Id}", command.VolunteerId);

        return volunteer.Value.Id.Value;
    }
}
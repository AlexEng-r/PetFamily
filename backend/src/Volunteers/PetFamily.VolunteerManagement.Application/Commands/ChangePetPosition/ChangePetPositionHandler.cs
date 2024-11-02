using CSharpFunctionalExtensions;
using FluentValidation;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Extensions;
using PetFamily.SharedKernel;
using PetFamily.SharedKernel.ValueObjects.Positions;
using PetFamily.VolunteerManagement.Application.Repositories;

namespace PetFamily.VolunteerManagement.Application.Commands.ChangePetPosition;

public class ChangePetPositionHandler
    : ICommandHandler<ChangePetPositionCommand, Guid>
{
    private readonly IValidator<ChangePetPositionCommand> _validator;
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly IVolunteerUnitOfWork _volunteerUnitOfWork;

    public ChangePetPositionHandler(IValidator<ChangePetPositionCommand> validator,
        IVolunteersRepository volunteersRepository,
        IVolunteerUnitOfWork volunteerUnitOfWork)
    {
        _validator = validator;
        _volunteersRepository = volunteersRepository;
        _volunteerUnitOfWork = volunteerUnitOfWork;
    }

    public async Task<Result<Guid, ErrorList>> Handle(ChangePetPositionCommand command,
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

        var pet = volunteer.Value.GetPetById(command.PetId);
        if (pet == null)
        {
            return Errors.General.NotFound(command.PetId).ToErrorList();
        }

        var position = Position.Create(command.PetPosition).Value;

        var movePositionResult = volunteer.Value.MovePet(pet, position);
        if (movePositionResult.IsFailure)
        {
            return movePositionResult.Error.ToErrorList();
        }
        
        await _volunteerUnitOfWork.SaveChanges(cancellationToken);

        return pet.Id.Value;
    }
}
using CSharpFunctionalExtensions;
using FluentValidation;
using PetFamily.Application.Database;
using PetFamily.Application.Extensions;
using PetFamily.Application.Repositories.Volunteers;
using PetFamily.Domain.Shared;
using PetFamily.Domain.ValueObjects.Positions;

namespace PetFamily.Application.Volunteers.ChangePetPosition;

public class ChangePetPositionHandler
{
    private readonly IValidator<ChangePetPositionCommand> _validator;
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ChangePetPositionHandler(IValidator<ChangePetPositionCommand> validator,
        IVolunteersRepository volunteersRepository,
        IUnitOfWork unitOfWork)
    {
        _validator = validator;
        _volunteersRepository = volunteersRepository;
        _unitOfWork = unitOfWork;
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
        
        await _unitOfWork.SaveChanges(cancellationToken);

        return pet.Id.Value;
    }
}
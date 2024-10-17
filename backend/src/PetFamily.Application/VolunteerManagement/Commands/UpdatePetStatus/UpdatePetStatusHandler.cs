using CSharpFunctionalExtensions;
using FluentValidation;
using PetFamily.Application.Abstractions;
using PetFamily.Application.Database;
using PetFamily.Application.Extensions;
using PetFamily.Application.Repositories.Volunteers;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.VolunteerManagement.Commands.UpdatePetStatus;

public class UpdatePetStatusHandler
    : ICommandHandler<UpdatePetStatusCommand>
{
    private readonly IValidator<UpdatePetStatusCommand> _validator;
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdatePetStatusHandler(IValidator<UpdatePetStatusCommand> validator,
        IVolunteersRepository volunteersRepository,
        IUnitOfWork unitOfWork)
    {
        _validator = validator;
        _volunteersRepository = volunteersRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<UnitResult<ErrorList>> Handle(UpdatePetStatusCommand command, CancellationToken cancellationToken)
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

        var pet = volunteer.Value.Pets.FirstOrDefault(x => x.Id.Value == command.PetId);
        if (pet == null)
        {
            return Errors.General.NotFound(command.PetId).ToErrorList();
        }

        pet.SetStatus(command.Status);

        await _unitOfWork.SaveChanges(cancellationToken);

        return Result.Success<ErrorList>();
    }
}
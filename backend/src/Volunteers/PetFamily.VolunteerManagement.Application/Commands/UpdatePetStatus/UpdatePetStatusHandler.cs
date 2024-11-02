using CSharpFunctionalExtensions;
using FluentValidation;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Extensions;
using PetFamily.SharedKernel;
using PetFamily.VolunteerManagement.Application.Repositories;

namespace PetFamily.VolunteerManagement.Application.Commands.UpdatePetStatus;

public class UpdatePetStatusHandler
    : ICommandHandler<UpdatePetStatusCommand>
{
    private readonly IValidator<UpdatePetStatusCommand> _validator;
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly IVolunteerUnitOfWork _volunteerUnitOfWork;

    public UpdatePetStatusHandler(IValidator<UpdatePetStatusCommand> validator,
        IVolunteersRepository volunteersRepository,
        IVolunteerUnitOfWork volunteerUnitOfWork)
    {
        _validator = validator;
        _volunteersRepository = volunteersRepository;
        _volunteerUnitOfWork = volunteerUnitOfWork;
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

        await _volunteerUnitOfWork.SaveChanges(cancellationToken);

        return Result.Success<ErrorList>();
    }
}
using CSharpFunctionalExtensions;
using FluentValidation;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Extensions;
using PetFamily.SharedKernel;
using PetFamily.SpeciesManagement.Application.Repositories;
using PetFamily.VolunteerManagement.Contracts;

namespace PetFamily.SpeciesManagement.Application.Commands.DeleteSpecies;

public class DeleteSpeciesHandler
    : ICommandHandler<DeleteSpeciesCommand>
{
    private readonly IValidator<DeleteSpeciesCommand> _validator;
    private readonly ISpeciesRepository _speciesRepository;
    private readonly IVolunteerContract _volunteerContract;
    private readonly ISpeciesUnitOfWork _unitOfWork;

    public DeleteSpeciesHandler(IValidator<DeleteSpeciesCommand> validator,
        ISpeciesRepository speciesRepository,
        IVolunteerContract volunteerContract,
        ISpeciesUnitOfWork unitOfWork)
    {
        _validator = validator;
        _speciesRepository = speciesRepository;
        _volunteerContract = volunteerContract;
        _unitOfWork = unitOfWork;
    }

    public async Task<UnitResult<ErrorList>> Handle(DeleteSpeciesCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
        {
            return validationResult.ToErrorList();
        }

        var species = await _speciesRepository.GetById(command.SpeciesId, cancellationToken);
        if (species.IsFailure)
        {
            return Errors.General.NotFound(command.SpeciesId).ToErrorList();
        }

        if (await _volunteerContract.IsSpeciesUsed(command.SpeciesId))
        {
            return Error.Conflict("Species.is.used", $"Species {command.SpeciesId} is used now").ToErrorList();
        }

        await _speciesRepository.Delete(species.Value, cancellationToken);
        await _unitOfWork.SaveChanges(cancellationToken);

        return UnitResult.Success<ErrorList>();
    }
}
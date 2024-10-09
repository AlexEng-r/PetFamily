using CSharpFunctionalExtensions;
using FluentValidation;
using PetFamily.Application.Abstractions;
using PetFamily.Application.Database;
using PetFamily.Application.Dtos;
using PetFamily.Application.Extensions;
using PetFamily.Application.Repositories.Specieses;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.SpeciesManagement.Commands.DeleteSpecies;

public class DeleteSpeciesHandler
    : ICommandHandler<DeleteSpeciesCommand>
{
    private readonly IValidator<DeleteSpeciesCommand> _validator;
    private readonly ISpeciesRepository _speciesRepository;
    private readonly IReadDbContext _readDbContext;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteSpeciesHandler(IValidator<DeleteSpeciesCommand> validator,
        ISpeciesRepository speciesRepository,
        IReadDbContext readDbContext,
        IUnitOfWork unitOfWork)
    {
        _validator = validator;
        _speciesRepository = speciesRepository;
        _readDbContext = readDbContext;
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

        var isUsedSpecies = _readDbContext.Pets.Any(s => s.SpeciesId == command.SpeciesId);
        if (isUsedSpecies)
        {
            return Error.Conflict("Species.is.used", $"Species {command.SpeciesId} is used now").ToErrorList();
        }

        await _speciesRepository.Delete(species.Value, cancellationToken);
        await _unitOfWork.SaveChanges(cancellationToken);

        return UnitResult.Success<ErrorList>();
    }
}
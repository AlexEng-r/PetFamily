using CSharpFunctionalExtensions;
using FluentValidation;
using PetFamily.Application.Abstractions;
using PetFamily.Application.Database;
using PetFamily.Application.Extensions;
using PetFamily.Application.Repositories.Specieses;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.SpeciesManagement.Commands.DeleteBreed;

public class DeleteBreedFromSpeciesHandler
    : ICommandHandler<DeleteBreedFromSpeciesCommand>
{
    private readonly IReadDbContext _readDbContext;
    private readonly ISpeciesRepository _speciesRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<DeleteBreedFromSpeciesCommand> _validator;

    public DeleteBreedFromSpeciesHandler(IReadDbContext readDbContext,
        ISpeciesRepository speciesRepository,
        IUnitOfWork unitOfWork,
        IValidator<DeleteBreedFromSpeciesCommand> validator)
    {
        _readDbContext = readDbContext;
        _speciesRepository = speciesRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<UnitResult<ErrorList>> Handle(DeleteBreedFromSpeciesCommand command,
        CancellationToken cancellationToken)
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

        var breed = species.Value.Breeds.FirstOrDefault(x => x.Id == command.BreedId);
        if (breed == null)
        {
            return Errors.General.NotFound(command.BreedId).ToErrorList();
        }

        var isUsedBreed = _readDbContext.Pets.Any(x => x.BreedId == command.BreedId);
        if (isUsedBreed)
        {
            return Error.Conflict("Breed.is.used", $"Breed {command.BreedId} is used now").ToErrorList();
        }

        breed.Delete();

        await _unitOfWork.SaveChanges(cancellationToken);

        return UnitResult.Success<ErrorList>();
    }
}
using CSharpFunctionalExtensions;
using FluentValidation;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Extensions;
using PetFamily.SharedKernel;
using PetFamily.SpeciesManagement.Application.Repositories;
using PetFamily.VolunteerManagement.Contracts;

namespace PetFamily.SpeciesManagement.Application.Commands.DeleteBreed;

public class DeleteBreedFromSpeciesHandler
    : ICommandHandler<DeleteBreedFromSpeciesCommand>
{
    private readonly IVolunteerContract _volunteerContract;
    private readonly ISpeciesRepository _speciesRepository;
    private readonly ISpeciesUnitOfWork _unitOfWork;
    private readonly IValidator<DeleteBreedFromSpeciesCommand> _validator;

    public DeleteBreedFromSpeciesHandler(IVolunteerContract volunteerContract,
        ISpeciesRepository speciesRepository,
        ISpeciesUnitOfWork unitOfWork,
        IValidator<DeleteBreedFromSpeciesCommand> validator)
    {
        _volunteerContract = volunteerContract;
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

        if (await _volunteerContract.IsBreedUsed(command.BreedId))
        {
            return Error.Conflict("Breed.is.used", $"Breed {command.BreedId} is used now").ToErrorList();
        }

        breed.Delete();

        await _unitOfWork.SaveChanges(cancellationToken);

        return UnitResult.Success<ErrorList>();
    }
}
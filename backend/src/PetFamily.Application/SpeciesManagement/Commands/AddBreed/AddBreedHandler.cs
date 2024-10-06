using CSharpFunctionalExtensions;
using FluentValidation;
using PetFamily.Application.Abstractions;
using PetFamily.Application.Database;
using PetFamily.Application.Extensions;
using PetFamily.Application.Repositories.Specieses;
using PetFamily.Domain.Shared;
using PetFamily.Domain.SpeciesManagement.Breeds;
using PetFamily.Domain.ValueObjects.String;

namespace PetFamily.Application.SpeciesManagement.Commands.AddBreed;

public class AddBreedHandler
    : ICommandHandler<AddBreedCommand, Guid>
{
    private readonly IValidator<AddBreedCommand> _validator;
    private readonly ISpeciesRepository _speciesRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddBreedHandler(IValidator<AddBreedCommand> validator,
        ISpeciesRepository speciesRepository,
        IUnitOfWork unitOfWork)
    {
        _validator = validator;
        _speciesRepository = speciesRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid, ErrorList>> Handle(AddBreedCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
        {
            return validationResult.ToErrorList();
        }

        var species = await _speciesRepository.GetById(command.SpeciesId, cancellationToken);
        if (species.IsFailure)
        {
            return species.Error.ToErrorList();
        }

        var breedName = NotEmptyString.Create(command.BreedName).Value;

        var breedExisting = species.Value.Breeds.FirstOrDefault(x => x.Name == breedName);
        if (breedExisting != null)
        {
            return Errors.Model.AlreadyExist("Breed name already exists").ToErrorList();
        }

        var breed = new Breed(BreedId.NewBreedId(), breedName);

        species.Value.AddBreed(breed);
        await _unitOfWork.SaveChanges(cancellationToken);

        return breed.Id.Value;
    }
}
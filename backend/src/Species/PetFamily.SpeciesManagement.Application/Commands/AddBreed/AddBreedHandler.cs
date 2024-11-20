using CSharpFunctionalExtensions;
using FluentValidation;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Extensions;
using PetFamily.SharedKernel;
using PetFamily.SharedKernel.ValueObjects.Ids;
using PetFamily.SharedKernel.ValueObjects.String;
using PetFamily.SpeciesManagement.Application.Repositories;
using PetFamily.SpeciesManagement.Domain.Entities.Breeds;

namespace PetFamily.SpeciesManagement.Application.Commands.AddBreed;

public class AddBreedHandler
    : ICommandHandler<AddBreedCommand, Guid>
{
    private readonly IValidator<AddBreedCommand> _validator;
    private readonly ISpeciesRepository _speciesRepository;
    private readonly ISpeciesUnitOfWork _unitOfWork;

    public AddBreedHandler(IValidator<AddBreedCommand> validator,
        ISpeciesRepository speciesRepository,
        ISpeciesUnitOfWork unitOfWork)
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
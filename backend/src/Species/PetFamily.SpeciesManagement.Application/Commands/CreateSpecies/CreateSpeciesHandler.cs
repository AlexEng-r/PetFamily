using CSharpFunctionalExtensions;
using FluentValidation;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Extensions;
using PetFamily.SharedKernel;
using PetFamily.SharedKernel.ValueObjects.Ids;
using PetFamily.SharedKernel.ValueObjects.String;
using PetFamily.SpeciesManagement.Application.Repositories;
using PetFamily.SpeciesManagement.Domain;

namespace PetFamily.SpeciesManagement.Application.Commands.CreateSpecies;

public class CreateSpeciesHandler
    : ICommandHandler<CreateSpeciesCommand, Guid>
{
    private readonly IValidator<CreateSpeciesCommand> _validator;
    private readonly ISpeciesRepository _speciesRepository;

    public CreateSpeciesHandler(IValidator<CreateSpeciesCommand> validator,
        ISpeciesRepository speciesRepository)
    {
        _validator = validator;
        _speciesRepository = speciesRepository;
    }

    public async Task<Result<Guid, ErrorList>> Handle(CreateSpeciesCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
        {
            return validationResult.ToErrorList();
        }

        var name = NotEmptyString.Create(command.Name).Value;

        if (await _speciesRepository.AnyByName(name, cancellationToken))
        {
            return Errors.Model.AlreadyExist("Species name already exists").ToErrorList();
        }

        var speciesId = SpeciesId.NewSpeciesId();
        var species = new Species(speciesId, name);

        return await _speciesRepository.Add(species, cancellationToken);
    }
}
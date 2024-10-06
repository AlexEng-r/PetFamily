using CSharpFunctionalExtensions;
using FluentValidation;
using PetFamily.Application.Abstractions;
using PetFamily.Application.Extensions;
using PetFamily.Application.Repositories.Specieses;
using PetFamily.Domain.Shared;
using PetFamily.Domain.SpeciesManagement.Specieses;
using PetFamily.Domain.ValueObjects.String;

namespace PetFamily.Application.SpeciesManagement.Commands.Create;

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
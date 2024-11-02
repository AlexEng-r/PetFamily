using CSharpFunctionalExtensions;
using PetFamily.SharedKernel;
using PetFamily.SpeciesManagement.Application.Repositories;
using PetFamily.SpeciesManagement.Contracts;

namespace PetFamily.SpeciesManagement.Presentation;

public class SpeciesContract
    : ISpeciesContract
{
    private readonly ISpeciesRepository _speciesRepository;

    public SpeciesContract(ISpeciesRepository speciesRepository)
    {
        _speciesRepository = speciesRepository;
    }

    public Task<Result<Domain.Species, Error>> GetSpeciesById(Guid speciesId, CancellationToken cancellationToken)
        => _speciesRepository.GetById(speciesId, cancellationToken);
}
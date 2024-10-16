﻿using PetFamily.Domain.Shared.Entities.BaseDomain;
using PetFamily.Domain.Shared.Interfaces;
using PetFamily.Domain.SpeciesManagement.Breeds;
using PetFamily.Domain.ValueObjects.String;

namespace PetFamily.Domain.SpeciesManagement.Specieses;

public class Species
    : Entity<SpeciesId>, ISoftDeletable
{
    public NotEmptyString Name { get; private set; }

    private readonly List<Breed> _breeds = [];

    public IReadOnlyList<Breed> Breeds => _breeds.AsReadOnly();

    public bool IsDeleted {get; private set;}

    private Species(SpeciesId id)
        : base(id)
    {
    }

    public Species(SpeciesId id, NotEmptyString name)
        : base(id)
    {
        Name = name;
    }

    public Species AddBreed(Breed breed)
    {
        _breeds.Add(breed);

        return this;
    }

    public void Delete() => IsDeleted = true;

    public void Restore() => IsDeleted = false;
}
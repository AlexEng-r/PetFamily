﻿namespace PetFamily.Core.Dtos;

public class BreedDto
{
    public Guid Id { get; init; }

    public Guid SpeciesId { get; init; }

    public string Name { get; init; } = null!;

    public bool IsDeleted { get; set; }
}
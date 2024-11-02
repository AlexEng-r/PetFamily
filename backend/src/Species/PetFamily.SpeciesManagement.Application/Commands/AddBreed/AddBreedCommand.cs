﻿using PetFamily.Core.Abstractions;

namespace PetFamily.SpeciesManagement.Application.Commands.AddBreed;

public record AddBreedCommand(Guid SpeciesId, string BreedName)
    : ICommand;
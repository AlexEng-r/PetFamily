using PetFamily.Application.Abstractions;

namespace PetFamily.Application.VolunteerManagement.Commands.DeletePet;

public record DeletePetCommand(Guid VolunteerId, Guid PetId)
    : ICommand;
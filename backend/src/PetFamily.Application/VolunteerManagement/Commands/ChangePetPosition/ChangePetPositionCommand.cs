using PetFamily.Application.Abstractions;

namespace PetFamily.Application.VolunteerManagement.Commands.ChangePetPosition;

public record ChangePetPositionCommand(Guid VolunteerId, Guid PetId, int PetPosition)
        : ICommand;
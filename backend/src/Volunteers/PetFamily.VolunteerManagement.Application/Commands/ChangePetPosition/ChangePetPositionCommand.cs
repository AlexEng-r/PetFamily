using PetFamily.Core.Abstractions;

namespace PetFamily.VolunteerManagement.Application.Commands.ChangePetPosition;

public record ChangePetPositionCommand(Guid VolunteerId, Guid PetId, int PetPosition)
        : ICommand;
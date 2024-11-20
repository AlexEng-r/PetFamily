using PetFamily.Core.Abstractions;
using PetFamily.Core.Enums;

namespace PetFamily.VolunteerManagement.Application.Commands.UpdatePetStatus;

public record UpdatePetStatusCommand(Guid VolunteerId, Guid PetId, StatusType Status)
        : ICommand;
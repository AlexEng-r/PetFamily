using PetFamily.Application.Abstractions;
using PetFamily.Domain.VolunteerManagement.Enums;

namespace PetFamily.Application.VolunteerManagement.Commands.UpdatePetStatus;

public record UpdatePetStatusCommand(Guid VolunteerId, Guid PetId, StatusType Status)
        : ICommand;
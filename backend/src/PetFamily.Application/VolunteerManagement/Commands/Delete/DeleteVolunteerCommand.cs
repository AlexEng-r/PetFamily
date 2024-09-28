using PetFamily.Application.Abstractions;

namespace PetFamily.Application.VolunteerManagement.Commands.Delete;

public record DeleteVolunteerCommand(Guid VolunteerId)
        : ICommand;
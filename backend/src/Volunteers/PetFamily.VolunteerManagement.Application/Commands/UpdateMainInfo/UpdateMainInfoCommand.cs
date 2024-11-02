using PetFamily.Core.Abstractions;
using PetFamily.Core.Dtos;

namespace PetFamily.VolunteerManagement.Application.Commands.UpdateMainInfo;

public record UpdateMainInfoCommand(Guid VolunteerId,
    FullNameDto FullName,
    string Description,
    int Experience,
    string Phone)
        : ICommand;
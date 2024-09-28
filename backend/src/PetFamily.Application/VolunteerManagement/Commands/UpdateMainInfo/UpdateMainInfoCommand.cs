using PetFamily.Application.Abstractions;
using PetFamily.Application.Dtos;

namespace PetFamily.Application.VolunteerManagement.Commands.UpdateMainInfo;

public record UpdateMainInfoCommand(Guid VolunteerId,
    FullNameDto FullName,
    string Description,
    int Experience,
    string Phone)
        : ICommand;
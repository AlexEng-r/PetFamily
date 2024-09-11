using PetFamily.Application.Volunteers.Common;

namespace PetFamily.Application.Volunteers.UpdateMainInfo;

public record UpdateMainInfoCommand(Guid VolunteerId,
    FullNameDto FullName,
    string Description,
    int Experience,
    string Phone);
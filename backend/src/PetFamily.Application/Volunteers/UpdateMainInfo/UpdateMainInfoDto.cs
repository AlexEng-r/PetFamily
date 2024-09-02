using PetFamily.Application.Volunteers.Common;

namespace PetFamily.Application.Volunteers.UpdateMainInfo;

public record UpdateMainInfoDto(
    FullNameDto FullName,
    string Description,
    int Experience,
    string Phone);
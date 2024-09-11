using PetFamily.Application.Volunteers.Common;

namespace PetFamily.Application.Volunteers.UpdateRequisites;

public record UpdateRequisiteCommand(Guid VolunteerId, IReadOnlyList<RequisiteDto> Requisites);
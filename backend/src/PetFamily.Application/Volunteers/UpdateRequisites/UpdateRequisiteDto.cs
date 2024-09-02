using PetFamily.Application.Volunteers.Common;

namespace PetFamily.Application.Volunteers.UpdateRequisites;

public record UpdateRequisiteDto(IReadOnlyList<RequisiteDto> Requisites);
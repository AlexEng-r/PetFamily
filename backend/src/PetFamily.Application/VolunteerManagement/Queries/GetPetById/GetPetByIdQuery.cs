using PetFamily.Application.Abstractions;

namespace PetFamily.Application.VolunteerManagement.Queries.GetPetById;

public record GetPetByIdQuery(Guid PetId)
    : IQuery;
using PetFamily.Application.Abstractions;

namespace PetFamily.Application.VolunteerManagement.Queries.GetVolunteerById;

public record GetVolunteerByIdQuery(Guid VolunteerId)
    : IQuery;
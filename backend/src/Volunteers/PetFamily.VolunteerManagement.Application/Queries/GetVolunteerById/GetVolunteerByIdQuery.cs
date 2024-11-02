using PetFamily.Core.Abstractions;

namespace PetFamily.VolunteerManagement.Application.Queries.GetVolunteerById;

public record GetVolunteerByIdQuery(Guid VolunteerId)
    : IQuery;
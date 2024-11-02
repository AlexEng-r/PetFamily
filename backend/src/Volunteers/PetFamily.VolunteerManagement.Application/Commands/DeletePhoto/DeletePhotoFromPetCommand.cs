using PetFamily.Core.Abstractions;

namespace PetFamily.VolunteerManagement.Application.Commands.DeletePhoto;

public record DeletePhotoFromPetCommand(Guid VolunteerId,
    Guid PetId,
    IReadOnlyCollection<Guid> PhotoIds)
        : ICommand;
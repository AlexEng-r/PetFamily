using PetFamily.Application.Abstractions;

namespace PetFamily.Application.VolunteerManagement.Commands.DeletePhoto;

public record DeletePhotoFromPetCommand(Guid VolunteerId,
    Guid PetId,
    IReadOnlyCollection<Guid> PhotoIds)
        : ICommand;
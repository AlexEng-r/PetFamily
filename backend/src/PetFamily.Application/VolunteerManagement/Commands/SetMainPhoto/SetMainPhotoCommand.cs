using PetFamily.Application.Abstractions;

namespace PetFamily.Application.VolunteerManagement.Commands.SetMainPhoto;

public record SetMainPhotoCommand(Guid VolunteerId, Guid PetId, string PhotoPath)
    : ICommand;
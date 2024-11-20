using PetFamily.Core.Abstractions;

namespace PetFamily.VolunteerManagement.Application.Commands.SetMainPhoto;

public record SetMainPhotoCommand(Guid VolunteerId, Guid PetId, string PhotoPath)
    : ICommand;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Dtos;

namespace PetFamily.VolunteerManagement.Application.Commands.AddPetPhoto;

public record AddPetPhotoCommand(
    Guid VolunteerId,
    Guid PetId,
    string BucketName,
    IEnumerable<UploadFileDto> Files)
    : ICommand;
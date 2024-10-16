using PetFamily.Application.Dtos;
using ICommand = PetFamily.Application.Abstractions.ICommand;

namespace PetFamily.Application.VolunteerManagement.Commands.AddPetPhoto;

public record AddPetPhotoCommand(
    Guid VolunteerId,
    Guid PetId,
    string BucketName,
    IEnumerable<UploadFileDto> Files)
    : ICommand;
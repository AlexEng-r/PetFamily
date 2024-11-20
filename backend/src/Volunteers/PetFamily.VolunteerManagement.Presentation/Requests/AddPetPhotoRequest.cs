using PetFamily.Core.Dtos;
using PetFamily.VolunteerManagement.Application.Commands.AddPetPhoto;

namespace PetFamily.VolunteerManagement.Presentation.Requests;

public record AddPetPhotoRequest(string BucketName)
{
    public AddPetPhotoCommand ToCommand(Guid volunteerId, Guid petId, IEnumerable<UploadFileDto> files) =>
        new(volunteerId, petId, BucketName, files);
}
using PetFamily.Application.Dtos;
using PetFamily.Application.VolunteerManagement.Commands.AddPetPhoto;

namespace PetFamily.API.Controllers.Volunteer.Requests;

public record AddPetPhotoRequest(string BucketName)
{
    public AddPetPhotoCommand ToCommand(Guid volunteerId, Guid petId, IEnumerable<UploadFileDto> files) =>
        new(volunteerId, petId, BucketName, files);
}
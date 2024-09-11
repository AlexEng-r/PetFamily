using PetFamily.Application.Volunteers.Common;

namespace PetFamily.Application.Volunteers.AddPetPhoto;

public record AddPetPhotoCommand(Guid VolunteerId, Guid PetId, IEnumerable<UploadFileDto> Files);
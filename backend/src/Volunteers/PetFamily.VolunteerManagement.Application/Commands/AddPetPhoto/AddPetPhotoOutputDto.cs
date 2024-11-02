namespace PetFamily.VolunteerManagement.Application.Commands.AddPetPhoto;

public record AddPetPhotoOutputDto(Guid PetId, IReadOnlyList<string> FailedFiles);
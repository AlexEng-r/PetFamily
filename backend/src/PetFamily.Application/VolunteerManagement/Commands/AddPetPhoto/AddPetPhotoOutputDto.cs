namespace PetFamily.Application.VolunteerManagement.Commands.AddPetPhoto;

public record AddPetPhotoOutputDto(Guid PetId, IReadOnlyList<string> FailedFiles);
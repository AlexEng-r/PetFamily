using PetFamily.Core.Dtos;
using PetFamily.Core.Enums;
using PetFamily.VolunteerManagement.Application.Commands.UpdatePet;

namespace PetFamily.VolunteerManagement.Presentation.Requests;

public record UpdatePetRequest(
    Guid PetId,
    string NickName,
    string AnimalType,
    string? Description,
    Guid? SpeciesId,
    Guid? BreedId,
    string Color,
    string? HealthInformation,
    AddressDto AddressDto,
    double? Weight,
    double? Height,
    string Phone,
    bool IsSterialized,
    DateTime? Birthday,
    bool IsVaccinated,
    StatusType Status,
    IReadOnlyList<RequisiteDto> Requisites)
{
    public UpdatePetCommand ToCommand(
        Guid volunteerId) => new(volunteerId,
        PetId,
        NickName,
        AnimalType,
        Description,
        SpeciesId,
        BreedId,
        Color,
        HealthInformation,
        AddressDto,
        Weight,
        Height,
        Phone,
        IsSterialized,
        Birthday,
        IsVaccinated,
        Status,
        Requisites
    );
}
using PetFamily.Application.Dtos;
using PetFamily.Application.VolunteerManagement.Commands.AddPets;
using PetFamily.Application.VolunteerManagement.Commands.UpdatePet;
using PetFamily.Domain.VolunteerManagement.Enums;

namespace PetFamily.API.Controllers.Volunteer.Requests;

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
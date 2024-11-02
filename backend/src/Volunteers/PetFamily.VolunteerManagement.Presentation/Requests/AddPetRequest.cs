using PetFamily.Core.Dtos;
using PetFamily.VolunteerManagement.Application.Commands.AddPets;
using PetFamily.VolunteerManagement.Domain.Enums;

namespace PetFamily.VolunteerManagement.Presentation.Requests;

public record AddPetRequest(
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
    public AddPetCommand ToCommand(Guid volunteerId)
        => new(
            volunteerId,
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
            Requisites);
}
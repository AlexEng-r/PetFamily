using PetFamily.Application.Volunteers.AddPets;
using PetFamily.Application.Volunteers.Common;
using PetFamily.Domain.VolunteerManagement.Enums;

namespace PetFamily.API.Controllers.Volunteer.Requests;

public record AddPetRequest(
    string NickName,
    string AnimalType,
    string? Description,
    string? Breed,
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
            Breed,
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
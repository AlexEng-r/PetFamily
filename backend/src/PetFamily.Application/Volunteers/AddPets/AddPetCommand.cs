using PetFamily.Application.Volunteers.Common;
using PetFamily.Domain.VolunteerManagement.Enums;

namespace PetFamily.Application.Volunteers.AddPets;

public record AddPetCommand(
    Guid VolunteerId,
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
    IReadOnlyList<RequisiteDto> Requisites);

public record AddressDto(string City, string House, string Flat);
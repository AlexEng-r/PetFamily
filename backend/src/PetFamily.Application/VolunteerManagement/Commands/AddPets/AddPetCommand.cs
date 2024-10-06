using PetFamily.Application.Abstractions;
using PetFamily.Application.Dtos;
using PetFamily.Domain.VolunteerManagement.Enums;

namespace PetFamily.Application.VolunteerManagement.Commands.AddPets;

public record AddPetCommand(
    Guid VolunteerId,
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
        : ICommand;

public record AddressDto(string City, string House, string Flat);
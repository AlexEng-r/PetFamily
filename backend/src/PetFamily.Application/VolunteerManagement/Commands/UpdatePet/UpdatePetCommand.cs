using PetFamily.Application.Abstractions;
using PetFamily.Application.Dtos;
using PetFamily.Application.VolunteerManagement.Commands.AddPets;
using PetFamily.Domain.VolunteerManagement.Enums;

namespace PetFamily.Application.VolunteerManagement.Commands.UpdatePet;

public record UpdatePetCommand(Guid VolunteerId,
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
    : ICommand;
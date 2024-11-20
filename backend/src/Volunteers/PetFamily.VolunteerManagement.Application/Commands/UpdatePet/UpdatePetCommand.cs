using PetFamily.Core.Abstractions;
using PetFamily.Core.Dtos;
using PetFamily.Core.Enums;

namespace PetFamily.VolunteerManagement.Application.Commands.UpdatePet;

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
using PetFamily.Application.Abstractions;
using PetFamily.Application.Dtos;

namespace PetFamily.Application.VolunteerManagement.Commands.UpdateRequisites;

public record UpdateRequisiteCommand(Guid VolunteerId, IReadOnlyList<RequisiteDto> Requisites)
    : ICommand;
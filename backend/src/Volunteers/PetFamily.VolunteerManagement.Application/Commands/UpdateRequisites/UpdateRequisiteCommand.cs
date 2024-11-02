using PetFamily.Core.Abstractions;
using PetFamily.Core.Dtos;

namespace PetFamily.VolunteerManagement.Application.Commands.UpdateRequisites;

public record UpdateRequisiteCommand(Guid VolunteerId, IReadOnlyList<RequisiteDto> Requisites)
    : ICommand;
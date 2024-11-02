using PetFamily.Core.Dtos;

namespace PetFamily.VolunteerManagement.Application.Dtos;

public class VolunteerDto
{
    public Guid Id { get; set; }

    public FullNameDto FullName { get; set; }

    public string Description { get; set; }

    public int Experience { get; set; }

    public string Phone { get; set; }

    public IReadOnlyList<SocialNetworksDto> SocialNetworks { get; set; }

    public IReadOnlyList<RequisiteDto> Requisites { get; set; }

    public bool IsDeleted { get; set; }
}
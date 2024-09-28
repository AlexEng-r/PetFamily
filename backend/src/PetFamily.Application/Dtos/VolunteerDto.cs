namespace PetFamily.Application.Dtos;

public class VolunteerDto
{
    public Guid Id { get; private set; }

    public FullNameDto FullName { get; private set; }

    public string Description { get; private set; } = string.Empty;

    public int Experience { get; private set; }

    public string Phone { get; private set; } = string.Empty;

    public IReadOnlyList<SocialNetworksDto> SocialNetworks { get; private set; }

    public IReadOnlyList<RequisiteDto> Requisites { get; private set; }
}
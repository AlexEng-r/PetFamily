namespace PetFamily.Domain.Requisites;

public record RequisiteDetails()
{
    public List<Requisite> Requisites { get; private set; }
}
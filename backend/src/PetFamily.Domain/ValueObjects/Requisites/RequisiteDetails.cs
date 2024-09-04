namespace PetFamily.Domain.ValueObjects.Requisites;

public record RequisiteDetails
{
    public IReadOnlyList<Requisite> Requisites { get; }

    private RequisiteDetails()
    {
    }

    public RequisiteDetails(IEnumerable<Requisite> requisites)
    {
        Requisites = requisites.ToList();
    }
}
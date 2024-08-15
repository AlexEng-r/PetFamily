namespace PetFamily.Domain.Requisites;

public record RequisiteDetails
{
    public IReadOnlyList<Requisite> Requisites { get; }

    private RequisiteDetails()
    {
    }

    private RequisiteDetails(IReadOnlyList<Requisite> requisites)
    {
        Requisites = requisites;
    }
}
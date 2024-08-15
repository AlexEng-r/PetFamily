namespace PetFamily.Domain.Addresses;

public record Address
{
    public string City { get; }

    public string House { get; }

    public string Flat { get; }

    public Address(string city, string house, string flat)
    {
        City = city;
        House = house;
        Flat = flat;
    }
}
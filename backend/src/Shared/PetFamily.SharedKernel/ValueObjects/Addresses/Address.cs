using CSharpFunctionalExtensions;

namespace PetFamily.SharedKernel.ValueObjects.Addresses;

public record Address
{
    public string City { get; }

    public string House { get; }

    public string Flat { get; }

    private Address(string city, string house, string flat)
    {
        City = city;
        House = house;
        Flat = flat;
    }

    public static Result<Address, Error> Create(string city, string house, string flat)
    {
        if (string.IsNullOrWhiteSpace(city) || city.Length > ConfigurationConstraint.MIN20_TEXT_LENGTH)
        {
            return Errors.General.ValueIsInvalid("City");
        }

        if (string.IsNullOrWhiteSpace(house) || house.Length > ConfigurationConstraint.MIN20_TEXT_LENGTH)
        {
            return Errors.General.ValueIsInvalid("House");
        }

        if (string.IsNullOrWhiteSpace(flat) || flat.Length > ConfigurationConstraint.MIN20_TEXT_LENGTH)
        {
            return Errors.General.ValueIsInvalid("Flat");
        }

        return new Address(city, house, flat);
    }
}
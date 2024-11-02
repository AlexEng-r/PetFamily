/*using PetFamily.Domain.SpeciesManagement.Breeds;
using PetFamily.Domain.SpeciesManagement.Specieses;
using PetFamily.Domain.ValueObjects.String;

namespace PetFamily.Base.Test.Builder;

public static class SpeciesBuilder
{
    public static Species GetSpeciesWithBreeds(int breedCount)
    {
        var speciesName = NotEmptyString.Create("species").Value;

        var species = new Species(SpeciesId.NewSpeciesId(), speciesName);

        for (int i = 0; i < breedCount; i++)
        {
            var breed = new Breed(BreedId.NewBreedId(), NotEmptyString.Create("breed").Value);
            species.AddBreed(breed);
        }

        return species;
    }
}*/
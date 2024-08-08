using System.ComponentModel.DataAnnotations;

namespace PetFamily.Domain.Aggregates.Pets;

public enum AnimalType
{
    [Display(Name = "Кот")]
    Cat = 1,

    [Display(Name = "Собака")]
    Dog
}
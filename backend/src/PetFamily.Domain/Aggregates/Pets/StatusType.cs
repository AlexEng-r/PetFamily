using System.ComponentModel.DataAnnotations;

namespace PetFamily.Domain.Aggregates.Pets;

public enum StatusType
{
    [Display(Name = "Нуждается в помощи")]
    NeedHelp = 1,

    [Display(Name = "Ищет дом")]
    LookingForAHome,

    [Display(Name = "Нашел дом")]
    FoundAHome
}
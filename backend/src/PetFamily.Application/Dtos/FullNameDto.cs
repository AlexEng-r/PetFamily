namespace PetFamily.Application.Dtos;

public class FullNameDto
{
    public string FirstName {get; set;}
    public string Surname {get; set;}
    public string Patronymic {get; set;}

    public FullNameDto()
    {
        
    }

    public FullNameDto(string firstName, string surname, string patronymic)
    {
        FirstName = firstName;
        Surname = surname;
        Patronymic = patronymic;
    }
}
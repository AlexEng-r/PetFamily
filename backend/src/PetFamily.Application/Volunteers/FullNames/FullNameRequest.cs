namespace PetFamily.Application.Volunteers.FullNames;

public record FullNameRequest(
    string FirstName,
    string Surname,
    string Patronymic);
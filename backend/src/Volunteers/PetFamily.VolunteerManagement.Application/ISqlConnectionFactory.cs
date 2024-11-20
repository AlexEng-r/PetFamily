using System.Data;

namespace PetFamily.VolunteerManagement.Application;

public interface ISqlConnectionFactory
{
    IDbConnection Create();
}
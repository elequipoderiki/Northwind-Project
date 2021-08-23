using System.Data.SqlClient;
using Dapper;
using Northwind.Models;
using Northwind.Repositories;

namespace Northwind.DataAccess
{
  public class UserRepository : Repository<User>, IUserRepository
  {
    public UserRepository(string connectionString) : base(connectionString)
    {
    }

    public User ValidateUser(string email, string password)
    {
      var parameters = new DynamicParameters();
      parameters.Add("@email",email);
      parameters.Add("@password", password);
     
      using (var connection = new SqlConnection(_connectionString))
      {
        var query = "dbo.ValidateUser";
        return connection.QueryFirstOrDefault<User>(
          query, parameters, commandType:System.Data.CommandType.StoredProcedure);
      }
    }
  }
}
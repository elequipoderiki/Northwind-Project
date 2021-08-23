using System.Collections.Generic;
using System.Linq;
using Dapper;
using Northwind.Models;
using Northwind.Repositories;
using System.Data.SqlClient;

namespace Northwind.DataAccess
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(string connectionString):base(connectionString)
        {

        }

        public IEnumerable<CustomerList> CustomerPagedList(int page, int rows)
        {
          var parameters = new DynamicParameters();
          parameters.Add("@page", page);
          parameters.Add("@rows", rows);

          //Uso de stored procedure customerpagedlist
          using(var connection = new SqlConnection(_connectionString))
          {
            var query = "dbo.CustomerPagedList";
            return connection.Query< CustomerList>(query, parameters,commandType:System.Data.CommandType.StoredProcedure);
          }
        }
    }
}
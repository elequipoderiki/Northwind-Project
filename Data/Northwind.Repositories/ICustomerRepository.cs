using System.Collections.Generic;
using System.Linq;
using Northwind.Models;

namespace Northwind.Repositories
{
    public interface ICustomerRepository : IRepository<Customer>
    {
         IEnumerable<CustomerList> CustomerPagedList(int page, int rows);
    }
}
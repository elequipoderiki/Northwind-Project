using System.Collections.Generic;
using Northwind.Models;

namespace Northwind.BussinessLogic.Interfaces
{
    public interface ISupplierLogic
    {
         
         
         Supplier GetById(int id);
         IEnumerable<Supplier> SupplierPagedList(int page, int rows, string searchTerm);
          int Insert(Supplier supplier);
          bool Update(Supplier supplier);
          bool Delete(Supplier supplier);
    }
}
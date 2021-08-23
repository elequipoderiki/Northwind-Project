using Northwind.Repositories;

namespace Northwind.UnitOfWork
{
    public interface IUnitOfWork
    {
      //note no setter for interfaces
      ICustomerRepository Customer { get; }        
      IUserRepository User {get; }
      ISupplierRepository Supplier {get;}
      IOrderRepository Order { get; }

  }
}
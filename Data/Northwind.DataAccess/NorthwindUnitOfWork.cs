using Northwind.Repositories;
using Northwind.UnitOfWork;

namespace Northwind.DataAccess
{
  public class NorthwindUnitOfWork : IUnitOfWork
  {
    //al crearse una instancia de NorthwindUnitOfWork
    //se inyecta la cadena de conexion de la base de datos
    //en una instancia de CustomerRepository
    //al crearse como singleton NorthwindUnitOfWork se garantiza
    //que CustomerRepository pueda usar la cadena de conexion 
    //lo mismo para los demas repositorios 
    public NorthwindUnitOfWork(string connectionString)
    {
        Customer = new CustomerRepository(connectionString);
        User = new UserRepository(connectionString);
        Supplier = new SupplierRepository(connectionString);
        Order = new OrderRepository(connectionString);
    }
    public ICustomerRepository Customer { get; private set;}
    public IUserRepository  User { get; private set; }
    public ISupplierRepository Supplier {get; private set;}
    public IOrderRepository Order { get;private set; }
  }
}
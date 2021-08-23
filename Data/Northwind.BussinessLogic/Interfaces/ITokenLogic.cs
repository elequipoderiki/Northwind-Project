using Northwind.Models;

namespace Northwind.BussinessLogic.Interfaces
{
    public interface ITokenLogic
    {
         
         User ValidateUser(string email, string password);
    }
}
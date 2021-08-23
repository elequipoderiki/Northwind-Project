using System.Collections.Generic;
using Northwind.BussinessLogic.Implementations;
using Northwind.Models;

namespace Northwind.BussinessLogic.Interfaces
{
    public interface IOrderLogic
    {
    IEnumerable<OrderList> getPaginatedOrder(int page, int rows);
    OrderList GetOrderById(int orderId);
    bool Delete(Order entity);
    Order GetById(int orderId);

    string GetOrderNumber(int orderId);
  }
}
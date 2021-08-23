using System.Collections.Generic;
using Northwind.Models;

namespace Northwind.Repositories
{
    public interface IOrderRepository: IRepository<Order>
    {
        IEnumerable<OrderList> getPaginatedOrder(int page, int rows);
        OrderList GetOrderById(int orderId);
    }
}
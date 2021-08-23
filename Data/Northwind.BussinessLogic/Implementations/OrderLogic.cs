using System.Collections.Generic;
using System.Linq;
using Northwind.BussinessLogic.Interfaces;
using Northwind.Models;
using Northwind.UnitOfWork;

namespace Northwind.BussinessLogic.Implementations
{
  public class OrderLogic : IOrderLogic
  {
    private readonly IUnitOfWork _unitOfWork;

    public OrderLogic(IUnitOfWork unitOfWork)
    {
      _unitOfWork = unitOfWork;
    }
    public bool Delete(Order entity)
    {
      return _unitOfWork.Order.Delete(entity);
    }

    public OrderList GetOrderById(int orderId)
    {
      return _unitOfWork.Order.GetOrderById(orderId);
    }

    public IEnumerable<OrderList> getPaginatedOrder(int page, int rows)
    {
      return _unitOfWork.Order.getPaginatedOrder(page, rows);
    }

    public Order GetById(int orderId)
    {
      return _unitOfWork.Order.GetById(orderId);
    }

    public string GetOrderNumber(int orderId)
    {
      var list = _unitOfWork.Order.GetList();
      if(list == null) return  string.Empty;
      var record = list.FirstOrDefault(x => x.Id == orderId);
      return record.OrderNumber;
    }
  }
}
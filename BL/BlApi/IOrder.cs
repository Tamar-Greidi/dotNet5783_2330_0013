using System.Collections.Generic;
using BO;

namespace BlApi;

public interface IOrder      ///interface of Order
{
    public IEnumerable<OrderForList?> Get();
    public Order GetDetails(int orderID);
    public BO.Order UpdateShipping(int orderID);
    public BO.Order UpdateDelivery(int orderID);
    //берес
    public void Update(int orderID);
}
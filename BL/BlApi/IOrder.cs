using System.Collections.Generic;
using BO;

namespace BlApi;

/// <summary>
/// Order's interface.
/// </summary>
public interface IOrder
{
    public IEnumerable<OrderForList?> Get();
    public Order GetDetails(int orderID);
    public Order UpdateShipping(int orderID);
    public Order UpdateDelivery(int orderID);
    public OrderTracking OrderTracking(int orderID);
    //берес
    public void Update(int orderID);
    public int OrderSelection();
}
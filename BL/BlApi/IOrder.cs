using System.Collections.Generic;
using BO;

namespace BlApi;

public interface IOrder
{
    public IEnumerable<OrderForList> Get();
    public Order GetDetails(int orderID);
    public BO.Order UpdateShipping(int orderID);
    public BO.Order UpdateDelivery(int orderID);
    //�����
    public void Update(int orderID);
}
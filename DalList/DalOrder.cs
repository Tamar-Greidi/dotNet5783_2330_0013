using DO;
using static Dal.DataSource;
using DalApi;
using Microsoft.VisualBasic;

namespace Dal;

/// <summary>
/// DalOrder class.
/// </summary>

public class DalOrder: IOrder
{
    public int Add(Order order)
    {
        
        if (orderQuantity > _arrOrder.Count())
        {
            order.ID = Config.OrderID++;
            _arrOrder.Add(order);
        }   
        else
            throw new ArrayIsFull();
        //try
        //{

        //    for (int i = 0; i < _arrOrder.Count(); i++)
        //    {
        //        if (_arrOrder[i].ID == order.ID)
        //        {
        //            throw new ObjectAlreadyExists();
        //        }
        //    }
        //}
        //catch (ObjectAlreadyExists ex)
        //{
        //    throw ex;
        //}
        //_arrOrder.Add(order);
        return order.ID;
    }

    public Order Get(int orderID)
    {
        for (int i = 0; i < _arrOrder.Count(); i++)
        {
            if (_arrOrder[i].ID == orderID)
            {
                return _arrOrder[i];
            }
        }
        try
        {
            throw new ObjectNotFound();
        }
        catch (ObjectNotFound ex)
        {
            throw ex;
        }
    }

    public IEnumerable<Order> GetAll()
    {
        List<Order> _OrdersShow = new();
        foreach (Order order in _arrOrder)
        {
            _OrdersShow.Add(order);
        }
        return _OrdersShow;
    }

    public int Update(Order order)
    {
        for (int i = 0; i < _arrOrder.Count; i++)
        {
            if (_arrOrder[i].ID == order.ID)
            {
                _arrOrder[i] = order;
                return _arrOrder[i].ID;
            }
        }
        try
        {
            throw new ObjectNotFound();
        }
        catch (ObjectNotFound ex)
        {
            throw ex;
        }
    }

    public void Delete(int orderIndex)
    {
        for (int i = 0; i < _arrOrder.Count(); i++)
        {
            if (_arrOrder[i].ID == orderIndex)
            {
                _arrOrder.RemoveAt(i);
                return;
            }
        }
        try
        {
            throw new ObjectNotFound();
        }
        catch (ObjectNotFound ex)
        {
            throw ex;
        }
    }
}
using DO;
using static Dal.DataSource;
using DalApi;
using System;

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
        return order.ID;
    }

    public Order Get(int orderID)
    {
        Order order = _arrOrder.Find(item => item.ID == orderID);
        if (order.ID == orderID)
            return order;
        throw new ObjectNotFound();
    }

    public Order Get(Predicate<Order> func)
    {
        Order order = _arrOrder.Find(func);
        if (order.ID == 0)
            throw new ObjectNotFound();
        return order;
    }

    public IEnumerable<Order> GetAll(Func<Order, bool>? func = null)
    {
        return func == null ? _arrOrder : _arrOrder.Where(func);
    }

    public int Update(Order order)
    {
        Order item =_arrOrder.Find(item => item.ID == order.ID);
        int itemIndex = _arrOrder.IndexOf(item);
        _arrOrder[itemIndex] = order;  
        return item.ID;
        //for (int i = 0; i < _arrOrder.Count; i++)
        //{
        //    if (_arrOrder[i].ID == order.ID)
        //    {
        //        _arrOrder[i] = order;
        //        return _arrOrder[i].ID;
        //    }
        //}
        //throw new ObjectNotFound();
    }

    public void Delete(int orderID)
    {
        Order item = _arrOrder.Find(item => item.ID == orderID);
        _arrOrder.Remove(item);
        return;
        //for (int i = 0; i < _arrOrder.Count(); i++)
        //{
        //    if (_arrOrder[i].ID == orderID)
        //    {
        //        _arrOrder.RemoveAt(i);
        //        return;
        //    }
        //}
        //throw new ObjectNotFound();
    }
}
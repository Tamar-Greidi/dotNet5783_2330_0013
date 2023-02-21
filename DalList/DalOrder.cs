using DO;
using static Dal.DataSource;
using DalApi;
using System;
using System.Runtime.CompilerServices;

namespace Dal;

/// <summary>
/// DalOrder class.
/// </summary>

public class DalOrder: IOrder
{
    [MethodImpl(MethodImplOptions.Synchronized)]
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

    [MethodImpl(MethodImplOptions.Synchronized)]
    public Order Get(int orderID)
    {
        Order order = _arrOrder.Find(item => item.ID == orderID);
        if (order.ID == orderID)
            return order;
        throw new ObjectNotFound();
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public Order Get(Predicate<Order> func)
    {
        Order order = _arrOrder.Find(func);
        if (order.ID == 0)
            throw new ObjectNotFound();
        return order;
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<Order> GetAll(Func<Order, bool>? func = null)
    {
        return func == null ? _arrOrder : _arrOrder.Where(func);
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public int Update(Order order)
    {
        Order item =_arrOrder.Find(item => item.ID == order.ID);
        int itemIndex = _arrOrder.IndexOf(item);
        _arrOrder[itemIndex] = order;  
        return item.ID;
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(int orderID)
    {
        Order item = _arrOrder.Find(item => item.ID == orderID);
        _arrOrder.Remove(item);
        return;
    }
}
using System;
using DO;
using static Dal.DataSource;
using DalApi;
using System.Collections.Generic;

namespace Dal;

/// <summary>
/// DalOrderItem class.
/// </summary>

public class DalOrderItem : IOrderItem
{
    public int Add(OrderItem orderItem)
    {
        OrderItem item = _arrOrderItem.Find(item => item.OrderID == orderItem.OrderID && item.ProductID == orderItem.ProductID);
        if (item.ID > 0)
            throw new ObjectAlreadyExists();
        Product product = _arrProduct.Find(item => item.ID == orderItem.ProductID);
        if (product.ID > 0)
        {
            orderItem.ID = Config.OrderItemID++;
            orderItem.Price = product.Price;
            _arrOrderItem.Add(orderItem);
            return orderItem.ID;
        }
        throw new ObjectNotFound();
    }

    public OrderItem Get(int orderItemID)
    {
        OrderItem item = _arrOrderItem.Find(item => item.ID == orderItemID);
        if(item.ID == 0)
            throw new ObjectNotFound();
        return item;
    }

    public OrderItem Get(Predicate<OrderItem> func)
    {
        OrderItem orderItem = _arrOrderItem.Find(func);
        if (orderItem.ID == 0)
            throw new ObjectNotFound();
        return orderItem;
    }

    public IEnumerable<OrderItem> GetAll(Func<OrderItem, bool>? func = null) => func == null ? _arrOrderItem : _arrOrderItem.Where(func);

    public int Update(OrderItem orderItem)
    {
        OrderItem item = _arrOrderItem.Find(item => item.ID == orderItem.ID);
        int itemIndex = _arrOrderItem.IndexOf(item);
        _arrOrderItem[itemIndex] = orderItem;
        return item.ID;
    }

    public void Delete(int orderItemID)
    {
        OrderItem item = _arrOrderItem.Find(item => item.ID == orderItemID);
        _arrOrderItem.Remove(item);
        return;
    }
}

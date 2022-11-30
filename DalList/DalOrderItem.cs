using System;
using DO;
using static Dal.DataSource;
using DalApi;

namespace Dal;

public class DalOrderItem : IOrderItem
{
    public int Add(OrderItem orderItem)
    {
        try
        {
            Get(orderItem.ID);
        }
        catch (Exception)
        {
            if (_arrOrderItem.Count() < _arrOrderItem.Count)
            {
                _arrOrderItem.Add(orderItem);
            }
            else
            {
                throw new ArrayIsFull();
            }
            return orderItem.ID;
        }
        throw new ObjectAlreadyExists();
    }

    public OrderItem Get(int orderItemID)
    {
        for (int i = 0; i < _arrOrderItem.Count(); i++)
        {
            if (_arrOrderItem[i].ID == orderItemID)
            {
                return _arrOrderItem[i];
            }
        }
        throw new ObjectNotFound();
    }

    public IEnumerable<OrderItem> GetAll()
    {
        if (_arrOrderItem.Count() == 0)
        {
            throw new ObjectNotFound();
        }
        List<OrderItem> _showOrderItems = new List<OrderItem>();
        for (int i = 0; i < _arrOrderItem.Count(); i++)
        {
            DO.OrderItem tempOrderItem = new();
            tempOrderItem.ID = _arrOrderItem[i].ID;
            _showOrderItems.Add(tempOrderItem);
        }
        return _showOrderItems;
    }

    public IEnumerable<OrderItem> GetProductsByOrder(int orderID)
    {
        try
        {
            new DalOrder().Get(orderID);
        }
        catch (Exception)
        {
            throw new ObjectNotFound();
        }
        List<OrderItem> _productsByOrder = new List<OrderItem>();
        for (int i = 0, j = 0; i < _arrOrderItem.Count(); i++)
        {
            if (_arrOrderItem[i].OrderID == orderID)
            {
                DO.OrderItem tempOrderItem = new();
                tempOrderItem.ID = _arrOrderItem[i].ID;
                _productsByOrder.Add(tempOrderItem);
            }
        }
        return _productsByOrder;
    }

    public OrderItem GetProductsByOrderAndProduct(int orderID, int productID)
    {
        try
        {
            new DalOrder().Get(orderID);
        }
        catch (Exception)
        {
            throw new ObjectNotFound();
        }
        try
        {
            new DalProduct().Get(productID);
        }
        catch (Exception)
        {
            throw new ObjectNotFound();
        }
        for (int i = 0; i < _arrOrderItem.Count(); i++)
            if (_arrOrderItem[i].OrderID == orderID && _arrOrderItem[i].ProductID == productID)
                return _arrOrderItem[i];
        throw new ObjectNotFound();
    }

    public int Update(OrderItem orderItem)
    {
        for (int i = 0; i < _arrOrderItem.Count(); i++)
        {
            if (_arrOrderItem[i].ID == orderItem.ID)
            {
                _arrOrderItem[i] = orderItem;
            }
        }
        throw new ObjectNotFound();
    }

    public void Delete(int orderItemID)
    {
        for (int i = 0; i < _arrOrderItem.Count(); i++)
        {
            if (_arrOrderItem[i].ID == orderItemID)
            {
                Delete(_arrOrderItem[i].ID);
                return;
            }
        }
        throw new ObjectNotFound();
    }
}

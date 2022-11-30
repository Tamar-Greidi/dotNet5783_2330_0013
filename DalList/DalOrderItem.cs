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
            if (Config.IndexOrderItem < _arrOrderItem.Count)
            {
                _arrOrderItem[Config.IndexOrderItem++] = orderItem;
            }
            else
            {
                throw new Exception("the array is full.");
            }
            return orderItem.ID;
        }
        throw new Exception("ID is already exsist.");
    }

    public OrderItem Get(int orderItemID)
    {
        for (int i = 0; i < Config.IndexOrderItem; i++)
        {
            if (_arrOrderItem[i].ID == orderItemID)
            {
                return _arrOrderItem[i];
            }
        }
        throw new Exception("ID isnt exsist");
    }

    public IEnumerable<OrderItem> GetAll()
    {
        if (Config.IndexOrderItem == 0)
        {
            throw new Exception("the orderItems dont exist yet");
        }
        List<OrderItem> _showOrderItems = new List<OrderItem>();
        for (int i = 0; i < Config.IndexOrderItem; i++)
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
        for (int i = 0, j = 0; i < Config.IndexOrderItem; i++)
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
            throw new Exception("Order is not found");
        }
        try
        {
            new DalProduct().Get(productID);
        }
        catch (Exception)
        {
            throw new Exception("Product is not found");
        }
        for (int i = 0; i < Config.IndexOrderItem; i++)
            if (_arrOrderItem[i].OrderID == orderID && _arrOrderItem[i].ProductID == productID)
                return _arrOrderItem[i];
        throw new Exception("Product is not found");
    }

    public int Update(OrderItem orderItem)
    {
        for (int i = 0; i < Config.IndexOrderItem; i++)
        {
            if (_arrOrderItem[i].ID == orderItem.ID)
            {
                _arrOrderItem[i] = orderItem;
            }
        }
        throw new Exception("orderItem is not exsist");
    }

    public void Delete(int orderItemID)
    {
        for (int i = 0; i < Config.IndexOrderItem; i++)
        {
            if (_arrOrderItem[i].ID == orderItemID)
            {
                Delete(_arrOrderItem[i].ID);
                return;
            }
        }
        throw new Exception("orderItem is not exsist");
    }
}

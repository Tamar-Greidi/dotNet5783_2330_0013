using System;
using DO;
using static Dal.DataSource;
using DalApi;

namespace Dal;

/// <summary>
/// DalOrderItem class.
/// </summary>

public class DalOrderItem : IOrderItem
{
    public int Add(OrderItem orderItem)
    {
        try
        {
            for (int i = 0; i < _arrOrderItem.Count(); i++)
            {
                if (_arrOrderItem[i].ID == orderItem.ID)
                {
                    throw new ObjectAlreadyExists();
                }
            }
        }
        catch (ObjectAlreadyExists ex)
        {
            throw ex;
        }
        foreach (Product item in _arrProduct)
        {
            if(item.ID == orderItem.ProductID)
            {
                orderItem.Price = item.Price;
                _arrOrderItem.Add(orderItem);
                return orderItem.ID;
            }
        }
        try
        {
            throw new ObjectNotFound();
        }
        catch(Exception ex)
        {
            throw ex;
        }
    }

    public OrderItem Get(int orderItemID)
    {
        for (int i = 0; i < _arrOrderItem.Count(); i++)
            if (_arrOrderItem[i].ID == orderItemID)
                return _arrOrderItem[i];
        try
        {
            throw new ObjectNotFound();
        }
        catch (ObjectNotFound ex)
        {
            throw ex;
        }
    }

    public IEnumerable<OrderItem> GetAll()
    {
        List<OrderItem> _showOrderItems = new();
        foreach (OrderItem orderItem in _arrOrderItem)
        {
            _showOrderItems.Add(orderItem);
        }
        return _showOrderItems;
    }

    public IEnumerable<OrderItem> GetProductsByOrder(int orderID)
    {
        //List<OrderItem> _productsByOrder = new List<OrderItem>();
        List<OrderItem> _productsByOrder = _arrOrderItem.FindAll(x => x.OrderID == orderID);
        return _productsByOrder;
        //for (int i = 0; i < _arrOrderItem.Count(); i++)
        //{
        //    if (_arrOrderItem[i].OrderID == orderID)
        //    {
        //        _productsByOrder.Add(_arrOrderItem[i]);
        //        return _productsByOrder;
        //    }
        //}
        try
        {
            throw new ObjectNotFound();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public OrderItem GetProductsByOrderAndProduct(int orderID, int productID)
    {
        OrderItem productsByOrderAndProduct = _arrOrderItem.Find(x => x.OrderID == orderID && x.ProductID == productID);
        return productsByOrderAndProduct;

        //for (int i = 0; i < _arrOrderItem.Count(); i++)
        //    if (_arrOrderItem[i].OrderID == orderID && _arrOrderItem[i].ProductID == productID)
        //        return _arrOrderItem[i];
        try
        {
            throw new ObjectNotFound();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public int Update(OrderItem orderItem)
    {
        for (int i = 0; i < _arrOrderItem.Count; i++)
        {
            if (_arrOrderItem[i].ID == orderItem.ID && _arrOrderItem[i].ProductID == orderItem.ProductID && _arrOrderItem[i].OrderID == orderItem.OrderID)
            {
                orderItem.Price = _arrOrderItem[i].Price;
                _arrOrderItem[i] = orderItem;
                return _arrOrderItem[i].ID;
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

    public void Delete(int orderItemID)
    {
        foreach (OrderItem item in _arrOrderItem)
        {
            if(item.ID == orderItemID)
            {
                _arrOrderItem.RemoveAll(x => x.ID == orderItemID);
                return;
            }
        }
        try
        {
            throw new ObjectNotFound();
        }
        catch(ObjectNotFound ex)
        {
            throw ex;
        }
    }
}

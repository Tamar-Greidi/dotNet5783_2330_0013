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
            Order curOrder=new Order();

            OrderItem item = _arrOrderItem.Find(item => item.ID == orderItem.ID);
            if (item.ID > 0)
                throw new ObjectAlreadyExists();
        }
        catch (ObjectAlreadyExists ex)
        {
            throw ex;
        }
        //Order order = _arrOrder.Find(x => x.ID == orderItem.OrderID);
        //order.ID > 0 &&
        Product product = _arrProduct.Find(x => x.ID == orderItem.ProductID);
        if (product.ID > 0)
        {
            orderItem.Price = product.Price;
            _arrOrderItem.Add(orderItem);
            return orderItem.ID;
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

    public OrderItem Get(int orderItemID, Predicate<OrderItem> func)
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

    public IEnumerable<OrderItem> GetAll(Func<OrderItem, bool>? func = null)
    {
        List<OrderItem> _showOrderItems = new();
        foreach (OrderItem orderItem in _arrOrderItem)
        {
            _showOrderItems.Add(orderItem);
        }
        return _showOrderItems;
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

    //public IEnumerable<OrderItem> GetProductsByOrder(int orderID)
    //{
    //    Order order = _arrOrder.Find(order => order.ID == orderID);
    //    if (order.ID == 0)
    //    {
    //        try
    //        {
    //            throw new ObjectNotFound();
    //        }
    //        catch (ObjectNotFound ex)
    //        {
    //            throw ex;
    //        }
    //    }
    //    List<OrderItem> _productsByOrder = _arrOrderItem.FindAll(x => x.OrderID == orderID);
    //    return _productsByOrder;
    //    try
    //    {
    //        throw new ObjectNotFound();
    //    }
    //    catch (ObjectNotFound ex)
    //    {
    //        throw ex;
    //    }
    //}

    ///public OrderItem GetProductByOrderAndProduct(int orderID, int productID)
    //{
    //    Order order = _arrOrder.Find(order => order.ID == orderID);
    //    Product product = _arrProduct.Find(product => product.ID == productID);
    //    if (order.ID == 0 || product.ID == 0)
    //    {
    //        try
    //        {
    //            throw new ObjectNotFound();
    //        }
    //        catch (ObjectNotFound ex)
    //        {
    //            throw ex;
    //        }
    //    }
    //    OrderItem productByOrderAndProduct = _arrOrderItem.Find(item => item.OrderID == orderID && item.ProductID == productID);
    //    if (productByOrderAndProduct.ID == 0)
    //    {
    //        try
    //        {
    //            throw new ObjectNotFound();
    //        }
    //        catch (ObjectNotFound ex)
    //        {
    //            throw ex;
    //        }
    //    }
    //    return productByOrderAndProduct;
    //}
}

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
        //OrderItem o = _arrOrderItem.Find(item=> item.OrderID==orderItem.OrderID && item.ProductID==orderItem.ProductID);
        //if (o.ID > 0)
        //{
        //    OrderItem or = new OrderItem();
        //    or = o;
        //    or.Amount += orderItem.Amount;
        //    Update(or);
        //}
        //else
        //{
        //}
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
        for (int i = 0; i < _arrOrderItem.Count(); i++)
            if (_arrOrderItem[i].ID == orderItemID)
                return _arrOrderItem[i];
        throw new ObjectNotFound();
    }

    public OrderItem Get(Predicate<OrderItem> func)
    {
        OrderItem orderItem = _arrOrderItem.Find(func);
        if (orderItem.ID == 0)
            throw new ObjectNotFound();
        return orderItem;
    }

    public IEnumerable<OrderItem> GetAll(Func<OrderItem, bool>? func = null)
    {
        return func == null ? _arrOrderItem : _arrOrderItem.Where(func);
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
        throw new ObjectNotFound();
    }

    public void Delete(int orderItemID)
    {
        foreach (OrderItem item in _arrOrderItem)
        {
            if (item.ID == orderItemID)
            {
                _arrOrderItem.RemoveAll(x => x.ID == orderItemID);
                return;
            }
        }
        throw new ObjectNotFound();
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

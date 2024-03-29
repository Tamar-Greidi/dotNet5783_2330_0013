using BO;
using DalApi;
using DO;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;

namespace BlImplementation;

/// <summary>
/// Implementation of the Order class.
/// </summary>
internal class BlOrder : BlApi.IOrder
{
    private IDal? Dal = DalApi.Factory.Get();

    /// <summary>
    /// Order list request.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="BO.DalException"></exception>
    /// <exception cref="BO.InvalidData"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<BO.OrderForList> Get()
    {
        int status;
        IEnumerable<DO.Order> orders = Dal?.Order.GetAll() ?? throw new BO.Null();
        List<BO.OrderForList> newOrders = new List<BO.OrderForList>();
        //Requesting all order details for this order
        foreach (var order in orders)
        {
            BO.OrderForList newOrder = new BO.OrderForList();
            IEnumerable<DO.OrderItem> orderItems = new List<DO.OrderItem>();
            try
            {
                orderItems = Dal.OrderItem.GetAll(item => item.OrderID == order.ID);
                double totalPrice = 0;
                //is calculating total price
                foreach (var item in orderItems)
                    totalPrice += item.Price * item.Amount;
                //Creating a status according to the dates
                if (order.DeliveryDate != DateTime.MinValue)
                    status = 2;
                else if (order.ShipDate != DateTime.MinValue)
                    status = 1;
                else
                    status = 0;
                newOrder.ID = order.ID;
                newOrder.CustomerName = order.CustomerName;
                newOrder.AmountOfItems = orderItems.Count();
                newOrder.Status = (BO.OrderStatus)status;
                newOrder.TotalPrice = totalPrice;

                newOrders.Add(newOrder);
            }
            catch (DalApi.ObjectNotFound)
            {
                newOrders.Add(newOrder);
            }
        }
        return newOrders.OrderBy(item => item.ID);
    }

    /// <summary>
    /// Order details request.
    /// </summary>
    /// <param name="orderID"></param>
    /// <returns></returns>
    /// <exception cref="BO.IncorrectDate"></exception>
    /// <exception cref="BO.DalException"></exception>
    /// <exception cref="BO.InvalidData"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public BO.Order GetDetails(int orderID)
    {
        if (orderID > 0)
        {
            try
            {
                DO.Order orderByID = Dal?.Order.Get(orderID) ?? throw new BO.Null();
                IEnumerable<DO.OrderItem> orderItems = Dal.OrderItem.GetAll(item => item.OrderID == orderID);
                List<BO.OrderItem> returnOrderItems = new List<BO.OrderItem>();
                double ordertotalPrice = 0;
                foreach (var item in orderItems)
                {
                    DO.Product product = Dal.Product.Get(item.ProductID);//get a product by id-product
                    ordertotalPrice += item.Price * item.Amount;
                    returnOrderItems.Add(new BO.OrderItem
                    {
                        ID = 0,
                        Name = product.Name,
                        ProductID = item.ProductID,
                        Price = item.Price,
                        Amount = item.Amount,
                        TotalPrice = item.Price * item.Amount
                    });//create BO.orderItem
                }
                int status;
                if (orderByID.DeliveryDate != DateTime.MinValue)
                    status = 2;
                else if (orderByID.ShipDate != DateTime.MinValue)
                    status = 1;
                else
                    status = 0;
                BO.Order order = new BO.Order
                {
                    ID = orderByID.ID,
                    CustomerName = orderByID.CustomerName,
                    CustomerEmail = orderByID.CustomerEmail,
                    CustomerAddress = orderByID.CustomerAddress,
                    OrderDate = orderByID.OrderDate,
                    Status = (BO.OrderStatus)status,
                    ShipDate = orderByID.ShipDate,
                    DeliveryDate = orderByID.DeliveryDate,
                    Items = returnOrderItems,
                    TotalPrice = ordertotalPrice,
                };//create BO.Order
                return order;
            }
            catch (DalApi.ObjectNotFound ex)
            {
                throw new BO.DalException(ex);
            }
        }
        else
            throw new BO.InvalidData();
    }

    /// <summary>
    /// Order shipping update.
    /// </summary>
    /// <param name="orderID"></param>
    /// <returns></returns>
    /// <exception cref="BO.OrderAlreadyShipped"></exception>
    /// <exception cref="BO.DalException"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public BO.Order UpdateShipping(int orderID)
    {
        try
        {
            DO.Order order = Dal?.Order.Get(orderID) ?? throw new BO.Null();
            if (order.ShipDate.CompareTo(DateTime.Now) < 0 && order.ShipDate.CompareTo(DateTime.MinValue) != 0)
                throw new BO.OrderAlreadyShipped();
            order.ShipDate = DateTime.Now;
            BO.Order BoOrder = new()
            {
                ID = orderID,
                CustomerName = order.CustomerName,
                CustomerEmail = order.CustomerEmail,
                CustomerAddress = order.CustomerAddress,
                OrderDate = order.OrderDate,
                Status = BO.OrderStatus.Shipped,
                ShipDate = DateTime.Now,
                DeliveryDate = order.DeliveryDate
            };
            IEnumerable<DO.OrderItem> orderItems = Dal.OrderItem.GetAll(item => item.OrderID == orderID);
            List<BO.OrderItem> TempOrderItems = new();
            BO.OrderItem TempOrderItem = new();
            foreach (DO.OrderItem item in orderItems)
            {
                TempOrderItem.ID = item.ID;
                TempOrderItem.Name = Dal.Product.Get(item.ProductID).Name;
                TempOrderItem.ProductID = item.ProductID;
                TempOrderItem.Price = Dal.Product.Get(item.ProductID).Price;
                TempOrderItem.Amount = item.Amount;
                TempOrderItem.TotalPrice = TempOrderItem.Amount * TempOrderItem.Price;

                TempOrderItems.Add(TempOrderItem);
            }
            BoOrder.Items = TempOrderItems;
            Dal.Order.Update(order);
            return BoOrder;
        }
        catch (Exception ex)
        {
            throw new BO.DalException(ex);
        }
    }

    /// <summary>
    /// Order delivery update.
    /// </summary>
    /// <param name="orderID"></param>
    /// <returns></returns>
    /// <exception cref="BO.OrderAlreadyDelivered"></exception>
    /// <exception cref="BO.DalException"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public BO.Order UpdateDelivery(int orderID)
    {
        try
        {
            DO.Order order = Dal?.Order.Get(orderID) ?? throw new BO.Null();
            if (order.ShipDate.CompareTo(DateTime.MinValue) == 0)
                throw new BO.OrderNotShippedYet();
            if (order.DeliveryDate.CompareTo(DateTime.Now) < 0 && order.DeliveryDate.CompareTo(DateTime.MinValue) != 0)
                throw new BO.OrderAlreadyDelivered();
            order.DeliveryDate = DateTime.Now;
            BO.Order BoOrder = new()
            {
                ID = orderID,
                CustomerName = order.CustomerName,
                CustomerEmail = order.CustomerEmail,
                CustomerAddress = order.CustomerAddress,
                OrderDate = order.OrderDate,
                ShipDate = order.ShipDate,
                DeliveryDate = DateTime.Now
            };
            IEnumerable<DO.OrderItem> orderItems = Dal.OrderItem.GetAll(item => item.OrderID == orderID);
            List<BO.OrderItem> TempOrderItems = new();
            BO.OrderItem TempOrderItem = new();
            foreach (DO.OrderItem item in orderItems)
            {
                TempOrderItem.ID = item.ID;
                TempOrderItem.Name = Dal.Product.Get(item.ProductID).Name;
                TempOrderItem.ProductID = item.ProductID;
                TempOrderItem.Price = Dal.Product.Get(item.ProductID).Price;
                TempOrderItem.Amount = item.Amount;
                TempOrderItem.TotalPrice = TempOrderItem.Amount * TempOrderItem.Price;

                TempOrderItems.Add(TempOrderItem);
            }

            BoOrder.Items = TempOrderItems;
            Dal.Order.Update(order);
            return BoOrder;
        }

        catch (Exception ex)
        {
            throw new BO.DalException(ex);
        }
    }

    /// <summary>
    /// Order Tracking.
    /// </summary>
    /// <param name="orderID"></param>
    /// <returns></returns>
    /// <exception cref="BO.IncorrectDate"></exception>
    /// <exception cref="BO.ObjectNotFound"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public OrderTracking OrderTracking(int orderID)
    {
        DO.Order order = Dal?.Order.Get(orderID) ?? throw new BO.Null();
        List<(DateTime, string)> tracking = new();
        if (order.ID == orderID)
        {
            int status;
            tracking.Add((order.OrderDate, "The order has been confirmed"));
            if (order.DeliveryDate != DateTime.MinValue)
            {
                status = 2;
                tracking.Add((order.ShipDate, "The order has been shipped"));
                tracking.Add((order.DeliveryDate, "The order has been delivered"));
            }
            else if (order.ShipDate != DateTime.MinValue)
            {
                status = 1;
                tracking.Add((order.ShipDate, "The order has been shipped"));
            }
            else
                status = 0;
            BO.OrderTracking orderTracking = new BO.OrderTracking
            {
                ID = orderID,
                Status = (BO.OrderStatus)status,
                Tracking = tracking
            };
            return orderTracking;
        }
        throw new BO.ObjectNotFound();
    }

    /// <summary>
    /// Bonus: Update an order.
    /// </summary>
    /// <param name="orderID"></param>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Update(int orderID)
    {
        //???��� ����� ���� ����� ���� ���� ������
    }

    /// <summary>
    /// Selecting the next order to handle.
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public int? OrderSelection()
    {
        //IEnumerable<DO.Order>? orders = Dal?.Order.GetAll().Where(item => item.OrderDate != DateTime.MinValue)
        //    .OrderBy(item => item.DeliveryDate == DateTime.MinValue ? item.ShipDate : item.DeliveryDate);
        //return orders?.First().ID;
        IEnumerable<DO.Order>? orders = Dal?.Order.GetAll().Where(item => item.DeliveryDate == DateTime.MinValue)
          .OrderBy(item => item.ShipDate != DateTime.MinValue ? item.ShipDate : item.OrderDate);
        try
        {
            return orders?.First().ID;
        }

        catch (Exception ex)
        {
            return null;
        };
    }


    

}
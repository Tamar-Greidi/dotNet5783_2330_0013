//using DalApi;

using BO;
using DalApi;
using DO;

namespace BlImplementation;

/// <summary>
/// Implementation of the Order class.
/// </summary>
internal class BlOrder : BlApi.IOrder
{
    private IDal Dal = DalApi.Factory.Get();

    /// <summary>
    /// Order list request.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="BO.DalException"></exception>
    /// <exception cref="BO.InvalidData"></exception>
    public IEnumerable<BO.OrderForList> Get()
    {
        int status;
        //try
        //{
        IEnumerable<DO.Order> orders = Dal.Order.GetAll();
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
                if (order.OrderDate.CompareTo(DateTime.Now) == 0 || DateTime.Now.CompareTo(order.OrderDate) > 0)
                    status = 0;
                else if (order.ShipDate != DateTime.MinValue && (DateTime.Now.CompareTo(order.ShipDate) == 0 || DateTime.Now.CompareTo(order.ShipDate) > 0))
                    status = 1;
                else if (order.DeliveryDate != DateTime.MinValue && (DateTime.Now.CompareTo(order.DeliveryDate) == 0 || DateTime.Now.CompareTo(order.DeliveryDate) > 0))
                    status = 2;
                else
                    throw new BO.InvalidData();
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
        return newOrders;
        //}
        //catch (DalApi.ObjectNotFound ex)
        //{
        //    throw new BO.DalException(ex);
        //} 
    }

    /// <summary>
    /// Order details request.
    /// </summary>
    /// <param name="orderID"></param>
    /// <returns></returns>
    /// <exception cref="BO.IncorrectDate"></exception>
    /// <exception cref="BO.DalException"></exception>
    /// <exception cref="BO.InvalidData"></exception>
    public BO.Order GetDetails(int orderID)
    {
        if (orderID > 0)
        {
            try
            {
                DO.Order orderByID = Dal.Order.Get(orderID);
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
                if (orderByID.OrderDate.CompareTo(DateTime.Now) == 0 || DateTime.Now.CompareTo(orderByID.OrderDate) > 0)
                    status = 0;
                else if (orderByID.ShipDate != DateTime.MinValue && (DateTime.Now.CompareTo(orderByID.ShipDate) == 0 || DateTime.Now.CompareTo(orderByID.ShipDate) > 0))
                    status = 1;
                else if (orderByID.DeliveryDate != DateTime.MinValue && (DateTime.Now.CompareTo(orderByID.DeliveryDate) == 0 || DateTime.Now.CompareTo(orderByID.DeliveryDate) > 0))
                    status = 2;
                else
                    throw new BO.IncorrectDate();

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
    public BO.Order UpdateShipping(int orderID)
    {
        try
        {
            DO.Order order = Dal.Order.Get(orderID);
            if (order.ShipDate.CompareTo(DateTime.Now) < 0)
                throw new BO.OrderAlreadyShipped();
            //order.ShipDate = DateTime.Now;
            BO.Order BoOrder = new()
            {
                ID = orderID,
                CustomerName = order.CustomerName,
                CustomerEmail = order.CustomerEmail,
                CustomerAddress = order.CustomerAddress,
                OrderDate = order.OrderDate,
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
    public BO.Order UpdateDelivery(int orderID)
    {
        try
        {
            // orderItem = new();
            DO.Order order = Dal.Order.Get(orderID);
            //BO.Order BoOrder = new();
            if (order.DeliveryDate.CompareTo(DateTime.Now) < 0 && order.DeliveryDate.CompareTo(DateTime.MinValue) != 0)
                throw new BO.OrderAlreadyDelivered();
            //order.DeliveryDate = 
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
    public OrderTracking OrderTracking(int orderID)
    {
        DO.Order order = Dal.Order.Get(orderID);
        if (order.ID == orderID)
        {
            int status;
            if (order.OrderDate.CompareTo(DateTime.Now) == 0 || DateTime.Now.CompareTo(order.OrderDate) > 0)
                status = 0;
            else if (order.ShipDate != DateTime.MinValue && (DateTime.Now.CompareTo(order.ShipDate) == 0 || DateTime.Now.CompareTo(order.ShipDate) > 0))
                status = 1;
            else if (order.DeliveryDate != DateTime.MinValue && (DateTime.Now.CompareTo(order.DeliveryDate) == 0 || DateTime.Now.CompareTo(order.DeliveryDate) > 0))
                status = 2;
            else
                throw new BO.IncorrectDate();
            BO.OrderTracking eee = new BO.OrderTracking
            {
                ID = orderID,
                Status = (BO.OrderStatus)status
            };
            return eee;
        }
        throw new BO.ObjectNotFound();
    }
    /// <summary>
    /// Bonus: Update an order.
    /// </summary>
    /// <param name="orderID"></param>
    public void Update(int orderID)
    {
        //???למה שמנהל יוכל לשנות כמות מוצר בהזמנה
    }
}
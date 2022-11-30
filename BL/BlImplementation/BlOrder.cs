
using Dal;
using DalApi;

namespace BlImplementation;

internal class BlOrder : BlApi.IOrder
{
    DalApi.IDal Dal = new Dal.DalList();
    public IEnumerable<BO.OrderForList> Get()
    {
        int status;
        IEnumerable<DO.Order> orders = Dal.Order.GetAll();
        List<BO.OrderForList> newOrders = new List<BO.OrderForList>();
        //Requesting all order details for this order
        foreach (var order in orders)
        {
            IEnumerable<DO.OrderItem> orderItems = new List<DO.OrderItem>();
            try
            {
                orderItems = Dal.OrderItem.GetProductsByOrder(order.ID);
            }
            catch (DalApi.ObjectNotFound ex)
            {
                throw new BO.DalException(ex);
            }
            double totalPrice = 0;
            //is calculating total price
            foreach (var item in orderItems)
                totalPrice += (item.Price * item.Amount);
            //Creating a status according to the dates
            if (order.OrderDate.CompareTo(DateTime.Now) == 0 || DateTime.Now.CompareTo(order.OrderDate) > 0)
                status = 0;
            else if (order.ShipDate != DateTime.MinValue && (DateTime.Now.CompareTo(order.ShipDate) == 0 || DateTime.Now.CompareTo(order.ShipDate) > 0))
                status = 1;
            else if (order.DeliveryDate != DateTime.MinValue && (DateTime.Now.CompareTo(order.DeliveryDate) == 0 || DateTime.Now.CompareTo(order.DeliveryDate) > 0))
                status = 2;
            else
                throw new BO.InvalidData();
            BO.OrderForList newOrder = new BO.OrderForList
            {
                ID = 0,
                CustomerName = order.CustomerName,
                AmountOfItems = orderItems.Count(),
                Status = (BO.OrderStatus)status,
                TotalPrice = totalPrice
            };
            newOrders.Add(newOrder);
        }
        return newOrders;
    }
    public BO.Order GetDetails(int orderID)
    {
        if (orderID > 0)
        {
            try
            {
                DO.Order orderByID = Dal.Order.Get(orderID);
                IEnumerable<DO.OrderItem> orderItems = Dal.OrderItem.GetProductsByOrder(orderID);
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

    public BO.Order UpdateShipping(int orderID)
    {
        //BO.Order b = new();
        //try
        //{
        //    DO.Order order = Dal.Order.Get(orderID);
        //    if (order.ShipDate == DateTime.MinValue || order.ShipDate.CompareTo(DateTime.Now) > 0)
        //    {
        //        order.ShipDate = DateTime.Now;
        //    }
        //}
        //catch
        //{
        //    throw;
        //}
        //return b;

        try
        {
            IEnumerable<DO.OrderItem> orderItem = new List<DO.OrderItem>();
            DO.Order order = new();
            BO.Order BoOrder = new();
            order = Dal.Order.Get(orderID);
            if (order.ShipDate.CompareTo(DateTime.Now) < 0)
                throw new BO.OrderAlreadyShipped();
            order.ShipDate = DateTime.Now;
            orderItem = Dal.OrderItem.GetProductsByOrder(orderID);
            BoOrder.ID = orderID;
            BoOrder.CustomerName = order.CustomerName;
            BoOrder.CustomerEmail = order.CustomerEmail;
            BoOrder.CustomerAddress = order.CustomerAddress;
            BoOrder.OrderDate = order.OrderDate;
            BoOrder.ShipDate = order.ShipDate;
            BoOrder.DeliveryDate = order.DeliveryDate;
            List<BO.OrderItem> tmpOrderItem = new();
            for (int i = 0; i < orderItem.Count(); i++)
            {
                BO.OrderItem orderItemTemp = new();
                orderItemTemp.ID = orderItem.ElementAt(i).ID;
                orderItemTemp.Name = Dal.Product.Get(orderItem.ElementAt(i).ID).Name;
                orderItemTemp.ProductID = orderItem.ElementAt(i).ProductID;
                orderItemTemp.Price = Dal.Product.Get(orderItem.ElementAt(i).ID).Price;
                orderItemTemp.Amount = orderItem.ElementAt(i).Amount;
                orderItemTemp.TotalPrice = orderItemTemp.Amount * orderItemTemp.Price;

                tmpOrderItem.Add(orderItemTemp);
            }
            BoOrder.Items = tmpOrderItem;
            Dal.Order.Update(order);
            return BoOrder;
        }
        catch (Exception ex)
        {
            throw new BO.DalException(ex);
        }
    }

    public BO.Order UpdateDelivery(int orderID)
    {
        try
        {
            List<DO.OrderItem> orderItem = new();
            DO.Order order = new();
            BO.Order BoOrder = new();

            order = Dal.Order.Get(orderID);
            if (order.DeliveryDate.CompareTo(DateTime.Now) < 0 && order.DeliveryDate.CompareTo(DateTime.MinValue) != 0)
            {
                throw new BO.OrderAlreadyDelivered();
            }
            order.DeliveryDate = DateTime.Now;
            orderItem = (List<DO.OrderItem>)Dal.OrderItem.GetProductsByOrder(orderID);
            BoOrder.CustomerName = order.CustomerName;
            BoOrder.CustomerEmail = order.CustomerEmail;
            BoOrder.ID = orderID;
            BoOrder.CustomerAddress = order.CustomerAddress;
            BoOrder.OrderDate = order.OrderDate;
            BoOrder.ShipDate = order.ShipDate;
            BoOrder.DeliveryDate = order.DeliveryDate;
            List<BO.OrderItem> tmpOrderItem = new();
            for (int i = 0; i < orderItem.Count; i++)
            {
                BO.OrderItem orderItemTemp = new();
                orderItemTemp.Amount = orderItem[i].Amount;
                orderItemTemp.Name = Dal.Product.Get(orderItem[i].ID).Name;
                orderItemTemp.ProductID = orderItem[i].ProductID;
                orderItemTemp.ID = orderItem[i].ID;
                orderItemTemp.Price = Dal.Product.Get(orderItem[i].ID).Price;
                orderItemTemp.TotalPrice = orderItemTemp.Amount * orderItemTemp.Price;
            }
            BoOrder.Items = tmpOrderItem;
            Dal.Order.Update(order);
            return BoOrder;
        }
        
        catch (Exception ex)
        {
            throw new BO.DalException(ex);
        }
}
    //�����
    public void Update(int orderID)
    {

    }
}
using DalApi;
using DO;
using System;
//using DO;
using System.Linq;
using System.Xml.Linq;

namespace Dal;

internal class Order : IOrder
{
    public int Add(DO.Order order)
    {
        XDocument doc = XDocument.Load(@"..\xml\Order.xml");
        XElement root = new XElement("Order");
        XDocument config = XDocument.Load(@"..\xml\Config.xml");
        int ID = Convert.ToInt32(config.Element("Order")?.Value) + 1;
        config.Element("Order").Value = ID.ToString();
        config?.Save(@"..\xml\Config.xml");
        root.Add(new XElement("ID", ID));
        root.Add(new XElement("CustomerName", order.CustomerName));
        root.Add(new XElement("CustomerEmail", order.CustomerEmail));
        root.Add(new XElement("CustomerAddress", order.CustomerAddress));
        root.Add(new XElement("OrderDate", order.OrderDate));
        root.Add(new XElement("ShipDate", order.ShipDate));
        root.Add(new XElement("DeliveryDate", order.DeliveryDate));
        doc.Element("Orders")?.Add(root);
        doc.Save(@"..\xml\Order.xml");
        return order.ID;
    }

    public DO.Order Get(int orderID)
    {
        XDocument doc = XDocument.Load(@"..\xml\Order.xml");
        var xmlOrders = doc.Descendants("Order");
        XElement? xOrder = xmlOrders.ToList().Find(item => Convert.ToInt32(item.Element("ID")?.Value) == orderID);
        if (Convert.ToInt32(xOrder?.Element("ID")?.Value) == 0)
            throw new ObjectNotFound();
        DO.Order order = new DO.Order()  
        {
            ID = Convert.ToInt32(xOrder?.Element("ID")?.Value),
            CustomerName = xOrder?.Element("CustomerName")?.Value,
            CustomerEmail = xOrder?.Element("CustomerEmail")?.Value,
            CustomerAddress = xOrder?.Element("CustomerAddress")?.Value,
            OrderDate = Convert.ToDateTime(xOrder?.Element("OrderDate")?.Value),
            ShipDate = Convert.ToDateTime(xOrder?.Element("ShipDate")?.Value),
            DeliveryDate = Convert.ToDateTime(xOrder?.Element("DeliveryDate")?.Value),
        };
        return order;
    }

    public DO.Order Get(Predicate<DO.Order> func)
    {
        //XDocument doc = XDocument.Load(@"..\xml\Order.xml");
        //var xmlOrders = doc.Descendants("Order");
        //XElement? xOrder = xmlOrders.ToList()?.Find(func);
        //if (Convert.ToInt32(xOrder?.Element("ID")?.Value) == 0)
        //    throw new ObjectNotFound();
        //DO.Order order = new DO.Order()
        //{
        //    ID = Convert.ToInt32(xOrder?.Element("ID")?.Value),
        //    CustomerName = xOrder?.Element("CustomerName")?.Value,
        //    CustomerEmail = xOrder?.Element("CustomerEmail")?.Value,
        //    CustomerAddress = xOrder?.Element("CustomerAddress")?.Value,
        //    OrderDate = Convert.ToDateTime(xOrder?.Element("OrderDate")?.Value),
        //    ShipDate = Convert.ToDateTime(xOrder?.Element("ShipDate")?.Value),
        //    DeliveryDate = Convert.ToDateTime(xOrder?.Element("DeliveryDate")?.Value),
        //};
        //return order;

        IEnumerable<DO.Order> orders = GetAll();
        return orders .ToList().Find(func);
    }

    public IEnumerable<DO.Order> GetAll(Func<DO.Order, bool>? func = null)
    {
        XDocument doc = XDocument.Load(@"..\xml\Order.xml");
        var xmlOrders = doc.Descendants("Order");
        List<DO.Order> orders = new List<DO.Order>();
        DO.Order order = new DO.Order();
        xmlOrders.ToList().ForEach(item =>
        {
            order.ID = Convert.ToInt32(item.Element("CustomerName")?.Value);
            order.CustomerName = item.Element("CustomerName")?.Value;
            order.CustomerEmail = item.Element("CustomerEmail")?.Value;
            order.CustomerAddress = item.Element("CustomerAddress")?.Value;
            order.OrderDate = Convert.ToDateTime(item?.Element("OrderDate")?.Value);
            order.ShipDate = Convert.ToDateTime(item?.Element("ShipDate")?.Value);
            order.DeliveryDate = Convert.ToDateTime(item?.Element("DeliveryDate")?.Value);
            orders.Add(order);
        });
        return func == null ? orders : orders.Where(func);

        //XDocument doc = XDocument.Load(@"..\xml\Order.xml");
        //var xmlOrders = doc.Descendants("Order");
        //List<DO.Order> orders = new List<DO.Order>();
        //DO.Order order = new DO.Order();
        //int ID;
        //DateTime OrderDate, ShipDate, DeliveryDate;
        //xmlOrders.ToList().ForEach(item =>
        //{
        //    int.TryParse(item.Element("ID")?.Value, out int ID);
        //    order.ID = ID;
        //    order.CustomerName = item.Element("CustomerName")?.Value;
        //    order.CustomerEmail = item.Element("CustomerEmail")?.Value;
        //    order.CustomerAddress = item.Element("CustomerAddress")?.Value;
        //    DateTime.TryParse(item?.Element("OrderDate")?.Value, out DateTime OrderDate);
        //    order.OrderDate = OrderDate;
        //    DateTime.TryParse(item?.Element("ShipDate")?.Value, out DateTime ShipDate);
        //    order.ShipDate = ShipDate;
        //    DateTime.TryParse(item?.Element("DeliveryDate")?.Value, out DateTime DeliveryDate);
        //    order.DeliveryDate = DeliveryDate;
        //    orders.Add(order);
        //});
        //return func == null ? orders : orders.Where(func);
    }

    public int Update(DO.Order order)
    {
        XDocument doc = XDocument.Load(@"..\xml\Order.xml");
        var xmlOrders = doc.Descendants("Order");
        XElement? xElement = xmlOrders.ToList().Find(item => Convert.ToInt32(item.Element("ID")?.Value) == order.ID);
        xElement?.SetValue(order);
        doc.Save(@"..\xml\Order.xml");
        return Convert.ToInt32(xElement?.Element("ID")?.Value);
    }

    public void Delete(int orderID)
    {
        XDocument doc = XDocument.Load(@"../xml/Order.xml");
        var xmlOrders = doc.Descendants("Order");
        xmlOrders.ToList().Find(item => Convert.ToInt32(item.Element("ID")?.Value) == orderID)?.Remove();
        doc.Save(@"..\xml\Order.xml");
    }
}

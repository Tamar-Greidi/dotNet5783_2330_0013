using DalApi;
using DO;
using System;
using System.Linq;
using System.Xml.Linq;

namespace Dal;

internal class Order : IOrder
{
    public int Add(DO.Order order)
    {
        XDocument doc = XDocument.Load(@"../Order.xml");
        XElement root = new XElement("Order");
        if (order.ID == 0)
        {
            XElement? config = XDocument.Load(@"../Config.xml").Root;
            int ID = Convert.ToInt32(config?.Element("Order")?.Value) + 1;
            config.Element("Order").Value = ID.ToString();
            config?.Save(@"../Config.xml");
            root.Add(new XElement("ID", ID.ToString()));
        }
        else 
            root.Add(new XElement("ID", order.ID));
        root.Add(new XElement("CustomerName", order.CustomerName));
        root.Add(new XElement("CustomerEmail", order.CustomerEmail));
        root.Add(new XElement("CustomerAddress", order.CustomerAddress));
        root.Add(new XElement("OrderDate", order.OrderDate));
        root.Add(new XElement("ShipDate", order.ShipDate));
        root.Add(new XElement("DeliveryDate", order.DeliveryDate));
        doc.Element("Orders")?.Add(root);
        doc.Save(@"../Order.xml");
        return order.ID;
    }

    public DO.Order Get(int orderID)
    {
        XDocument doc = XDocument.Load(@"../Order.xml");
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
        IEnumerable<DO.Order> orders = GetAll();
        return orders .ToList().Find(func);
    }

    public IEnumerable<DO.Order> GetAll(Func<DO.Order, bool>? func = null)
    {
        XDocument doc = XDocument.Load(@"../Order.xml");
        var xmlOrders = doc.Descendants("Order");
        List<DO.Order> orders = new List<DO.Order>();
        DO.Order order = new DO.Order();
        xmlOrders.ToList().ForEach(item =>
        {
            order.ID = Convert.ToInt32(item.Element("ID")?.Value);
            order.CustomerName = item.Element("CustomerName")?.Value;
            order.CustomerEmail = item.Element("CustomerEmail")?.Value;
            order.CustomerAddress = item.Element("CustomerAddress")?.Value;
            order.OrderDate = Convert.ToDateTime(item?.Element("OrderDate")?.Value);
            order.ShipDate = Convert.ToDateTime(item?.Element("ShipDate")?.Value);
            order.DeliveryDate = Convert.ToDateTime(item?.Element("DeliveryDate")?.Value);
            orders.Add(order);
        });
        return func == null ? orders : orders.Where(func);
    }

    public int Update(DO.Order order)
    {
        Delete(order.ID);
        return Add(order);
    }

    public void Delete(int orderID)
    {
        XDocument doc = XDocument.Load(@"../Order.xml");
        var xmlOrders = doc.Descendants("Order");
        xmlOrders.ToList().Find(item => Convert.ToInt32(item.Element("ID")?.Value) == orderID)?.Remove();
        doc.Save(@"../Order.xml");
    }
}

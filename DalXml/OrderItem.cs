using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using DalApi;
using DO;

namespace Dal;

internal class OrderItem : IOrderItem
{
    public int Add(DO.OrderItem orderItem)
    {
        List<DO.OrderItem> orderItems = GetAll().ToList();
        DO.OrderItem findOrderItem = orderItems.Find(item => item.ID == orderItem.ID);
        if (findOrderItem.ID != 0)
            throw new ObjectAlreadyExists();
        else
        {
            XElement? config = XDocument.Load(@"../Config.xml").Root;
            int ID = Convert.ToInt32(config?.Element("OrderItem")?.Value) + 1;
            config.Element("OrderItem").Value = ID.ToString();
            config?.Save(@"../Config.xml");
            orderItem.ID = ID;
            orderItems.Add(orderItem);
        }
        StreamWriter write = new StreamWriter("../OrderItem.xml");
        XmlSerializer ser = new XmlSerializer(typeof(List<DO.OrderItem>));
        ser.Serialize(write, orderItems);
        write.Close();
        return orderItem.ID;
    }

    public DO.OrderItem Get(int OrderItemID)
    {
        IEnumerable<DO.OrderItem> orderItems = GetAll();
        DO.OrderItem orderItem = orderItems.ToList().Find(item => item.ID == OrderItemID);
        if (orderItem.ID == 0)
            throw new ObjectNotFound();
        return orderItem;
    }

    public DO.OrderItem Get(Predicate<DO.OrderItem> func)
    {
        IEnumerable<DO.OrderItem> orderItems = GetAll();
        return orderItems.ToList().Find(func);
    }

    public IEnumerable<DO.OrderItem> GetAll(Func<DO.OrderItem, bool>? func = null)
    {
        XmlSerializer ser = new XmlSerializer(typeof(List<DO.OrderItem>));
        StreamReader read = new StreamReader("../OrderItem.xml");
        IEnumerable<DO.OrderItem>? orderItems = (IEnumerable<DO.OrderItem>?)ser.Deserialize(read);
        read.Close();
        return func == null ? orderItems : orderItems?.ToList().Where(func);
    }

    public int Update(DO.OrderItem orderItem)
    {
        try
        {
            Delete(orderItem.ID);
        }
        catch (ObjectNotFound ex)
        {
            throw ex;
        }
        List<DO.OrderItem> orderItems = GetAll().ToList();

        orderItems.Add(orderItem);
        StreamWriter write = new StreamWriter("../OrderItem.xml");
        XmlSerializer ser = new XmlSerializer(typeof(List<DO.OrderItem>));
        ser.Serialize(write, orderItems);
        write.Close();
        return orderItem.ID;
    }
    public void Delete(int orderItemID)
    {
        List<DO.OrderItem> products = GetAll().ToList();
        products.Remove(Get(orderItemID));
        StreamWriter write = new StreamWriter("../OrderItem.xml");
        XmlSerializer ser = new XmlSerializer(typeof(List<DO.Product>));
        ser.Serialize(write, products);
        write.Close();
    }
}

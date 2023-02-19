using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using DalApi;
using DO;

namespace Dal;

internal class Product:IProduct
{
    public int Add(DO.Product product)
    {
        List<DO.Product> products = GetAll().ToList();
        DO.Product pp = products.Find(item => item.ID == product.ID);
        if (pp.ID == product.ID)
            throw new ObjectAlreadyExists();
        else
        {
            //לטפל ב config
            products.Add(product);
        }
            
        //List<DO.Product> lst = new List<DO.Product>();
        
        StreamWriter write = new StreamWriter("../../../../Product.xml");
        XmlSerializer ser = new XmlSerializer(typeof(List<DO.Product>));
        ser.Serialize(write, products);
        write.Close();
        return product.ID;
    }

    public DO.Product Get(int productID)
    {
        IEnumerable<DO.Product> products = GetAll();
        DO.Product product = products.ToList().Find(item => item.ID == productID);
        if(product.ID == 0) 
            throw new ObjectNotFound();
        return product;
    }

    public DO.Product Get(Predicate<DO.Product> func)
    {
        IEnumerable<DO.Product> products = GetAll();
        return products.ToList().Find(func);
    }

    public IEnumerable<DO.Product> GetAll(Func<DO.Product, bool>? func = null)
    {
        XmlSerializer ser = new XmlSerializer(typeof(List<DO.Product>));
        StreamReader read = new StreamReader("../../../../Product.xml");
        IEnumerable<DO.Product>? lstProduct = (IEnumerable<DO.Product>?)ser.Deserialize(read);
        read.Close();
        return func == null ? lstProduct : lstProduct?.ToList().Where(func);
    }

    public int Update(DO.Product product)
    {
        Delete(product.ID);
        return Add(product);
    }
    public void Delete(int productID)
    {
        List<DO.Product> products = GetAll().ToList(); 
        products.Remove(Get(productID));
        StreamWriter write = new StreamWriter("../../../../Product.xml");
        XmlSerializer ser = new XmlSerializer(typeof(List<DO.Product>));
        ser.Serialize(write, products);
        write.Close();
    }
}
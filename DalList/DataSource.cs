using DalApi;
using DO;
using System;
using System.Text.RegularExpressions;

namespace Dal;

/// <summary>
/// DataSource class.
/// </summary>

internal static class DataSource
{
    static readonly Random _randomNumber = new();

        ///ctor
    static DataSource()
    {
        s_Initialize();
    }

    ///class for constants numbers.
    // static public class Constants
    // {
        public const int productQuantity = 50;
        public const int orderQuantity = 100;
        public const int orderItemQuantity = 200;
    // }

    ///Config
    internal static class Config
    {
        //public static int IndexOrderItem = 0, IndexProduct = 0, IndexOrder = 0;
        public static int ProductID = 1000, OrderID = 2000, OrderItemID = 3000;
    }

       ///Defining arrays.    
    internal static List<DO.Product> _arrProduct = new List<DO.Product> { };
    internal static List<DO.Order> _arrOrder = new List<DO.Order> { };
    internal static List<DO.OrderItem> _arrOrderItem = new List<DO.OrderItem> { };

    private static void addProduct(DO.Product product)
    {
        _arrProduct.Add(product);
    }

    private static void addOrder(DO.Order order)
    {
        _arrOrder.Add(order);
    }

    private static void addOrderItem(DO.OrderItem orderItem)
    {
        _arrOrderItem.Add(orderItem);
        //_arrOrderItem[Config.IndexOrderItem++] = orderItem;
    }

    private static void s_Initialize()
    {
        (string, double, DO.categories)[] _arrProducts = {
            ("A", 100, DO.categories.watchs),("B", 100, DO.categories.watchs),("C", 100, DO.categories.watchs),
            ("D", 100, DO.categories.cameras),("E", 100, DO.categories.cameras),("F", 100, DO.categories.cameras),
            ("G", 100, DO.categories.camputers),("H", 100, DO.categories.camputers),("I", 100, DO.categories.camputers),
            ("J", 100, DO.categories.ipads),("K", 100, DO.categories.ipads),("L", 100, DO.categories.ipads)
        };

        (string, string, string)[] _arrCustomers = {
            ("a", "100", "0"),("b", "100", "0"),
            ("c", "100", "0"),("d", "100", "0"),
            ("e", "100", "0"),("f", "100", "0"),
            ("g", "100", "0"),("h", "100", "0"),
            ("i", "100", "0"),("j", "100", "0"),
            ("k", "100", "0"),("l", "100", "0"),
            ("m", "100", "0"),("n", "100", "0"),
            ("o", "100", "0"),("p", "100", "0"),
            ("q", "100", "0"),("r", "100", "0"),
            ("s", "100", "0"),("t", "100", "0"),
        };

        int randomItem;
        for (int i = 0; i < 10; i++, randomItem++)
        {
            randomItem = (int)_randomNumber.NextInt64(0, 9);
            DO.Product product = new()
            {
                ID = Config.ProductID++,
                Name = _arrProducts[randomItem % 10].Item1,
                Price = _arrProducts[randomItem % 10].Item2,
                Category = _arrProducts[randomItem % 10].Item3,
                InStock = (int)_randomNumber.NextInt64(0, 20)
            };
            addProduct(product);
        }

        for (int i = 0; i < 20; i++)
        {
            randomItem = (int)_randomNumber.NextInt64(0, 19);
            DO.Order order = new()
            {
                ID = Config.OrderID++,
                CustomerName = _arrCustomers[randomItem % 20].Item1,
                CustomerEmail = _arrCustomers[randomItem % 20].Item2,
                CustomerAddress = _arrCustomers[randomItem % 20].Item3,
                OrderDate = DateTime.Now
            };
            TimeSpan time = new((int)_randomNumber.NextInt64(1, 3), 0, 0, 0);
            order.ShipDate = (randomItem % 20) % 5 != 0 ? order.OrderDate.Add(time) : DateTime.MinValue;
            time = new TimeSpan((int)_randomNumber.NextInt64(3, 7), 0, 0, 0);
            order.DeliveryDate = (randomItem % 20) % 3 != 0 || (randomItem % 20) % 4 != 0 ? order.ShipDate.Add(time) : DateTime.MinValue;
            addOrder(order);
        }
        for (int i = 0; i < 20; i++)
        {
            randomItem = (int)_randomNumber.NextInt64(0, 3);
            for (int j = 0; j < randomItem; j++)
            {
                DO.OrderItem orderItem = new()
                {
                    ID = Config.OrderItemID++,
                    ProductID = (int)_randomNumber.NextInt64(1, 20),
                    OrderID = _arrOrder[i].ID,
                    Amount = (int)_randomNumber.NextInt64(1, 10)
                };
                //orderItem.Price = orderItem.Amount * new DalProduct().Get(orderItem.ProductID).Price;
                try
                {
                    orderItem.Price = new DalProduct().Get(orderItem.ProductID).Price;
                }
                catch(ObjectNotFound ex)
                {
                    throw ex;
                }
                addOrderItem(orderItem);
            }
        }

        //int counter = 0;
        //for (int i = 20; i < 40;)//adding items to order not more than 3 items
        //{
        //    OrderItem oi2 = new OrderItem();
        //    index = (int)_randomNumber.Next(1, 4);
        //    for (int j = 0; j < index; j++)
        //    {
        //        oi2.OrderID = _arrOrderItem[counter].ID;
        //        int iProduct = (int)_randomNumber.Next(10);
        //        oi2.ID = Config.OrderItemID++;
        //        oi2.ProductID = _arrProduct[iProduct].ID;
        //        oi2.Price = _arrProduct[iProduct].Price;
        //        oi2.Amount = (int)_randomNumber.Next(30);
        //        addOrderItem(oi2);
        //    }
        //    i += index;
        //    counter++;
        //}
    }
}
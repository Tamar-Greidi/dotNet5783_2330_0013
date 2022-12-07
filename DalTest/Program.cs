using DalApi;
using DO;
using System;

namespace Dal;

public class Program
{
    private static IDal dalList = new DalList();
    public static void Main()
    {
        Console.WriteLine("Press your option" +
            "\n Press 0 to exit" +
            "\n Press 1 to product" +
            "\n Press 2 to order" +
            "\n Press 3 to orderItem");
        int choose = int.Parse(Console.ReadLine());
        while (choose!=0)
        {
            string choosenOption = choose == 1 ? "product" : choose == 2 ? "order" : "orderItem";
            Console.WriteLine("enter your option" +
                "\n Press a to add " + choosenOption +
                "\n Press b to present " + choosenOption + " by ID" +
                "\n Press c to present all " + choosenOption + "s" +
                "\n Press d to update " + choosenOption +
                "\n Press e to delete " + choosenOption + " by ID" +
                ///choosenOption == "orderItem" ? "" +
                "\n Press f to present " + choosenOption + " by oderID and productID" +
                "\n Press g to present all " + choosenOption + "s in a specific order by orderID");
            char choosenMethod = char.Parse(Console.ReadLine());
            switch (choose)
            {
                case 1:
                    _product(choosenMethod); break;
                case 2:
                    _order(choosenMethod); break;
                case 3:
                    _orderItem(choosenMethod); break;
                default:
                    break;
            }
            Console.WriteLine("Press your option" +
           "\n Press 0 to exit" +
           "\n Press 1 to product" +
           "\n Press 2 to order" +
           "\n Press 3 to orderItem");
            choose = int.Parse(Console.ReadLine());
        }
    }
    public static void _product(char choosenMethod)
    {
        switch (choosenMethod)
        {
            case 'a':
                //add product
                Product createProduct = new Product();
                Console.WriteLine("enter product ID");
                int recivedId = int.Parse(Console.ReadLine());
                while (recivedId < 1000 || recivedId > 9999)
                {
                    Console.WriteLine("enter a valid id - 4 disits");
                    recivedId = int.Parse(Console.ReadLine());
                }
                createProduct.ID = recivedId;
                Console.WriteLine("enter product name");
                createProduct.Name = Console.ReadLine();
                Console.WriteLine("enter product price");
                createProduct.Price = int.Parse(Console.ReadLine());
                Console.WriteLine("enter product catagory");
                createProduct.Category = (categories)int.Parse(Console.ReadLine());
                Console.WriteLine("enter product inStock");
                int recivedInStock= int.Parse(Console.ReadLine());
                while (recivedInStock < 0)
                {
                    Console.WriteLine("enter a valid inStock - at list 0");
                    recivedInStock = int.Parse(Console.ReadLine());
                }
                createProduct.InStock = recivedInStock;
                try
                {
                    dalList.Product.Add(createProduct);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                }
                break;

            case 'b':
                //present product by ID
                Console.WriteLine("enter product ID");
                int givenID = int.Parse(Console.ReadLine());
                try
                {
                    Product presentProduct = dalList.Product.Get(givenID);
                    Console.WriteLine(presentProduct.ToString());
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                }
                break;

            case 'c':
                //present all products
                IEnumerable<Product> products = dalList.Product.GetAll();
                foreach (Product product in products)
                {
                    Console.WriteLine(product.ToString());
                }
                break;

            case 'd':
                //update product
                Product updateProduct = new Product();
                Console.WriteLine("enter product ID");
                updateProduct.ID = int.Parse(Console.ReadLine());
                Console.WriteLine("enter product name");
                updateProduct.Name = Console.ReadLine();
                Console.WriteLine("enter product price");
                updateProduct.Price = int.Parse(Console.ReadLine());
                Console.WriteLine("enter product catagory");
                updateProduct.Category = (categories)int.Parse(Console.ReadLine());
                Console.WriteLine("enter product inStock");
                updateProduct.InStock = int.Parse(Console.ReadLine());
                try
                {
                    int returnUpdateID = new DalProduct().Update(updateProduct);
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                }
                break;

            case 'e':
                //delete product
                Console.WriteLine("enter product ID");
                givenID = int.Parse(Console.ReadLine());
                try
                {
                    new DalProduct().Delete(givenID);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                }
                break;

            default:
                break;
        }
    }
    public static void _order(char choosenMethod)
    {
        switch (choosenMethod)
        {
            case 'a':
                //add order
                Order createOrder = new Order();
                Console.WriteLine("enter order ID");
                createOrder.ID = int.Parse(Console.ReadLine());
                Console.WriteLine("enter customer name");
                createOrder.CustomerName = Console.ReadLine();
                Console.WriteLine("enter customer address");
                createOrder.CustomerAddress = Console.ReadLine();
                Console.WriteLine("enter customer email");
                createOrder.CustomerEmail = Console.ReadLine();
                Console.WriteLine("enter order date");
                createOrder.OrderDate = DateTime.Parse(Console.ReadLine());
                Console.WriteLine("enter ship date");
                createOrder.ShipDate = DateTime.Parse(Console.ReadLine());
                Console.WriteLine("enter delivaery date");
                createOrder.DeliveryDate = DateTime.Parse(Console.ReadLine());
                try
                {
                    dalList.Order.Add(createOrder);
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                }
                break;

            case 'b':
                //present order by ID
                Console.WriteLine("enter order ID");
                int givenID = int.Parse(Console.ReadLine());
                try
                {
                    Order presentOrder = dalList.Order.Get(givenID);
                    Console.WriteLine(presentOrder.ToString());
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                }
                break;

            case 'c':
                //present all orders
                IEnumerable<Order> orders = dalList.Order.GetAll();
                foreach (Order order in orders)
                {
                    Console.WriteLine(order.ToString());
                }
                break;

            case 'd':
                //update order
                Order updateOrder = new Order();
                Console.WriteLine("enter order ID");
                updateOrder.ID = int.Parse(Console.ReadLine());
                Console.WriteLine("enter customer name");
                updateOrder.CustomerName = Console.ReadLine();
                Console.WriteLine("enter customer address");
                updateOrder.CustomerAddress = Console.ReadLine();
                Console.WriteLine("enter customer email");
                updateOrder.CustomerEmail = Console.ReadLine();
                Console.WriteLine("enter order date");
                updateOrder.OrderDate = DateTime.Parse(Console.ReadLine());
                Console.WriteLine("enter ship date");
                updateOrder.ShipDate = DateTime.Parse(Console.ReadLine());
                Console.WriteLine("enter delivaery date");
                updateOrder.DeliveryDate = DateTime.Parse(Console.ReadLine());
                try
                {
                    int returnUpdateID = new DalOrder().Update(updateOrder);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                }
                break;

            case 'e':
                //delete order
                Console.WriteLine("enter order ID");
                givenID = int.Parse(Console.ReadLine());
                try
                {
                    new DalOrder().Delete(givenID);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                }
                break;

            default:
                break;
        }
    }
    public static void _orderItem(char choosenMethod)
    {
        switch (choosenMethod)
        {
            case 'a':
                //add orderItem
                OrderItem createOrderItem = new OrderItem();
                Console.WriteLine("enter order ID");
                createOrderItem.OrderID = int.Parse(Console.ReadLine());
                Console.WriteLine("enter product ID");
                createOrderItem.ProductID = int.Parse(Console.ReadLine());
                Console.WriteLine("enter orderItem amount");
                createOrderItem.Amount = int.Parse(Console.ReadLine());
                Console.WriteLine("enter orderItem Price");
                createOrderItem.Price = double.Parse(Console.ReadLine());
                try
                {
                    dalList.OrderItem.Add(createOrderItem);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                }
                break;

            case 'b':
                //present orderOrderItem by ID
                Console.WriteLine("enter orderItem ID");
                int givenID = int.Parse(Console.ReadLine());
                try
                {
                    OrderItem presentOrderItem = dalList.OrderItem.Get(givenID);
                    Console.WriteLine(presentOrderItem.ToString());
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                }
                break;

            case 'c':
                //present all orderOrderItems
                try
                {
                    IEnumerable<OrderItem> orderItems = dalList.OrderItem.GetAll();
                    foreach (OrderItem orderItem in orderItems)
                    {
                        Console.WriteLine(orderItem.ToString());
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                }
                break;

            case 'd':
                //update orderOrderItem
                OrderItem updateOrderItem = new OrderItem();
                Console.WriteLine("enter product ID");
                updateOrderItem.ProductID = int.Parse(Console.ReadLine());
                Console.WriteLine("enter order ID");
                updateOrderItem.OrderID = int.Parse(Console.ReadLine());
                Console.WriteLine("enter orderItem price");
                updateOrderItem.Price = int.Parse(Console.ReadLine());
                Console.WriteLine("enter orderItem amount");
                updateOrderItem.Amount = int.Parse(Console.ReadLine());
                Console.WriteLine("enter orderOrderItem inStock");
                try
                {
                    int returnUpdateID = new DalOrderItem().Update(updateOrderItem);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                }
                break;

            case 'e':
                //delete orderItem
                Console.WriteLine("enter orderItem ID");
                givenID = int.Parse(Console.ReadLine());
                try
                {
                    new DalOrderItem().Delete(givenID);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                }
                break;

            case 'f':
                // present all orderItems in a specific order by orderID
                Console.WriteLine("enter orderItem ID");
                givenID = int.Parse(Console.ReadLine());
                new DalOrderItem().GetProductsByOrder(givenID);
                break;
                
            case 'g':
                //present orderItem by oderID and productID
                Console.WriteLine("enter order ID");
                int givenOrderID = int.Parse(Console.ReadLine());
                int givenProductID = int.Parse(Console.ReadLine());
                new DalOrderItem().GetProductsByOrderAndProduct(givenOrderID, givenProductID);
                break;

            default:
                break;
        }
    }
}
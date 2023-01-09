using Dal;
using DalApi;
using DO;

namespace DalTest;

public class Program
{
    private static IDal? dalList = Factory.Get();
    public static void Main()
    {
        Console.WriteLine("Press your option" +
            "\n Press 0 to exit" +
            "\n Press 1 to product" +
            "\n Press 2 to order" +
            "\n Press 3 to orderItem");
        int choose;
        int.TryParse(Console.ReadLine(),out choose);
        while (choose != 0)
        {
            string choosenOption = choose == 1 ? "product" : choose == 2 ? "order" : "orderItem";
            Console.WriteLine("Press your option" +
                "\n Press a to add " + choosenOption +
                "\n Press b to present " + choosenOption + " by ID" +
                "\n Press c to present all " + choosenOption + "s" +
                "\n Press d to update " + choosenOption +
                "\n Press e to delete " + choosenOption + " by ID");
            Console.WriteLine(choosenOption == "orderItem" ? " Press f to present all " + choosenOption + "s in a specific order by order ID" +
                "\n Press g to present " + choosenOption + " by order ID and product ID" : "");

            char choosenMethod;
            char.TryParse(Console.ReadLine(), out choosenMethod);
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
            int.TryParse(Console.ReadLine(), out choose);
        }
    }
    public static void _product(char choosenMethod)
    {
        int ID, Price, InStock, Category;
        switch (choosenMethod)
        {
            case 'a':
                //add product
                Product createProduct = new Product();
                Console.WriteLine("enter product ID");
                int recivedId;
                int.TryParse(Console.ReadLine(), out recivedId);
                while (recivedId < 1000 || recivedId > 9999)
                {
                    Console.WriteLine("enter a valid id - 4 disits");
                    int.TryParse(Console.ReadLine(), out recivedId);
                }
                createProduct.ID = recivedId;
                Console.WriteLine("enter product name");
                createProduct.Name = Console.ReadLine();
                Console.WriteLine("enter product price");
                int.TryParse(Console.ReadLine(), out Price);
                createProduct.Price = Price;
                Console.WriteLine("enter product catagory");
                int.TryParse(Console.ReadLine(), out Category);
                createProduct.Category = (categories)Category; 
                Console.WriteLine("enter product inStock");
                int.TryParse(Console.ReadLine(), out InStock);
                int recivedInStock = InStock;
                
                while (recivedInStock < 0)
                {
                    Console.WriteLine("enter a valid inStock - at list 0");
                    int.TryParse(Console.ReadLine(), out recivedInStock);
                }
                createProduct.InStock = recivedInStock;
                try
                {
                    dalList?.Product.Add(createProduct);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                }
                break;

            case 'b':
                //present product by ID
                Console.WriteLine("enter product ID");
                int givenID;
                int.TryParse(Console.ReadLine(), out givenID);
                try
                {
                    Product presentProduct = dalList?.Product.Get(givenID) ?? throw new Null();
                    Console.WriteLine(presentProduct.ToString());
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                }
                break;

            case 'c':
                //present all products
                IEnumerable<Product> products = dalList?.Product.GetAll() ?? throw new Null();
                foreach (Product product in products)
                {
                    Console.WriteLine(product.ToString());
                }
                break;

            case 'd':
                //update product
                Product updateProduct = new Product();
                Console.WriteLine("enter product ID");
                int.TryParse(Console.ReadLine(), out ID);
                updateProduct.ID = ID;
                Console.WriteLine("enter product name");
                updateProduct.Name = Console.ReadLine();
                Console.WriteLine("enter product price");
                int.TryParse(Console.ReadLine(), out Price);
                updateProduct.Price = Price;
                Console.WriteLine("enter product catagory");
                int.TryParse(Console.ReadLine(), out Category);
                updateProduct.Category = (categories)Category;
                Console.WriteLine("enter product inStock");
                int.TryParse(Console.ReadLine(), out InStock);
                updateProduct.InStock = InStock;
                try
                {
                    int returnUpdateID = new DalProduct().Update(updateProduct);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                }
                break;

            case 'e':
                //delete product
                Console.WriteLine("enter product ID");
                int.TryParse(Console.ReadLine(), out givenID);
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
        int ID;
        DateTime OrderDate, ShipDate, DeliveryDate;
        switch (choosenMethod)
        {
            case 'a':
                //add order
                Order createOrder = new Order();
                Console.WriteLine("enter order ID");
                int.TryParse(Console.ReadLine(), out ID);
                createOrder.ID = ID; 
                Console.WriteLine("enter customer name");
                createOrder.CustomerName = Console.ReadLine();
                Console.WriteLine("enter customer address");
                createOrder.CustomerAddress = Console.ReadLine();
                Console.WriteLine("enter customer email");
                createOrder.CustomerEmail = Console.ReadLine();
                Console.WriteLine("enter order date");
                DateTime.TryParse(Console.ReadLine(), out OrderDate);
                createOrder.OrderDate = OrderDate;
                Console.WriteLine("enter ship date");
                DateTime.TryParse(Console.ReadLine(), out ShipDate);
                createOrder.ShipDate = ShipDate;
                Console.WriteLine("enter delivaery date");
                DateTime.TryParse(Console.ReadLine(), out DeliveryDate);
                createOrder.DeliveryDate = DeliveryDate;
                try
                {
                    dalList?.Order.Add(createOrder);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                }
                break;

            case 'b':
                //present order by ID
                Console.WriteLine("enter order ID");
                int givenID;
                int.TryParse(Console.ReadLine(), out givenID);
                try
                {
                    Order presentOrder = dalList?.Order.Get(givenID) ?? throw new Null();
                    Console.WriteLine(presentOrder.ToString());
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                }
                break;

            case 'c':
                //present all orders
                IEnumerable<Order> orders = dalList?.Order.GetAll() ?? throw new Null();
                foreach (Order order in orders)
                {
                    Console.WriteLine(order.ToString());
                }
                break;

            case 'd':
                //update order
                Order updateOrder = new Order();
                Console.WriteLine("enter order ID");
                int.TryParse(Console.ReadLine(), out ID);
                updateOrder.ID = ID;
                Console.WriteLine("enter customer name");
                updateOrder.CustomerName = Console.ReadLine();
                Console.WriteLine("enter customer address");
                updateOrder.CustomerAddress = Console.ReadLine();
                Console.WriteLine("enter customer email");
                updateOrder.CustomerEmail = Console.ReadLine();
                Console.WriteLine("enter order date");
                DateTime.TryParse(Console.ReadLine(), out OrderDate);
                updateOrder.OrderDate = OrderDate;
                Console.WriteLine("enter ship date");
                DateTime.TryParse(Console.ReadLine(), out ShipDate);
                updateOrder.ShipDate = ShipDate;
                Console.WriteLine("enter delivaery date");
                DateTime.TryParse(Console.ReadLine(), out DeliveryDate);
                updateOrder.DeliveryDate = DeliveryDate;
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
                int.TryParse(Console.ReadLine(), out givenID);
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
        int ID, OrderID, ProductID, Amount;
        switch (choosenMethod)
        {
            case 'a':
                //add orderItem
                OrderItem createOrderItem = new OrderItem();
                Console.WriteLine("enter orderItem ID");
                int.TryParse(Console.ReadLine(), out ID);
                createOrderItem.ID = ID;
                Console.WriteLine("enter order ID");
                int.TryParse(Console.ReadLine(), out OrderID);
                createOrderItem.OrderID = OrderID;
                Console.WriteLine("enter product ID");
                int.TryParse(Console.ReadLine(), out ProductID);
                createOrderItem.ProductID = ProductID;
                Console.WriteLine("enter orderItem amount");
                int.TryParse(Console.ReadLine(), out Amount);
                createOrderItem.Amount = Amount;
                //Console.WriteLine("enter orderItem Price");
                //createOrderItem.Price = double.Parse(Console.ReadLine());
                try
                {
                    dalList?.OrderItem.Add(createOrderItem);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                }
                break;

            case 'b':
                //present orderOrderItem by ID
                Console.WriteLine("enter orderItem ID");
                int givenID;
                int.TryParse(Console.ReadLine(), out givenID);
                try
                {
                    OrderItem presentOrderItem = dalList?.OrderItem.Get(givenID) ?? throw new Null();
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
                    IEnumerable<OrderItem> orderItems = dalList?.OrderItem.GetAll() ?? throw new Null();
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
                Console.WriteLine("enter orderItem ID");
                int.TryParse(Console.ReadLine(), out ID);
                updateOrderItem.ID = ID;
                Console.WriteLine("enter product ID");
                int.TryParse(Console.ReadLine(), out ProductID);
                updateOrderItem.ProductID = ProductID;
                Console.WriteLine("enter order ID");
                int.TryParse(Console.ReadLine(), out OrderID);
                updateOrderItem.OrderID = OrderID;
                //Console.WriteLine("enter orderItem price");
                //updateOrderItem.Price = int.Parse(Console.ReadLine());
                Console.WriteLine("enter orderItem amount");
                int.TryParse(Console.ReadLine(), out Amount);
                updateOrderItem.Amount = Amount;
                //Console.WriteLine("enter orderOrderItem inStock");
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
                int.TryParse(Console.ReadLine(), out givenID);
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
                Console.WriteLine("enter order ID:");
                int.TryParse(Console.ReadLine(), out givenID);
                try
                {
                    IEnumerable<OrderItem> orderItems = dalList?.OrderItem.GetAll(item => item.ID == givenID) ?? throw new Null();
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

            case 'g':
                //present orderItem by oderID and productID
                Console.WriteLine("enter order ID:");
                int orderID;
                int.TryParse(Console.ReadLine(), out orderID);
                Console.WriteLine("enter product ID:");
                int productID;
                int.TryParse(Console.ReadLine(), out productID);
                try
                {
                    OrderItem orderItem = dalList?.OrderItem.Get(item => item.OrderID == orderID && item.ProductID == productID) ?? throw new Null();
                    Console.WriteLine(orderItem.ToString());
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
}
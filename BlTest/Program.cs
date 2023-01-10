using BO;
using BlImplementation;
using BlApi;
using DO;

namespace BLTest;

public class Program
{
    private static IBl IBl = new Bl();
    public static void Main()
    {
        Cart cart = new Cart();
        cart.Items = new List<BO.OrderItem>();
        Console.WriteLine("Press your option:" +
            "\n Press 0 to exit" +
            "\n Press 1 to product" +
            "\n Press 2 to order" +
            "\n Press 3 to cart");
        int choice;
        int.TryParse(Console.ReadLine(), out choice);
        while (choice != 0)
        {
            switch (choice)
            {
                case 1:
                    _Product();
                    break;
                case 2:
                    _Order();
                    break;
                case 3:
                    _Cart(cart);
                    break;
            }
            Console.WriteLine("Press your option:" +
                "\n Press 0 to exit" +
                "\n Press 1 to product" +
                "\n Press 2 to order" +
                "\n Press 3 to cart");
            int.TryParse(Console.ReadLine(), out choice);
        }
    }
    public static void _Product()
    {
        Console.WriteLine("Press your option:" +
            "\n Press 0 to exit " +
            "\n Press 1 to get products" +
            "\n Press 2 to get catalog" +
            "\n Press 3 to get product details" +
            "\n Press 4 to add product" +
            "\n Press 5 to delete product" +
            "\n Press 6 to update product");
        int choice;
        int.TryParse(Console.ReadLine(), out choice);
        while (choice != 0)
        {
            int ID, Price, Category, InStock;
            switch (choice)
            {
                case 1:
                    ///get products:
                    try
                    {
                        IEnumerable<ProductForList?> getProducts = IBl.Product.GetCatalog();
                        foreach (ProductForList? item in getProducts)
                        {
                            Console.WriteLine(item?.ToString());
                        }
                    }
                    catch (DalException ex)
                    {
                        Console.WriteLine(ex.Message + " " + ex.InnerException?.Message);
                    }
                    break;

                case 2:
                    ///get catalog:
                    try
                    {
                        IEnumerable<ProductForList> GetProductForCatalog = IBl.Product.GetCatalog();
                        foreach (ProductForList item in GetProductForCatalog)
                        {
                            Console.WriteLine(item.ToString());
                        }
                    }
                    catch (DalException ex)
                    {
                        Console.WriteLine(ex.Message + " " + ex.InnerException?.Message);
                    }
                    break;

                case 3:
                    ///get product details:
                    Console.WriteLine("please enter the product ID:");
                    int productID;
                    int.TryParse(Console.ReadLine(), out productID);
                    try
                    {
                        BO.Product product = IBl.Product.GetProductDetails(productID);
                        Console.WriteLine(product.ToString());
                    }
                    catch (DalException ex)
                    {
                        Console.WriteLine(ex.Message + " " + ex.InnerException?.Message);
                    }
                    break;

                case 4:
                    ///add product:
                    BO.Product addProduct = new BO.Product();
                    Console.WriteLine("enter product ID");
                    int recivedId;
                    int.TryParse(Console.ReadLine(), out recivedId);
                    while (recivedId < 1000 || recivedId > 9999)
                    {
                        Console.WriteLine("enter a valid id - 4 disits");
                        int.TryParse(Console.ReadLine(), out recivedId);
                    }
                    addProduct.ID = recivedId;
                    Console.WriteLine("enter product name");
                    addProduct.Name = Console.ReadLine();
                    Console.WriteLine("enter product price");
                    int.TryParse(Console.ReadLine(), out Price);
                    addProduct.Price = Price;
                    Console.WriteLine("enter product catagory");
                    int.TryParse(Console.ReadLine(), out Category);
                    addProduct.Category = (BO.categories)Category;
                    Console.WriteLine("enter product inStock");
                    int recivedInStock;
                    int.TryParse(Console.ReadLine(), out recivedInStock);
                    while (recivedInStock < 0)
                    {
                        Console.WriteLine("enter a valid inStock - at list 0");
                        int.TryParse(Console.ReadLine(), out recivedInStock);
                    }
                    addProduct.InStock = recivedInStock;
                    try
                    {
                        IBl.Product.Add(addProduct);
                        Console.WriteLine("product added succesfully");
                    }
                    catch (DalException ex)
                    {
                        Console.WriteLine(ex.Message + " " + ex.InnerException?.Message);
                    }
                    break;

                case 5:
                    ///delete product:
                    Console.WriteLine("insert product ID:");
                    int.TryParse(Console.ReadLine(), out ID);
                    try
                    {
                        IBl.Product.Delete(ID);
                        Console.WriteLine("product deleted succesfully");
                    }
                    catch (DalException ex)
                    {
                        Console.WriteLine(ex.Message + " " + ex.InnerException?.Message);
                    }
                    break;
                case 6:
                    ///update product:
                    BO.Product updateProduct = new BO.Product();
                    Console.WriteLine("enter product ID");
                    int.TryParse(Console.ReadLine(), out ID);
                    updateProduct.ID = ID;
                    try
                    {
                        BO.Product recentproduct = IBl.Product.GetProductDetails(updateProduct.ID);
                        Console.WriteLine(recentproduct.ToString());
                    }
                    catch (DalException ex)
                    {
                        Console.WriteLine(ex.Message + " " + ex.InnerException?.Message);
                        break;
                    }
                    Console.WriteLine("enter product name");
                    updateProduct.Name = Console.ReadLine();
                    Console.WriteLine("enter product price");
                    int.TryParse(Console.ReadLine(), out Price);
                    updateProduct.Price = Price;
                    Console.WriteLine("enter product catagory");
                    int.TryParse(Console.ReadLine(), out Category);
                    updateProduct.Category = (BO.categories)Category;
                    Console.WriteLine("enter product inStock");
                    int.TryParse(Console.ReadLine(), out InStock);
                    updateProduct.InStock = InStock;
                    try
                    {
                        IBl.Product.Update(updateProduct);
                        Console.WriteLine("product updated succesfully");
                    }
                    catch (DalException ex)
                    {
                        Console.WriteLine(ex.Message + " " + ex.InnerException?.Message);
                    }
                    break;
            }

            Console.WriteLine("Press your option:" +
                "\n Press 0 to exit " +
                "\n Press 1 to get products" +
                "\n Press 2 to get catalog" +
                "\n Press 3 to get product details" +
                "\n Press 4 to add product" +
                "\n Press 5 to delete product" +
                "\n Press 6 to update product");
            int.TryParse(Console.ReadLine(), out choice);
        }
    }

    public static void _Order()
    {
        Console.WriteLine("Press your option:" +
            "\n Press 0 to exit " +
            "\n Press 1 to get orders" +
            "\n Press 2 to get order details" +
            "\n Press 3 to update shipping" +
            "\n Press 4 to update delivery");
        int choice;
        int.TryParse(Console.ReadLine(), out choice);
        while (choice != 0)
        {
            int OrderID;
            switch (choice)
            {
                case 1:
                    ///get orders:
                    try
                    {
                        IEnumerable<OrderForList?> allOrders = IBl.Order.Get();
                        //allOrders = IBl.Order.Get();
                        foreach (var item in allOrders)
                        {
                            Console.WriteLine(item?.ToString());
                        }
                    }
                    catch (DalException ex)
                    {
                        Console.WriteLine(ex.Message + " " + ex.InnerException?.Message);
                    }
                    break;

                case 2:
                    ///get order details:
                    Console.WriteLine("insert order ID:");
                    int orderId;
                    int.TryParse(Console.ReadLine(), out orderId);
                    try
                    {
                        BO.Order orderDetails = IBl.Order.GetDetails(orderId);
                        Console.WriteLine(orderDetails);
                    }
                    catch (DalException ex)
                    {
                        Console.WriteLine(ex.Message + " " + ex.InnerException?.Message);
                    }
                    catch (InvalidData e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    break;

                case 3:
                    ///update shipping:
                    Console.WriteLine("insert order id:");
                    int.TryParse(Console.ReadLine(), out OrderID);
                    try
                    {
                        IBl.Order.UpdateShipping(OrderID);
                        Console.WriteLine("order updated succesfully!");
                    }
                    catch (DalException ex)
                    {
                        Console.WriteLine(ex.Message + " " + ex.InnerException?.Message);
                    }
                    catch (OrderAlreadyDelivered e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    break;

                case 4:
                    ///update delivery:
                    Console.WriteLine("insert order id:");
                    int.TryParse(Console.ReadLine(), out OrderID);
                    try
                    {
                        IBl.Order.UpdateDelivery(OrderID);
                        Console.WriteLine("order updated succesfully!");
                    }
                    catch (DalException ex)
                    {
                        Console.WriteLine(ex.Message + " " + ex.InnerException?.Message);
                    }
                    catch (OrderAlreadyDelivered e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    break;
            }
            Console.WriteLine("Press your option:" +
                        "\n Press 0 to exit " +
                        "\n Press 1 to get orders" +
                        "\n Press 2 to get order details" +
                        "\n Press 3 to update shipping" +
                        "\n Press 4 to update delivery");
            int.TryParse(Console.ReadLine(), out choice);
        }
    }

    public static Cart _Cart(Cart cart)
    {
        Console.WriteLine("Press your option:" +
           "\n Press 0 to exit " +
           "\n Press 1 to add product" +
           "\n Press 2 to update product amount" +
           "\n Press 3 to confirmation cart");
        int choice;
        int.TryParse(Console.ReadLine(), out choice);
        while (choice != 0)
        {
            int ProductID, Amount;
            switch (choice)
            {
                case 1:
                    Console.WriteLine("enter product ID:");
                    int.TryParse(Console.ReadLine(), out ProductID);
                    try
                    {
                        cart = IBl.Cart.Add(cart, ProductID);
                        Console.WriteLine("product added succesfully!");
                        Console.WriteLine(cart.ToString());
                    }
                    catch (DalException ex)
                    {
                        Console.WriteLine(ex.Message + " " + ex.InnerException?.Message);
                    }
                    catch (OutOfStock ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;

                case 2:
                    Console.WriteLine("enter product id");
                    int.TryParse(Console.ReadLine(), out ProductID);
                    Console.WriteLine("enter amount of product");
                    int.TryParse(Console.ReadLine(), out Amount);
                    try
                    {
                        cart = IBl.Cart.UpdateProductAmount(cart, ProductID, Amount);
                        Console.WriteLine("product updated succesfully");
                        Console.WriteLine(cart.ToString());
                    }
                    catch (DalException ex)
                    {
                        Console.WriteLine(ex.Message + " " + ex.InnerException?.Message);
                    }
                    catch (OutOfStock ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    break;

                case 3:
                    Console.WriteLine("insert customer name");
                    string? customerName = Console.ReadLine();
                    Console.WriteLine("insert customer email");
                    string? customerEmail = Console.ReadLine();
                    Console.WriteLine("insert customer address");
                    string? customerAddress = Console.ReadLine();
                    try
                    {
                        IBl.Cart.ConfirmationCart(cart);
                        Console.WriteLine("order confirmed!" + "\n thank you for ordering at our shop!");
                        cart.CustomerEmail = "";
                        cart.CustomerAddress = "";
                        cart.CustomerName = "";
                        cart.Items?.Clear();
                        cart.TotalPrice = 0;
                    }
                    catch (DalException ex)
                    {
                        Console.WriteLine(ex.Message + " " + ex.InnerException?.Message);
                    }
                    catch (OutOfStock ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
            }
            Console.WriteLine("Press your option:" +
               "\n Press 0 to exit " +
               "\n Press 1 to add product" +
               "\n Press 2 to update product amount" +
               "\n Press 3 to confirmation cart");
            int.TryParse(Console.ReadLine(), out choice);
        }
        return cart;
    }
}

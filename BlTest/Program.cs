using BO;
using BlImplementation;
using BlApi;
using DO;

namespace BLTest;

public class Program
{
    private static IBl IBl = new Bl ();
    public static void Main()
    {
        Cart cart = new Cart();
        cart.Items = new List<BO.OrderItem>();
        Console.WriteLine("Press your option:" +
            "\n Press 0 to exit" +
            "\n Press 1 to product" +
            "\n Press 2 to order" +
            "\n Press 3 to cart");
        int choice = int.Parse(Console.ReadLine());
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
            choice = int.Parse(Console.ReadLine());
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
        int choice = int.Parse(Console.ReadLine());
        while (choice != 0)
        {
            switch (choice)
            {
                case 1:
                    ///get products:
                    try
                    {
                        IEnumerable<ProductForList?> getProducts = IBl.Product.GetCatalog();
                        foreach (ProductForList item in getProducts)
                        {
                            Console.WriteLine(item.ToString());
                        }
                    }
                    catch (DalException ex)
                    {
                        Console.WriteLine(ex.Message + " " + ex.InnerException.Message);
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
                        Console.WriteLine(ex.Message + " " + ex.InnerException.Message);
                    }
                    break;

                case 3:
                    ///get product details:
                    Console.WriteLine("please enter the product ID:");
                    int productID = int.Parse(Console.ReadLine());
                    try
                    {
                        BO.Product product = IBl.Product.GetProductDetails(productID);
                        Console.WriteLine(product.ToString());
                    }
                    catch (DalException ex)
                    {
                        Console.WriteLine(ex.Message + " " + ex.InnerException.Message);
                    }
                    break;

                case 4:
                    ///add product:
                    BO.Product addProduct = new BO.Product();
                    Console.WriteLine("enter product ID");
                    int recivedId = int.Parse(Console.ReadLine());
                    while (recivedId < 1000 || recivedId > 9999)
                    {
                        Console.WriteLine("enter a valid id - 4 disits");
                        recivedId = int.Parse(Console.ReadLine());
                    }
                    addProduct.ID = recivedId;
                    Console.WriteLine("enter product name");
                    addProduct.Name = Console.ReadLine();
                    Console.WriteLine("enter product price");
                    addProduct.Price = int.Parse(Console.ReadLine());
                    Console.WriteLine("enter product catagory");
                    addProduct.Category = (BO.categories)int.Parse(Console.ReadLine());
                    Console.WriteLine("enter product inStock");
                    int recivedInStock = int.Parse(Console.ReadLine());
                    while (recivedInStock < 0)
                    {
                        Console.WriteLine("enter a valid inStock - at list 0");
                        recivedInStock = int.Parse(Console.ReadLine());
                    }
                    addProduct.InStock = recivedInStock;
                    try
                    {
                        IBl.Product.Add(addProduct);
                        Console.WriteLine("product added succesfully");
                    }
                    catch (DalException ex)
                    {
                        Console.WriteLine(ex.Message + " " + ex.InnerException.Message);
                    }
                    break;

                case 5:
                    ///delete product:
                    Console.WriteLine("insert product ID:");
                    int id = int.Parse(Console.ReadLine());
                    try
                    {
                        IBl.Product.Delete(id);
                        Console.WriteLine("product deleted succesfully");
                    }
                    catch (DalException ex)
                    {
                        Console.WriteLine(ex.Message + " " + ex.InnerException.Message);
                    }
                    break;
                case 6:
                    ///update product:
                    BO.Product updateProduct = new BO.Product();
                    Console.WriteLine("enter product ID");
                    updateProduct.ID = int.Parse(Console.ReadLine());
                    try
                    {
                        BO.Product recentproduct = IBl.Product.GetProductDetails(updateProduct.ID);
                        Console.WriteLine(recentproduct.ToString());
                    }
                    catch (DalException ex)
                    {
                        Console.WriteLine(ex.Message + " " + ex.InnerException.Message);
                        break;
                    }
                    Console.WriteLine("enter product name");
                    updateProduct.Name = Console.ReadLine();
                    Console.WriteLine("enter product price");
                    updateProduct.Price = int.Parse(Console.ReadLine());
                    Console.WriteLine("enter product catagory");
                    updateProduct.Category = (BO.categories)int.Parse(Console.ReadLine());
                    Console.WriteLine("enter product inStock");
                    updateProduct.InStock = int.Parse(Console.ReadLine());
                    try
                    {
                        IBl.Product.Update(updateProduct);
                        Console.WriteLine("product updated succesfully");
                    }
                    catch (DalException ex)
                    {
                        Console.WriteLine(ex.Message + " " + ex.InnerException.Message);
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
            choice = int.Parse(Console.ReadLine());
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
        int choice = int.Parse(Console.ReadLine());
        while (choice != 0)
        {
            switch (choice)
            {
                case 1:
                    ///get orders:
                    try
                    {
                        IEnumerable<OrderForList> allOrders = IBl.Order.Get();
                        //allOrders = IBl.Order.Get();
                        foreach (var item in allOrders)
                        {
                            Console.WriteLine(item.ToString());
                        }
                    }
                    catch (DalException ex)
                    {
                        Console.WriteLine(ex.Message + " " + ex.InnerException.Message);
                    }
                    break;

                case 2:
                    ///get order details:
                    Console.WriteLine("insert order ID:");
                    int orderId = int.Parse(Console.ReadLine());
                    try
                    {
                        BO.Order orderDetails = IBl.Order.GetDetails(orderId);
                        Console.WriteLine(orderDetails);
                    }
                    catch (DalException ex)
                    {
                        Console.WriteLine(ex.Message + " " + ex.InnerException.Message);
                    }
                    catch (InvalidData e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    break;

                case 3:
                    ///update shipping:
                    Console.WriteLine("insert order id:");
                    int orderId1 = int.Parse(Console.ReadLine());
                    try
                    {
                        IBl.Order.UpdateShipping(orderId1);
                        Console.WriteLine("order updated succesfully!");
                    }
                    catch (DalException ex)
                    {
                        Console.WriteLine(ex.Message + " " + ex.InnerException.Message);
                    }
                    catch (OrderAlreadyDelivered e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    break;

                case 4:
                    ///update delivery:
                    Console.WriteLine("insert order id:");
                    int orderId2 = int.Parse(Console.ReadLine());
                    try
                    {
                        IBl.Order.UpdateDelivery(orderId2);
                        Console.WriteLine("order updated succesfully!");
                    }
                    catch (DalException ex)
                    {
                        Console.WriteLine(ex.Message + " " + ex.InnerException.Message);
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
            choice = int.Parse(Console.ReadLine());
        }
    }

    public static Cart _Cart(Cart cart)
    {
        Console.WriteLine("Press your option:" +
           "\n Press 0 to exit " +
           "\n Press 1 to add product" +
           "\n Press 2 to update product amount" +
           "\n Press 3 to confirmation cart");
        int choice = int.Parse(Console.ReadLine());
        while (choice != 0)
        {
            switch (choice)
            {
                case 1:
                    Console.WriteLine("enter product ID:");
                    int productId = int.Parse(Console.ReadLine());
                    try
                    {
                        cart = IBl.Cart.Add(cart, productId);
                        Console.WriteLine("product added succesfully!");
                        Console.WriteLine(cart.ToString());
                    }
                    catch (DalException ex)
                    {
                        Console.WriteLine(ex.Message + " " + ex.InnerException.Message);
                    }
                    catch (OutOfStock ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;

                case 2:
                    Console.WriteLine("enter products id");
                    int id = int.Parse(Console.ReadLine());
                    Console.WriteLine("enter amount of product");
                    int amount = int.Parse(Console.ReadLine());
                    try
                    {
                        cart = IBl.Cart.UpdateProductAmount(cart, id, amount);
                        Console.WriteLine("product updated succesfully");
                        Console.WriteLine(cart.ToString());
                    }
                    catch (DalException ex)
                    {
                        Console.WriteLine(ex.Message + " " + ex.InnerException.Message);
                    }
                    catch (OutOfStock ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    break;
                    
                case 3:
                    Console.WriteLine("insert customer name");
                    string customerName = Console.ReadLine();
                    Console.WriteLine("insert customer email");
                    string customerEmail = Console.ReadLine();
                    Console.WriteLine("insert customer address");
                    string customerAddress = Console.ReadLine();
                    try
                    {
                        IBl.Cart.ConfirmationCart(cart, customerName, customerEmail, customerAddress);
                        Console.WriteLine("order confirmed!" + "\n thank you for ordering at our shop!");
                        cart.CustomerEmail = "";
                        cart.CustomerAddress = "";
                        cart.CustomerName = "";
                        cart.Items.Clear();
                        cart.TotalPrice = 0;
                    }
                    catch (DalException ex)
                    {
                        Console.WriteLine(ex.Message + " " + ex.InnerException.Message);
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
            choice = int.Parse(Console.ReadLine());
        }
        return cart;
    }
}

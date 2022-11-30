using BO;
using BlImplementation;
using BlApi;
using Dal;
using DO;
using System.Diagnostics;

namespace BLTest
{
    public class Program
    { ///vjnjh bnvjhgf
        private static IBl IBl = new Bl ();
        public static void Main(string[] args)
        {
            Cart cart = new Cart();
            cart.Items = new List<BO.OrderItem>();
            Console.WriteLine("Please enter " + "\n0 for exit " + "\n1 for Cart " + "\n2 for Order " + "\n3 for Product");
            int choice = int.Parse(Console.ReadLine());
            while (choice != 0)
            {
                switch (choice)
                {
                    case 1:
                        _Cart(cart);
                        break;
                    case 2:
                        _Order();
                        break;
                    case 3:
                        _Product();
                        break;
                }
                Console.WriteLine("Please enter " + "\n0 for exit " + "\n1 for Cart " + "\n2 for Order " + "\n3 for Product");
                choice = int.Parse(Console.ReadLine());
            }

        }
        public static Cart _Cart(Cart cart)
        {
            Console.WriteLine("Please enter " + "\n0 for exit " + "\n1 for UpdateProduct " + "\n2 for AddItem " + "\n3 for CheckOut");
            int choice = int.Parse(Console.ReadLine());
            while (choice != 0)
            {
                switch (choice)
                {
                    case 1:
                        Console.WriteLine("insert products id");
                        int id = int.Parse(Console.ReadLine());
                        Console.WriteLine("insert amount of product");
                        int amount = int.Parse(Console.ReadLine());
                        try
                        {
                            cart = IBl.Cart.UpdateProductAmount(cart, id, amount);
                            Console.WriteLine("product updated succesfully");
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

                        Console.WriteLine("insert product id");
                        int productId = int.Parse(Console.ReadLine());
                        try
                        {
                            cart = IBl.Cart.Add(cart, productId);
                            Console.WriteLine("product added succesfully!");
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
                Console.WriteLine("Please enter " + "\n0 for exit " + "\n1 for UpdateProduct " + "\n2 for AddItem " + "\n3 for CheckOut");
                choice = int.Parse(Console.ReadLine());
            }
            return cart;
        }
        public static void _Order()
        {
            Console.WriteLine("Please enter " + "\n0 for exit " + "\n1 for GetOrders " + "\n2 for GetOrdersDetails " + "\n3 for UpdateSent" + "\n4 for UpdateDelivered");
            int choice = int.Parse(Console.ReadLine());
            while (choice != 0)
            {
                switch (choice)
                {
                    case 1:
                        try
                        {
                            IEnumerable<OrderForList> allOrders = new List<OrderForList>();
                            allOrders = IBl.Order.Get();
                            foreach (var item in allOrders)
                            {
                                Console.WriteLine(item);
                            }
                        }
                        catch (DalException ex)
                        {
                            Console.WriteLine(ex.Message + " " + ex.InnerException.Message);
                        }
                        break;
                    case 2:
                        Console.WriteLine("insert order id:");
                        int orderId =int.Parse(Console.ReadLine());
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
                Console.WriteLine("Please enter " + "\n0 for exit " + "\n1 for GetOrders " + "\n2 for GetOrdersDetails " + "\n3 for UpdateSent" + "\n4 for UpdateDelivered");
                choice = int.Parse(Console.ReadLine());
            }
        }
        public static void _Product()
        {
            Console.WriteLine("Please enter " + "\n0 for exit " + "\n1 for UpdateProduct " + "\n2 for GetProductForCatalog " + "\n3 for GetProducts" + "\n4 for GetProductsDetails" + "\n5 for DeleteProduct" + "\n6 for AddProduct");
            int choice = int.Parse(Console.ReadLine());
            while (choice != 0)
            {
                switch (choice)
                {
                    case 1:
                        BO.Product updateProduct = new BO.Product();
                        Console.WriteLine("please enter the products id");
                        updateProduct.ID = int.Parse(Console.ReadLine());
                        try
                        {
                            BO.Product recentproduct = IBl.Product.GetProductDetails(updateProduct.ID);
                            Console.WriteLine(recentproduct);
                        }
                        catch (DalException ex)
                        {
                            Console.WriteLine(ex.Message + " " + ex.InnerException.Message);
                            break;
                        }
                        catch (InvalidData ex)
                        {
                            Console.WriteLine(ex.Message);
                            break ;
                        }
                        Console.WriteLine("please reenter the products name");
                        updateProduct.Name = Console.ReadLine();
                        Console.WriteLine("please reenter the products category:" + "\n 0 for cats" + "\n 1 for dogs" + "\n 2 for fish" + "\n 3 for snakes ");
                        updateProduct.Category = (BO.categories)int.Parse(Console.ReadLine());
                        Console.WriteLine("please reenter the products price");
                        updateProduct.Price = int.Parse(Console.ReadLine());
                        Console.WriteLine("please reenter the products amount in stock");
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
                    case 2:
                        try
                        {
                            IEnumerable<ProductItem> GetProductForCatalog = new List<ProductItem>();
                            Console.WriteLine("insert product's id");
                            int productId = int.Parse(Console.ReadLine());
                
                            GetProductForCatalog= IBl.Product.GetCatalog();
                            Console.WriteLine(GetProductForCatalog);
                        }
                        catch (DalException ex)
                        {
                            Console.WriteLine(ex.Message + " " + ex.InnerException.Message);
                        }
                        break;
                    case 3:
                        try
                        {
                            IEnumerable<BO.ProductForList> GetProducts = new List<BO.ProductForList>();
                            GetProducts= IBl.Product.GetProducts();
                            foreach (var item in GetProducts)
                            {
                                Console.WriteLine(item);
                            }
                        }
                        catch (DalException ex)
                        {
                            Console.WriteLine(ex.Message + " " + ex.InnerException.Message);
                        }
                        break;
                    case 4:
                        BO.Product updateProduct1 = new BO.Product();
                        Console.WriteLine("please enter the products id");
                        updateProduct1.ID = int.Parse(Console.ReadLine());
                        try
                        {
                            BO.Product recentproduct = new BO.Product();
                            recentproduct= IBl.Product.GetProductDetails(updateProduct1.ID);
                            Console.WriteLine(recentproduct);
                        }
                        catch (DalException ex)
                        {
                            Console.WriteLine(ex.Message + " " + ex.InnerException.Message);
                        }
                        catch (InvalidData ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case 5:
                        Console.WriteLine("insert product's id:");
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
                        BO.Product addProduct = new BO.Product();
                        Console.WriteLine("please enter the products id");
                        addProduct.ID = int.Parse(Console.ReadLine());
                        Console.WriteLine("please enter the products name");
                        addProduct.Name = Console.ReadLine();
                        Console.WriteLine("please enter the products category:" + "\n 0 for cats" + "\n 1 for dogs" + "\n 2 for fish" + "\n 3 for snakes ");
                        addProduct.Category = (BO.categories)int.Parse(Console.ReadLine());
                        Console.WriteLine("please enter the products price");
                        addProduct.Price = int.Parse(Console.ReadLine());
                        Console.WriteLine("please enter the products amount in stock");
                        addProduct.InStock = int.Parse(Console.ReadLine());
                        try
                        {
                            IBl.Product.Add(addProduct);
                            Console.WriteLine("product added succesfully");
                        }
                        catch (DalException ex)
                        {
                            Console.WriteLine(ex.Message + " " + ex.InnerException.Message);
                        }
                        catch (InvalidData ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                }
                Console.WriteLine("Please enter " + "\n0 for exit " + "\n1 for UpdateProduct " + "\n2 for GetProductForCatalog " + "\n3 for GetProducts" + "\n4 for GetProductsDetails" + "\n5 for DeleteProduct" + "\n6 for AddProduct");
                choice = int.Parse(Console.ReadLine());
            }
        }

    }
}

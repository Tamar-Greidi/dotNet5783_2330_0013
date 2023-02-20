using System.Net.Http.Headers;
namespace Dal;

internal class Program
{
    public static void Main(string[] args)
    {
        DalApi.IDal? Dal = DalApi.Factory.Get();
        DO.Product product = new DO.Product()
        {
            ID = 9002,
            Name = "Update",
            Price = 9001123,
            Category = (DO.categories)0
        };
        try
        {
            Dal?.Product.Add(product);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception: Add try " + ex.Message);
        }
        Console.WriteLine("GetAll cw:");
        Dal?.Product.GetAll().ToList().ForEach(item => Console.WriteLine(item));
        //try
        //{
        //    var abc = Dal?.Product.Get(9001);
        //    Console.WriteLine("get 9001");
        //    Console.WriteLine(abc);
        //}
        //catch (Exception ex)
        //{
        //    Console.WriteLine("Exception: Get try " + ex.Message);
        //}
        //try
        //{
        //    Dal?.Product.Delete(9001);
        //}
        //catch (Exception ex)
        //{
        //    Console.WriteLine("Exception: delete try " + ex.Message);
        //}

        //Console.WriteLine("GetAll after delete cw:");
        //Dal?.Product.GetAll().ToList().ForEach(item => Console.WriteLine(item));
        //try
        //{
        //    Dal?.Product.Update(product);
        //    Console.WriteLine("GetAll after update cw:");
        //    Dal?.Product.GetAll().ToList().ForEach(item => Console.WriteLine(item));
        //}
        //catch (Exception ex)
        //{
        //    Console.WriteLine("Exception: update try " + ex.Message);
        //}







        //DO.OrderItem orderItem = new DO.OrderItem()
        //{
        //    //ID = 9001,
        //    //Name = "cvbfgh",
        //    //Price = 123,
        //    //Category = (DO.categories)0
        //};
        //try
        //{
        //    Dal?.OrderItem.Add(orderItem);
        //}
        //catch (Exception ex)
        //{
        //    Console.WriteLine("Exception: " + ex.Message);
        //}

        //Dal?.OrderItem.GetAll().ToList().ForEach(item => Console.WriteLine(item));
        //try
        //{
        //    var abc = Dal?.OrderItem.Get(9000);
        //    Console.WriteLine("get 9000");
        //    Console.WriteLine(abc);
        //}
        //catch (Exception ex)
        //{
        //    Console.WriteLine("Exception: " + ex.Message);
        //}
        ////Dal?.OrderItem.Delete(9000);
        ////Dal?.OrderItem.GetAll().ToList().ForEach(item => Console.WriteLine(item));
        //Dal?.OrderItem.Update(orderItem);
        //Dal?.OrderItem.GetAll().ToList().ForEach(item => Console.WriteLine(item));

        //Console.WriteLine("Hello, World!");

    }
}
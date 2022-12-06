using System.Security.Principal;
namespace BlApi;

public interface IBl    ///  interface of Bl .
{
    public IProduct Product { get; }
    public ICart Cart { get; }
    public IOrder Order { get; }
}

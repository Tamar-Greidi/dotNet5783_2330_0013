using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
namespace DalApi;

/// <summary>
/// OrderItem's interface (Inherited from crud).
/// </summary>

public interface IOrderItem : ICrud<OrderItem>
{
    public IEnumerable<OrderItem> GetProductsByOrder(int orderID);
    public OrderItem GetProductByOrderAndProduct(int orderID, int productID);
}
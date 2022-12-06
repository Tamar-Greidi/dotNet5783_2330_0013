using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
namespace DalApi;

public interface IOrderItem : ICrud<OrderItem>   /// interface of orderItem (Inherited from crud).
{
    public IEnumerable<OrderItem?> GetProductsByOrder(int orderID);

    public OrderItem GetProductsByOrderAndProduct(int orderID, int productID);
}

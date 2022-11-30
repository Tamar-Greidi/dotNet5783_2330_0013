using System.Collections.Generic;
using BO;

namespace BlApi;

public interface ICart
{ 
    public Cart Add(Cart cart, int productID);
    public Cart UpdateProductAmount(Cart cart, int productID, int amount);
    public void ConfirmationCart(Cart cart, string CustomerName, string CustomerEmail, string CustomerAddress);
}
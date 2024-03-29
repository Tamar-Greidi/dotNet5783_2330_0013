using System.Collections.Generic;
using BO;

namespace BlApi;

/// <summary>
/// Cart's interface.
/// </summary>
public interface ICart 
{ 
    public Cart Add(Cart cart, int productID);
    public Cart UpdateProductAmount(Cart cart, int productID, int amount);
    public void ConfirmationCart(Cart cart);
}
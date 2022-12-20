using DalApi;

namespace BlImplementation;

/// <summary>
/// Implementation of the Cart class.
/// </summary>
internal class BlCart : BlApi.ICart
{
    private IDal Dal = DalApi.Factory.Get();
    /// <summary>
    /// Add product to cart.
    /// </summary>
    /// <param name="cart"></param>
    /// <param name="productID"></param>
    /// <returns></returns>
    /// <exception cref="BO.OutOfStock"></exception>
    public BO.Cart Add(BO.Cart cart, int productID)
    {
        try
        {
            DO.Product AddProduct = Dal.Product.Get(productID);
            foreach (var item in cart.Items)
            {
                if (AddProduct.InStock >= 1 && item.ProductID == productID)
                {
                    item.Amount++;
                    item.TotalPrice += AddProduct.Price;
                    cart.TotalPrice += AddProduct.Price;
                }
            }
            if (AddProduct.InStock >= 1)
            {
                BO.OrderItem orderItemToAdd = new BO.OrderItem()
                {
                    ProductID = productID,
                    Price = AddProduct.Price,
                    TotalPrice = AddProduct.Price,
                    Name = AddProduct.Name,
                    Amount = 1
                };
                cart.TotalPrice += orderItemToAdd.Price;
                cart.Items.Add(orderItemToAdd);
            }
            else
                try
                {
                    throw new BO.OutOfStock();
                }
                catch (BO.OutOfStock ex)
                {
                    throw ex;
                }
        }
        catch (DalApi.ObjectNotFound ex) //לא קיים כזה
        {
            throw new BO.DalException(ex);
        }
        return cart;
    }
    /// <summary>
    /// Product amount update.
    /// </summary>
    /// <param name="cart"></param>
    /// <param name="productID"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    /// <exception cref="BO.OutOfStock"></exception>
    public BO.Cart UpdateProductAmount(BO.Cart cart, int productID, int amount)
    {
        foreach (var item in cart.Items)
        {
            if (item.ProductID == productID)
            {
                DO.Product product = Dal.Product.Get(productID);
                //add
                if (item.Amount < amount)
                {
                    if (product.InStock < (amount - item.Amount))
                        throw new BO.OutOfStock();
                    item.Amount = amount;
                    item.TotalPrice = item.Price * item.Amount;
                }
                //reduce
                if (item.Amount > amount)
                {
                    item.Amount = amount;
                    item.TotalPrice = item.Price * item.Amount;
                }
                //remove
                if (item.Amount == 0)
                {
                    cart.Items.Remove(item);
                    cart.TotalPrice -= item.Price * item.Amount;
                }
            }
        }
        return cart;
    }
    /// <summary>
    /// Basket confirmation for order.
    /// </summary>
    /// <param name="cart"></param>
    /// <param name="CustomerName"></param>
    /// <param name="CustomerEmail"></param>
    /// <param name="CustomerAddress"></param>
    /// <exception cref="BO.InvalidData"></exception>
    /// <exception cref="BO.DalException"></exception>
    public void ConfirmationCart(BO.Cart cart, string CustomerName, string CustomerEmail, string CustomerAddress)
    {
        if (CustomerName == null || CustomerEmail == null || CustomerAddress == null)
            throw new BO.InvalidData();
        foreach (var item in cart.Items)
        {
            try
            {
                DO.Product TempProduct = Dal.Product.Get(item.ProductID);
                if (item.Amount < 0 || item.Amount > TempProduct.InStock)
                    throw new BO.InvalidData();
            }
            catch (BO.InvalidData ex)
            {
                throw ex;
            }
        }
        DO.Order order = new();
        order.CustomerName = CustomerName;
        order.CustomerEmail = CustomerEmail;
        order.CustomerAddress = CustomerAddress;
        order.OrderDate = DateTime.Now;
        order.ShipDate = DateTime.MinValue;
        order.DeliveryDate = DateTime.MinValue;
        try
        {
            int orderID = Dal.Order.Add(order);
            foreach (var item in cart.Items)
            {
                DO.OrderItem orderItem = new();
                orderItem.ID = 0;
                orderItem.ProductID = item.ProductID;
                orderItem.OrderID = orderID;
                orderItem.Price = item.Price;
                orderItem.Amount = item.Amount;
                try
                {
                    Dal.OrderItem.Add(orderItem);
                }
                catch (DalApi.ObjectNotFound ex) //לא קיים כזה
                {
                    throw new BO.DalException(ex);
                }
                catch (BO.OutOfStock ex)
                {
                    throw ex;
                }
            }
        }
        catch (BO.ObjectAlreadyExists ex)
        {
            throw ex;
        }
    }
}
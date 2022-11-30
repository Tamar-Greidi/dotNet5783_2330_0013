
namespace BlImplementation;

internal class BlCart : BlApi.ICart
{
    private static DalApi.IDal Dal = new Dal.DalList();

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
                throw new BO.OutOfStock();
        }
        catch (BO.ObjectNotFound ex) //אין כזה מוצר
        {
            //throw new BO.ObjectNotFound(ex);
        }
        return cart;
    }

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

    public void ConfirmationCart(BO.Cart cart, string CustomerName, string CustomerEmail, string CustomerAddress)
    {
        if (CustomerName == null || CustomerEmail == null || CustomerAddress == null)
            throw new BO.InvalidData();
        foreach (var item in cart.Items)
        {
            try
            {
                DO.Product TempProduct = Dal.Product.Get(item.ID);
                if (item.Amount < 0 || item.Amount > TempProduct.InStock)
                    throw new BO.InvalidData();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
        DO.Order order = new();
        order.CustomerName = CustomerName;
        order.CustomerEmail = CustomerEmail;
        order.CustomerAddress = CustomerAddress;
        order.OrderDate = DateTime.Now;
        order.ShipDate = DateTime.MinValue;
        order.DeliveryDate = DateTime.MinValue;
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
            catch (Exception)
            {
                throw new Exception("לא הצליח להוסיף פריט להזמנה");
            }
        }
    }
}
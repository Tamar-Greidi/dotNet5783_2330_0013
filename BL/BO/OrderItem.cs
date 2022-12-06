namespace BO;
public class OrderItem   // Business Object/OrderItem:
{
    public int ID { get; set; }
    public string Name { get; set; }
    public int ProductID { get; set; }
    //public string ProductName { get; set; }
    public double Price { get; set; }
    public int Amount { get; set; }
    public double TotalPrice { get; set; }

    public override string ToString() => $@"
        OrderItem ID={ProductID}:
        Name: {Name},
        Product ID: {ProductID},
        Price: {Price},
        Amount: {Amount},
        Total Price: {TotalPrice}
        ";
}
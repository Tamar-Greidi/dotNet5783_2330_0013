namespace BO;
public class OrderTracking
{
    public int ID { get; set; }
    public OrderStatus Status { get; set; }

    public override string ToString() => $@"
        Item ID={ID}:
        Status: {Status}
        ";
}
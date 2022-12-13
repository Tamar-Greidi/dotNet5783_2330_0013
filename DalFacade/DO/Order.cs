
namespace DO;

/// <summary>
/// Data Object/Order
/// </summary>
public struct Order
{
    public int ID { get; set; }
    public string? CustomerName { get; set; }
    public string? CustomerEmail { get; set; }
    public string? CustomerAddress { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime ShipDate { get; set; }
    public DateTime DeliveryDate { get; set; }

    public override string ToString() => $@"
    Order ID={ID}: {CustomerName},
    CustomerEmail: {CustomerEmail},
    CustomerAddress: {CustomerAddress} 
    Order Date: {OrderDate}
    Ship Date: {ShipDate}
    Delivery Date: {DeliveryDate}        	
    ";
}
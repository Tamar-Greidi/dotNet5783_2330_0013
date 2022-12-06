using System;


namespace DO;

public struct Order     ///struct of order.
{
    public int ID { get; set; }
    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }
    public string CustomerAddress { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime ShipDate { get; set; }
    public DateTime DeliveryDate { get; set; }

    public override string ToString() => $@"
    Order ID={ID}: {CustomerName},{CustomerEmail},{CustomerAddress} 
        	Order Date: {OrderDate}
        	Ship Date: {ShipDate}
        	Delivery Date: {DeliveryDate}

        	
    ";
}
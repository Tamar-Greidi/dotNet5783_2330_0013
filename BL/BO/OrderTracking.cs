using System;

namespace BO;

/// <summary>
/// Business Object/OrderTracking
/// </summary>

public class OrderTracking
{
    public int ID { get; set; }
    public OrderStatus Status { get; set; }
    public List<(DateTime, string)>? Tracking { get; set; }

    public override string ToString()
    {
        string toString = "\n ID: " + ID + "\n Status: " + Status + "\n Tracking: ";
        Tracking?.ForEach(item => toString += "\n\t Date: "+item.Item1+ ", Description: " + item.Item2);
        return toString;
    }
}
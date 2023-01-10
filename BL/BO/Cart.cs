using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

/// <summary>
/// Business Object/Cart
/// </summary>
public class Cart
{
    public string? CustomerName { get; set; }
    public string? CustomerEmail { get; set; }
    public string? CustomerAddress { get; set; }
    public List<OrderItem>? Items { get; set; }
    public double TotalPrice { get; set; }

    public override string ToString()
    {
        string toString = "\n Customer Name: " + CustomerName + "," +
        "\n Email: " + CustomerEmail + "," +
        "\n Address: " + CustomerAddress + "," +
        "\n Items: ";
        Items?.ForEach(item => toString += "\n item #" + (Items.IndexOf(item) + 1) + ":" + item);
        toString += "\n Total Price: " + TotalPrice;
        return toString;
    }
} 
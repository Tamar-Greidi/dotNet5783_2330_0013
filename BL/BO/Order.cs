﻿using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

/// <summary>
/// Business Object/Order
/// </summary>
public class Order
{
    public int ID { get; set; }
    public string? CustomerName { get; set; }
    public string? CustomerEmail { get; set; }
    public string? CustomerAddress { get; set; }
    public DateTime OrderDate { get; set; }
    public OrderStatus Status { get; set; }
    public DateTime ShipDate { get; set; }
    public DateTime DeliveryDate { get; set; }
    public List<BO.OrderItem>? Items { get; set; }
    public double TotalPrice { get; set; }

    public override string ToString() => $@"
        Order ID: {ID},
        Customer Name: {CustomerName},
        Email: {CustomerEmail},
        Address: {CustomerAddress} 
        Order Date: {OrderDate}
        Ship Date: {ShipDate}
        Delivery Date: {DeliveryDate}	
    ";
}
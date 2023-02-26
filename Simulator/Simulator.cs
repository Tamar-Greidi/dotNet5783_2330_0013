using DalApi;
using System;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;

namespace Simulator;
public static class Simulator
{
    public delegate void StatusChanged(BO.Order order, string status, DateTime start, DateTime end, int time);
    public static event StatusChanged? statusChanged = null;
    public delegate void StopSimulator(DateTime end, string reason = "");
    public static event StopSimulator? stopSimulator = null;
    volatile private static bool stopRequest = false;
    public static bool IsAlive { get; set; } = false;

    public static void Start()
    {
        IsAlive = true;
        Thread? thread = new Thread(()=>Run());
        stopRequest = false;
        thread.Start();
    }

    public static void Run()
    {
        BlApi.IBl bl = BlApi.Factory.Get();
        Random rand = new Random();
        string? status = Convert.ToString(0);
        string stop = "";
        while (!stopRequest)
        {
            int? orderID = bl.Order.OrderSelection();
            if (orderID == null)
            {
                stopRequest = true;
                stop = "no order to handler";
                break;
            }
            if (stopRequest)
                break;
            BO.Order? order = bl.Order.GetDetails(Convert.ToInt32(orderID));
            int time = rand.Next(1000, 2000);
            DateTime start = DateTime.Now;
            Thread.Sleep(time);
            BO.Order UpdateOrder = new BO.Order();
            if (order.Status == BO.OrderStatus.Confirmed)
            {
                //bl.Order.UpdateShipping(orderID.Value);
                UpdateOrder = bl.Order.UpdateShipping(orderID.Value);
                status = "Shipped";
            }
            else
            {
                //bl.Order.UpdateDelivery(Convert.ToInt32(orderID));
                UpdateOrder = bl.Order.UpdateDelivery(Convert.ToInt32(orderID));
                status = "Delivery";
            }
            DateTime end = DateTime.Now;
            statusChanged?.Invoke(UpdateOrder, status, start, end, time / 1000);
            Thread.Sleep(1000);
        }
        stopSimulator?.Invoke(DateTime.Now, stop);
    }

    public static void Stop()
    {
        stopRequest = true;
        IsAlive = false;
    }
}
using DalApi;
using System;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;

namespace Simulator;

public delegate void stopEvent();
public delegate void StateChange(BO.OrderStatus prevStatus, BO.OrderStatus newStatus);
public delegate void OrderSelected(BO.Order order, int assecond);

public static class Simulator
{
    private static BO.OrderStatus _prevStatus;
    private static BO.OrderStatus _nextStatus;
    private static volatile bool _toStop = false;
    public static event stopEvent? stopEvent = null;
    public static event StateChange? statusChanged = null;
    public static event OrderSelected? orderSelected = null;

    public static void Start()
    {
        _toStop = false;
        BlApi.IBl bl = BlApi.Factory.Get();
        Random rand = new Random();
        Thread? thread = new Thread(() =>
        {
            int? orderID = bl.Order.OrderSelection();
            if (orderID == null)
                return;
            BO.Order order = bl.Order.GetDetails((int)orderID);
            //orderSelected(order, rand.Next(1000, 5000));
        });
    }

}



//    public delegate void ProductSelectedDel(BO.Order product, int willUpdatedAt);

//public event EventHandler statusChanged;


//public static class Simulator
//{
//    private static volatile int vola;
//    public static bool IsAlive { get; set; } = false;
//    public static void Stop()
//    {

//    }
//}
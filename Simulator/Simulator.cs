using System.Diagnostics;

namespace Simulator;

public delegate void ProductSelectedDel(BO.Order product, int willUpdatedAt);
public static class Simulator
{
    private static volatile int vola;

    public static bool IsAlive { get; set; } = false;
    public static void Run()
    {
        Thread thread;
    }

    public static void Stop()
    {
        
    }

}
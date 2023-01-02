using DalApi;

namespace Dal;

/// <summary>
/// DalList class.
/// </summary>

internal sealed class DalList : IDal
{
    public IProduct Product => new DalProduct();
    public IOrder Order => new DalOrder();
    public IOrderItem OrderItem => new DalOrderItem();
    public static IDal Instance { get; } = new DalList();
    DalList()
    {

    }
}
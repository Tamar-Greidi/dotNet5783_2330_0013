using DalApi;

namespace Dal;

/// <summary>
/// DalList class.
/// </summary>

internal sealed class DalList : IDal
{
    private IProduct Product => new DalProduct();
    private IOrder Order => new DalOrder();
    private IOrderItem OrderItem => new DalOrderItem();
    public static IDal Instance { get; } = new DalList();
}
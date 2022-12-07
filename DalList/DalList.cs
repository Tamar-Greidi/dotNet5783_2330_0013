using DalApi;

namespace Dal;

/// <summary>
/// DalList class.
/// </summary>

sealed public class DalList : IDal
{
    public IProduct Product => new DalProduct();
    public IOrder Order => new DalOrder();
    public IOrderItem OrderItem => new DalOrderItem();
}
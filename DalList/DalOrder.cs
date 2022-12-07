using DO;
using static Dal.DataSource;
using DalApi;

namespace Dal;

/// <summary>
/// DalOrder class.
/// </summary>

public class DalOrder: IOrder
{
    public int Add(Order order)
    {
        try
        {
            Get(order.ID);
        }
        catch (Exception)
        {

            if (_arrOrder.Count() < _arrOrder.Count)
            {
                _arrOrder.Add(order);
            }
            else
            {
                throw new ArrayIsFull();
            }
            return order.ID;
        }
        throw new ObjectAlreadyExists();
    }

    public Order Get(int orderID)
    {
        for (int i = 0; i < _arrOrder.Count(); i++)

        {
            if (_arrOrder[i].ID == orderID)
            {
                return _arrOrder[i];
            }
        }
        throw new ObjectNotFound();
    }

    public IEnumerable<Order> GetAll()
    {
        try
        {
            if (_arrOrder.Count() == 0)
                throw new ObjectNotFound();
        }
        catch(Exception)
        {
            throw new ObjectNotFound();
        }
        List<Order> _OrdersShow = new List<Order>();
        for (int i = 0; i < _arrOrder.Count(); i++)
        {
            DO.Order tempOrder = new();
            tempOrder.ID = _arrOrder[i].ID;
            tempOrder.CustomerName = _arrOrder[i].CustomerName;
            _OrdersShow.Add(tempOrder);
        }
        return _OrdersShow;
    }

    public int Update(Order order)
    {
        for (int i = 0; i < _arrOrder.Count(); i++)
        {
            if (_arrOrder[i].ID == order.ID)
            {
                _arrOrder[i] = order;
            }
        }
        throw new ObjectNotFound();
    }

    public void Delete(int orderIndex)
    {
        for (int i = 0; i < _arrOrder.Count(); i++)
        {
            if (_arrOrder[i].ID == orderIndex)
            {
                _arrOrder.RemoveAt(i);
                return;
            }
        }
        throw new ObjectNotFound();
    }
}
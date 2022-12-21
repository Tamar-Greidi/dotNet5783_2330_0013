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
        
        if (orderQuantity > _arrOrder.Count())
        {
            order.ID = Config.OrderID++;
            _arrOrder.Add(order);
        }   
        else
            throw new ArrayIsFull();
        return order.ID;
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

    public Order Get(Predicate<Order> func)
    {
        Order order = _arrOrder.Find(func);
        if (order.ID == 0)
            throw new ObjectNotFound();
        return order;
    }

    public IEnumerable<Order> GetAll(Func<Order, bool>? func = null)
    {
        //return func == null ? _arrOrder : _arrOrder.Where(func);
        IEnumerable<Order> a = func == null ? _arrOrder : _arrOrder.Where(func);
        return a;
    }

    public int Update(Order order)
    {
        for (int i = 0; i < _arrOrder.Count; i++)
        {
            if (_arrOrder[i].ID == order.ID)
            {
                _arrOrder[i] = order;
                return _arrOrder[i].ID;
            }
        }
        try
        {
            throw new ObjectNotFound();
        }
        catch (ObjectNotFound ex)
        {
            throw ex;
        }
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
        try
        {
            throw new ObjectNotFound();
        }
        catch (ObjectNotFound ex)
        {
            throw ex;
        }
    }
}
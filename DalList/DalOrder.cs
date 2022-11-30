using DO;
using static Dal.DataSource;
using DalApi;

namespace Dal;

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

            if (Config.IndexOrder < _arrOrder.Count)
            {
                _arrOrder[Config.IndexOrder++] = order;
            }
            else
            {
                throw new Exception("the array is full.");
            }
            return order.ID;
        }
        throw new Exception("ID is already exsist.");
    }

    public Order Get(int orderID)
    {
        for (int i = 0; i < Config.IndexOrder; i++)

        {
            if (_arrOrder[i].ID == orderID)
            {
                return _arrOrder[i];
            }
        }
        throw new Exception("order id is not exsist");
    }

    public IEnumerable<Order> GetAll()
    {
        if (Config.IndexOrder == 0)
        {
            throw new Exception("the orders dont exist yet");
        }
        List<Order> _OrdersShow = new List<Order>();
        for (int i = 0; i < Config.IndexOrder; i++)
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
        for (int i = 0; i < Config.IndexOrder; i++)
        {
            if (_arrOrder[i].ID == order.ID)
            {
                _arrOrder[i] = order;
            }
        }
        throw new Exception("order is not exsist");
    }

    public void Delete(int orderIndex)
    {
        for (int i = 0; i < Config.IndexOrder; i++)
        {
            if (_arrOrder[i].ID == orderIndex)
            {
                _arrOrder.RemoveAt(i);
                return;
            }
        }
        throw new Exception("order is not exsist");
    }
}
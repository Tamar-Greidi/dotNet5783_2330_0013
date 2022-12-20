using DO;
using static Dal.DataSource;
using DalApi;

namespace Dal;

/// <summary>
/// DalProduct class.
/// </summary>

public class DalProduct: IProduct
{
    public int Add(Product product)
    {
        try
        {
            if (Get(product.ID).ID == product.ID)
                throw new ObjectAlreadyExists();
        }
        catch (ObjectNotFound)
        {
            _arrProduct.Add(product);
        }
        return product.ID;
    }

    public Product Get(int productID)
    {
        Product product = _arrProduct.Find(product => product.ID == productID);
        if (product.ID == 0)
            if (product.ID == 0)
                throw new ObjectNotFound();
        return product;
    }

    public Product Get(int productID, Predicate<Product>? func)
    {
        Product product = _arrProduct.Find(func);
        if (product.ID == 0)
            throw new ObjectNotFound();
        return product;
    }

    public IEnumerable<Product> GetAll(Func<Product, bool> func = null)
    {
        return func == null ?  _arrProduct: _arrProduct.Where(func);
    }

    public int Update(Product product)
    {

        for (int i = 0; i < _arrProduct.Count; i++)
        {
            if (_arrProduct[i].ID == product.ID)
            {
                _arrProduct[i] = product;
                return _arrProduct[i].ID;
            }
        }
        throw new ObjectNotFound();
    }

    public void Delete(int productIndex)
    {
        for (int i = 0; i < _arrProduct.Count(); i++)
        {
            if (_arrProduct[i].ID == productIndex)
            {
                _arrProduct.RemoveAt(i);
                return;
            }
        }
        throw new ObjectNotFound();
    }
}
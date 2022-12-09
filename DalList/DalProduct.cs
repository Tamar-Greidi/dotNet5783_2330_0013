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
        catch (ObjectAlreadyExists ex)
        {
            throw ex;
        }
        _arrProduct.Add(product);
        return product.ID;
    }

    public Product Get(int productID)
    {
        Product product = _arrProduct.Find(product => product.ID == productID);
        if (product.ID == 0)
        {
            try
            {
                throw new ObjectNotFound();
            }
            catch (ObjectNotFound ex)
            {
                throw ex;
            }
        }
        return product;
    }

    public IEnumerable<Product> GetAll()
    {
        List<Product> _ProductsShow = new();
        foreach (Product product in _arrProduct)
        {
            _ProductsShow.Add(product);
        }
        return _ProductsShow;
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
        try
        {
            throw new ObjectNotFound();
        }
        catch(ObjectNotFound ex)
        {
            throw ex;
        }
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
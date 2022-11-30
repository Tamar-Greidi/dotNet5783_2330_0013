using DO;
using System;
using static Dal.DataSource;
using DalApi;

namespace Dal;

public class DalProduct: IProduct
{
    public int Add(Product product)
    {
        try
        {
            Get(product.ID);
        }
        catch (Exception)
        {
            if (_arrProduct.Count() < _arrProduct.Count)
            {
                _arrProduct.Add(product);
            }
            else
            {
                throw new ArrayIsFull();
            }
            return product.ID;
        }
        throw new ObjectAlreadyExists();
    }

    public Product Get(int productID)
    {
        for (int i = 0; i < _arrProduct.Count(); i++)
        {
            if (_arrProduct[i].ID == productID)
            {
                return _arrProduct[i];
            }
        }
        throw new ObjectNotFound();
    }

    public IEnumerable<Product> GetAll()
    {
        if (_arrProduct.Count() == 0)
            try
            {
                throw new ObjectNotFound();
            }
            catch(Exception)
            {
                throw new ObjectNotFound();
            }
        List<Product> _ProductsShow = new List<Product>();
        for (int i = 0; i < _arrProduct.Count(); i++)
        {
            DO.Product tempProduct = new();
            tempProduct.ID = _arrProduct[i].ID;
            tempProduct.Name = _arrProduct[i].Name;
            _ProductsShow.Add(tempProduct);
        }
        return _ProductsShow;
    }

    public int Update(Product product)
    {
        for (int i = 0; i < _arrProduct.Count(); i++)
        {
            if (_arrProduct[i].ID == product.ID)
            {
                _arrProduct[i] = product;
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
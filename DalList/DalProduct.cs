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
            if (Config.IndexProduct < _arrProduct.Count)
            {
                _arrProduct[Config.IndexProduct++] = product;
            }
            else
            {
                throw new ArrayIsFull();
                //throw new Exception("the array is full.");
            }
            return product.ID;
        }
        throw new ObjectAlreadyExists();
        //throw new Exception("ID is already exsist.");
    }

    public Product Get(int productID)
    {
        for (int i = 0; i < Config.IndexProduct; i++)
        {
            if (_arrProduct[i].ID == productID)
            {
                return _arrProduct[i];
            }
        }
        throw new ObjectNotFound();
        //throw new Exception("id is not exsist");
    }

    public IEnumerable<Product> GetAll()
    {
        if (Config.IndexProduct == 0)
            try
            {
                throw new ObjectNotFound();
            }
            catch(Exception)
            {
                throw new ObjectNotFound();
            }
        //throw new Exception("the products dont exist yet");
        List<Product> _ProductsShow = new List<Product>();
        for (int i = 0; i < Config.IndexProduct; i++)
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
        for (int i = 0; i < Config.IndexProduct; i++)
        {
            if (_arrProduct[i].ID == product.ID)
            {
                _arrProduct[i] = product;
            }
        }
        throw new ObjectNotFound();
        //throw new Exception("product is not exsist");
    }

    public void Delete(int productIndex)
    {
        for (int i = 0; i < Config.IndexProduct; i++)
        {
            if (_arrProduct[i].ID == productIndex)
            {
                //_arrProduct[i] = null;
                _arrProduct.RemoveAt(i);
                return;
            }
        }
        throw new ObjectNotFound();
        //throw new Exception("product is not exsist");
    }
}
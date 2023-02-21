using DO;
using static Dal.DataSource;
using DalApi;
using System.Runtime.CompilerServices;

namespace Dal;

/// <summary>
/// DalProduct class.
/// </summary>

public class DalProduct: IProduct
{
    [MethodImpl(MethodImplOptions.Synchronized)]
    public int Add(Product product)
    {
        Product existingProduct = _arrProduct.Find(item => item.Name == product.Name && item.Category == product.Category);
        if (existingProduct.ID > 0)
            throw new ObjectAlreadyExists();
        product.ID = Config.ProductID++;
        _arrProduct.Add(product);
        return product.ID;
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public Product Get(int productID)
    {
        Product product = _arrProduct.Find(product => product.ID == productID);
        if (product.ID == 0)
            throw new ObjectNotFound();
        return product;
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public Product Get(Predicate<Product> func)
    {
        Product product = _arrProduct.Find(func);
        if (product.ID == 0)
            throw new ObjectNotFound();
        return product;
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<Product> GetAll(Func<Product, bool>? func = null) => func == null ? _arrProduct : _arrProduct.Where(func);

    [MethodImpl(MethodImplOptions.Synchronized)]
    public int Update(Product product)
    {
        Product item = _arrProduct.Find(item => item.ID == product.ID);
        int itemIndex = _arrProduct.IndexOf(item);
        _arrProduct[itemIndex] = product;
        return item.ID;
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(int productIndex)
    {
        Product item = _arrProduct.Find(item => item.ID == productIndex);
        _arrProduct.Remove(item);
        return;
    }
}
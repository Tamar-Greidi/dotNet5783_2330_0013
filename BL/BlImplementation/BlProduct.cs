//using BO;
//using DalApi;

using BO;
using DalApi;
using System;
using System.Linq;

namespace BlImplementation;

/// <summary>
/// Implementation of the Product class.
/// </summary>
internal class BlProduct : BlApi.IProduct
{
    private IDal Dal = DalApi.Factory.Get();

    /////<summary>
    ///// Requesting a list of products from the data layer (for director screen).
    ///// </summary>
    ///// <returns></returns>
    //public IEnumerable<BO.ProductForList> GetProducts()
    //{
    //    IEnumerable<DO.Product> DProducts = Dal.Product.GetAll();
    //    List<BO.ProductForList> BProductList = new List<BO.ProductForList>();
    //    foreach (DO.Product Product in DProducts) //Building on the database a logical entity type product list
    //    {
    //        BO.ProductForList product = new BO.ProductForList()
    //        {
    //            ID = Product.ID,
    //            Name = Product.Name,
    //            Price = Product.Price,
    //            Category = (BO.categories)Product.Category
    //        };
    //        BProductList.Add(product);
    //    }
    //    return BProductList;
    //}

    /// <summary>
    /// Request a list of products from the data layer (for customer screen).
    /// </summary>
    /// <returns></returns>
    public IEnumerable<BO.ProductItem> GetAll(Func<DO.Product, bool>? func = null)
    {
        IEnumerable<DO.Product> products = Dal.Product.GetAll();
        List<BO.ProductItem> productsItems = new List<BO.ProductItem>();
        foreach (DO.Product product in products)
        {
            BO.ProductItem productItem = new BO.ProductItem()
            {
                ID = product.ID,
                Name = product.Name,
                Price = product.Price,
                Category = (BO.categories)product.Category,
                Amount = 0,
                InStock = product.InStock > 0 ? true : false
            };
            productsItems.Add(productItem);
        }
        return productsItems;
    }

    /// <summary>
    /// Request a list of products from the data layer ///(for customer screen).
    /// </summary>
    /// <returns></returns>
    public IEnumerable<BO.ProductForList> GetCatalog(Func<DO.Product, bool>? func = null)
    {
        IEnumerable<DO.Product> DProducts = Dal.Product.GetAll(func);
        List<BO.ProductForList> BProductList = new List<BO.ProductForList>();
        foreach (DO.Product Product in DProducts) //Building on the database a logical entity type product list
        {
            BO.ProductForList product = new BO.ProductForList()
            {
                ID = Product.ID,
                Name = Product.Name,
                Price = Product.Price,
                Category = (BO.categories)Product.Category,
            };
            BProductList.Add(product);
        }
        return BProductList;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="category"></param>
    /// <returns></returns>
    public IEnumerable<BO.ProductForList> GetListByCategory(BO.categories category)
    {
        return GetCatalog(item => item.Category == (DO.categories)category);
    }

    /// <summary>
    /// Product details request.
    /// </summary>
    /// <param name="productID"></param>
    /// <returns></returns>
    /// <exception cref="BO.DalException"></exception>
    /// <exception cref="BO.InvalidData"></exception>
    public BO.Product GetProductDetails(int productID)
    {
        try
        {
            DO.Product readProduct = Dal.Product.Get(productID);
            BO.Product newProduct = new BO.Product()
            {
                ID = readProduct.ID,
                Name = readProduct.Name,
                Price = readProduct.Price,
                Category = (BO.categories)readProduct.Category,
                InStock = readProduct.InStock
            };
            return newProduct;
        }
        catch (DalApi.ObjectNotFound ex) //לא קיים כזה
        {
            throw new BO.DalException(ex);
        }
        throw new BO.InvalidData();
    }

    /// <summary>
    /// Product details request (for customer screen).
    /// </summary>
    /// <param name="productID"></param>
    /// <param name="cart"></param>
    /// <returns></returns>
    /// <exception cref="BO.DalException"></exception>
    /// <exception cref="BO.InvalidData"></exception>
    public BO.ProductItem GetProductDetails(int productID, Cart cart)
    {
        try
        {
            DO.Product readProduct = Dal.Product.Get(productID);
            BO.ProductItem newProduct = new BO.ProductItem()
            {
                ID = readProduct.ID,
                Name = readProduct.Name,
                Price = readProduct.Price,
                Category = (BO.categories)readProduct.Category,
                //Amount = ?איך יודעים כמה
                InStock = readProduct.InStock > 0 ? true : false
            };
            return newProduct;
        }
        catch (DalApi.ObjectNotFound ex) //לא קיים כזה
        {
            throw new BO.DalException(ex);
        }
        throw new BO.InvalidData();
    }

    /// <summary>
    /// Adding a product.
    /// </summary>
    /// <param name="product"></param>
    /// <exception cref="BO.DalException"></exception>
    public void Add(BO.Product product)
    {
        if (product.ID < 0 || product.Name == "" || product.Price < 0 || product.InStock < 0)
        {
            throw new BO.InvalidData(); //אחד מהנתונים שגוי
        }
        DO.Product addingProduct = new DO.Product()
        {
            ID = product.ID,
            Name = product.Name,
            Price = product.Price,
            Category = (DO.categories)product.Category,
            InStock = product.InStock
        };
        try
        {
            Dal.Product.Add(addingProduct);
        }
        catch (DalApi.ObjectAlreadyExists ex)
        {
            throw new BO.DalException(ex);
        }
        catch (DalApi.ObjectNotFound ex)
        {
            throw new BO.DalException(ex);
        }
    }

    /// <summary>
    /// Product deletion.
    /// </summary>
    /// <param name="productID"></param>
    /// <exception cref="BO.ObjectNotFound"></exception>
    /// <exception cref="BO.DalException"></exception>
    public void Delete(int productID)
    {
        IEnumerable<DO.OrderItem> ItemsInOrder = Dal.OrderItem.GetAll();
        foreach (DO.OrderItem orderItem in ItemsInOrder)
        {
            if (orderItem.ProductID == productID)
            {
                throw new BO.ObjectNotFound();
            }
        }
        try { Dal.Product.Delete(productID); }
        catch (DalApi.ObjectNotFound ex)
        {
            throw new BO.DalException(ex);
        }
    }

    /// <summary>
    /// Product update.
    /// </summary>
    /// <param name="product"></param>
    /// <exception cref="BO.InvalidData"></exception>
    /// <exception cref="BO.DalException"></exception>
    public void Update(BO.Product product)
    {
        if (product.ID < 0 || product.Name == "" || product.Price < 0 || product.InStock < 0)
        {
            throw new BO.InvalidData();
        }
        DO.Product updatingProduct = new DO.Product()
        {
            ID = product.ID,
            Name = product.Name,
            Price = product.Price,
            Category = (DO.categories)product.Category,
            InStock = product.InStock
        };
        try
        {
            Dal.Product.Update(updatingProduct);
        }
        catch (DalApi.ObjectNotFound ex)
        {
            throw new BO.DalException(ex);
        }
    }
}
//using BO;
//using DalApi;

//using BO;

namespace BlImplementation;

/// <summary>
/// Implementation of the Product class.
/// </summary>
internal class BlProduct : BlApi.IProduct
{
    DalApi.IDal Dal = new Dal.DalList();

    ///Requesting a list of products from the data layer (for director screen).
    public IEnumerable<BO.ProductForList> GetProducts()
    {
        //IEnumerable<DO.Product> DProducts = new List<DO.Product>();
        IEnumerable<DO.Product> DProducts = Dal.Product.GetAll();
        List<BO.ProductForList> BProductList = new List<BO.ProductForList>();
        foreach (DO.Product Product in DProducts) //Building on the database a logical entity type product list
        {
            BO.ProductForList product = new BO.ProductForList()
            {
                ID = Product.ID,
                Name = Product.Name,
                Price = Product.Price,
                Category = (BO.categories)Product.Category
            };
            BProductList.Add(product);
        }
        return BProductList;
    }

    ///Request a list of products from the data layer (for customer screen).
    public IEnumerable<BO.ProductItem> GetCatalog()
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
                InStock = product.InStock > 0 ? true : false,
                Amount = 0
            };
            productsItems.Add(productItem);
        }
        return productsItems;

    }

    ///Product details request.
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

    ///Product details request.
    public BO.Product GetProductDetails(int productID, BO.Cart cart)
    {
        throw new NotImplementedException();
    }

    ///Adding a product.
    public void Add(BO.Product product)
    {
        try
        {
            if (product.ID < 0 || product.Name == "" || product.Price < 0 || product.InStock < 0)
            {
                throw new BO.InvalidData(); //אחד מהנתונים שגוי
            }
        }
        catch (BO.InvalidData ex)
        {
            throw ex;
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

    ///Product deletion.
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

    ///Product update.
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
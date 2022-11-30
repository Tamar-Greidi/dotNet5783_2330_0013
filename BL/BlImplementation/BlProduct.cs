
using DalApi;

namespace BlImplementation;

internal class BlProduct : BlApi.IProduct
{
    IDal Dal = new Dal.DalList();
    ///Requesting a list of products from the data layer (for director screen).
    public IEnumerable<BO.ProductForList> GetProducts()
    {
        IEnumerable<DO.Product> DListOfProducts = Dal.Product.GetAll();
        List<BO.ProductForList> BProductList = new List<BO.ProductForList>();
        foreach (DO.Product Product in DListOfProducts) //Building on the database a logical entity type product list
        {
            BO.ProductForList productForList = new BO.ProductForList()
            {
                ID = Product.ID,
                Name = Product.Name,
                Price = Product.Price,
                Category = (BO.categories)Product.Category
            };
            BProductList.Add(productForList);
        }
        return BProductList;
    }

    ///Request a list of products from the data layer (for customer screen).
    public IEnumerable<BO.ProductItem> GetCatalog()
    {
        IEnumerable<DO.Product> DListOfProducts = Dal.Product.GetAll();
        List<BO.ProductItem> BProductsItems = new List<BO.ProductItem>();
        foreach (DO.Product Product in DListOfProducts)
        {
            BO.ProductItem productItemForList = new BO.ProductItem()
            {
                ID = Product.ID,
                Name = Product.Name,
                Price = Product.Price,
                Category = (BO.categories)Product.Category,
                InStock = Product.InStock > 0 ? true : false,
                Amount = 0
            };
            BProductsItems.Add(productItemForList);
        }
        return BProductsItems;
    }

    ///Product details request.
    public BO.Product GetProductDetails(int productID)
    {
        if (productID >= 0)
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
        }
        else
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
        try { Dal.Product.Add(addingProduct); }
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

    ///Product data update.
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
using BO;
namespace BlApi;

/// <summary>
/// Product's interface.
/// </summary>
public interface IProduct
{
    //public IEnumerable<Product> GetProducts(Func<Product, bool>? func);
    public IEnumerable<ProductItem> GetAll(Func<DO.Product, bool>? func = null);
    public IEnumerable<ProductForList> GetCatalog(Func<DO.Product, bool>? func = null);
    public IEnumerable<BO.ProductItem> GetListProductItemByCategory(BO.categories category);
    public IEnumerable<BO.ProductForList> GetListProductForListByCategory(BO.categories category);
    public Product GetProductDetails(int productID);
    public ProductItem GetProductDetails(int productID, Cart cart);
    public void Add(Product product);
    public void Delete(int productID);
    public void Update(Product product);
}
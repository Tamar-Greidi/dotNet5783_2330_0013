using BO;
namespace BlApi;

/// <summary>
/// Product's interface.
/// </summary>
public interface IProduct
{
    ///public IEnumerable<Product> GetProducts();
    public IEnumerable<ProductForList> GetCatalog();
    public Product GetProductDetails(int productID);
    public ProductItem GetProductDetails(int productID, Cart cart);
    public void Add(Product product);
    public void Delete(int productID);
    public void Update(Product product);
}
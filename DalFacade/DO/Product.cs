namespace DO;

/// <summary>
/// Data Object/Product
/// </summary>

public struct Product
{
    public int ID { get; set; }
    public string? Name { get; set; }
    public double Price { get; set; }
    public categories Category { get; set; }
    public int InStock { get; set; }
    public override string ToString() => $@"
    Product ID={ID}:
        Name: {Name}, 
        Category: {Category}
        Price: {Price}
        Amount in stock: {InStock}
    ";
}
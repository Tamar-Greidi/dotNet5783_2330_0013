using DO;
using System;
namespace BO;
public class Product // Business Object/Product:
{
    public int ID { get; set; }
    public string? Name { get; set; }
    public double Price { get; set; }
    public categories? Category { get; set; }
    public int InStock { get; set; }
    public override string ToString() => $@"
        Product ID={ID}:
        Name: {Name}, 
        Price: {Price}
        Category: {Category}
        Amount in stock: {InStock}
    ";
}
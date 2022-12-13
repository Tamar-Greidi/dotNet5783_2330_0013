using DO;
using System;
namespace BO;

/// <summary>
/// Business Object/ProductForList
/// </summary>

public class ProductForList
{
	public int ID { get; set; }
	public string? Name { get; set; }
	public double Price { get; set; }
	public categories Category { get; set; }
	public override string ToString() => $@"
        Product ID={ID}:
        Name: {Name}, 
        Price: {Price}
        Category: {Category}
    ";
}
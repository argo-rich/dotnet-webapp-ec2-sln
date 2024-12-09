using Northwind.EntityModels;

namespace dotnet_webapp_ec2.Models;

public class ProductSearchModel
{
    public string? ProductName { get; set; }

    public int? CategoryId { get; set; }

    public string? PriceType { get; set; }

    public int? Price { get; set; }

    public string? RequestMethod { get; set; }

    public List<Category>? Categories { get; set; }

    public List<Product>? Products { get; set; }
}

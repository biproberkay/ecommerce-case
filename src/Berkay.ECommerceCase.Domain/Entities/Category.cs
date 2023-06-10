namespace Berkay.ECommerceCase.Domain.Entities;

using System.Text.Json.Serialization;

public class Category
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }

    // Product ilişkisi
    public ICollection<Product>? Products { get; set; }
}

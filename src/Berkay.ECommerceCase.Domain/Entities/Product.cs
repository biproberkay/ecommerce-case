namespace Berkay.ECommerceCase.Domain.Entities;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

public class Product
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }

    [Required(ErrorMessage = "Product name is required.")]
    public string Name { get; set; }

    [MaxLength(250, ErrorMessage = "Description can have a maximum of 250 characters.")]
    public string? Description { get; set; }

    [DataType(DataType.Currency)]
    [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a valid price.")]
    public decimal Price { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Please enter a valid stock quantity.")]
    public int Stock { get; set; }

    [Display(Name = "Created At")]
    public DateTime CreatedAt { get; set; }

    [Display(Name = "Updated At")]
    public DateTime UpdatedAt { get; set; }

    // Category ilişkisi
    public int CategoryId { get; set; }
    public Category Category { get; set; }

    // CartItem ilişkisi
    public ICollection<CartItem> CartItems { get; set; }
}


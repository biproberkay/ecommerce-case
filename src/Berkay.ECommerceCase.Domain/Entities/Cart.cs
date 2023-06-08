namespace Berkay.ECommerceCase.Domain.Entities;

using Berkay.ECommerceCase.Domain.Contracts;
using System.Text.Json.Serialization;

public class Cart : IDiscountable
{
    public int Id { get; set; }
    public decimal DiscountAmount { get; set; } = 0.00m;

    public decimal CalculatedAmount
    {
        get { return CartItems.Sum(item => item.CalculatedAmount * item.Quantity) - DiscountAmount; }
    }
    public List<CartItem> CartItems { get; set; }

    public string UserId { get; set; } // Kullanıcı ilişkisi için UserId

    public User User { get; set; } // Kullanıcı ilişkisi
}

namespace Berkay.ECommerceCase.Domain.Entities;

using Berkay.ECommerceCase.Domain.Contracts;
using Berkay.ECommerceCase.Domain.Events;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

public class Cart : BaseEntity, IDiscountable
{
    public override string Id { get => base.Id; set => base.Id = value; }
    [NotMapped] public decimal DiscountAmount { get; set; } = 0.00m;
    [NotMapped] public decimal? CalculatedAmount {
        get
        {
            if(CartItems is not null)
            {
                return CartItems.Sum(i => i.CalculatedAmount) - DiscountAmount;
            }
            else
            {
                return null;
            }
        }
    }
    public List<CartItem> CartItems { get; set; } = new();

    public string? UserId { get; set; } // Kullanıcı ilişkisi için UserId

    public User? User { get; set; } // Kullanıcı ilişkisi

}

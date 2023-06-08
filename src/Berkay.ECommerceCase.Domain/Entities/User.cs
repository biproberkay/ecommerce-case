namespace Berkay.ECommerceCase.Domain.Entities;

using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

public class User : IdentityUser<string>
{
    public override string Id { get; set; } = Guid.NewGuid().ToString();
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public DateTime RefreshTokenExpiryTime { get; set; }

    public override bool EmailConfirmed { get; set; } = true;

    public List<Cart> Carts { get; set; } // Kullanıcının sepetleri

}



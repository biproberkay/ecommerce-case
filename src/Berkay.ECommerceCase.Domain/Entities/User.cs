namespace Berkay.ECommerceCase.Domain.Entities;

using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

public class User : IdentityUser<string>
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }

    public override bool EmailConfirmed { get; set; } = true;

}



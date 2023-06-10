using Berkay.ECommerceCase.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Berkay.ECommerceCase.Domain.Entities
{
    public class CartItem :BaseEntity, IDiscountable
    {
        public override string Id { get => base.Id; set => base.Id = value; }//this is for inheriting data annotaion
        public int Quantity { get; set; }
        public string? ProductId { get; set; }
        public string? CartId { get; set; }

        [JsonIgnore]
        public Cart? Cart { get; set; }
        [JsonIgnore]
        public Product? Product { get; set; }

        [NotMapped]
        public decimal ProductPrice
        {
            get
            {
                if (Product is not null)
                    return Product.Price;
                else
                    return 0.00m;
            }
        }
        [NotMapped]
        public int CategoryId
        {
            get
            {
                if (Product is not null)
                    return Product.CategoryId;
                else
                    return 0;
            }
        }
        [NotMapped]
        public decimal DiscountAmount { get; set; }
        [NotMapped]
        public decimal CalculatedAmount => ProductPrice * Quantity - DiscountAmount;
    }
}

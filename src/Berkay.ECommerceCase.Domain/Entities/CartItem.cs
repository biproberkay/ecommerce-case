using Berkay.ECommerceCase.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Berkay.ECommerceCase.Domain.Entities
{
    public class CartItem : IDiscountable
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal CalculatedAmount => Product.Price * Quantity - DiscountAmount;
        public decimal DiscountAmount { get; set; } = 0.00m;
        public string ProductId { get; set; } = string.Empty;
        public int CartId { get; set; }

        public Cart Cart { get; set; }
        public Product Product { get; set; }
    }
}

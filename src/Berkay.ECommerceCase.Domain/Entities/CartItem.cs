﻿using Berkay.ECommerceCase.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Berkay.ECommerceCase.Domain.Entities
{
    public class CartItem : BaseEntity, IDiscountable
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
        public decimal? ProductPrice
        {
            get
            {
                if (Product is not null)
                    return Product.Price;
                else
                    return null;
            }
        }
        [NotMapped]
        public int? CategoryId
        {
            get
            {
                if (Product is not null)
                    return Product.CategoryId;
                else
                    return null;
            }
        }
        [NotMapped]
        public string? ProductName
        {
            get
            {
                if (Product is not null)
                    return Product.Name;
                else
                    return null;
            }
        }
        [NotMapped]
        public decimal DiscountAmount { get; set; } = 0.00m;
        [NotMapped]
        public decimal? CalculatedAmount
        {
            get
            {
                if (ProductPrice is not null)
                    return (decimal)ProductPrice * Quantity - DiscountAmount;
                else
                    return null;
            }
        }
    }
}

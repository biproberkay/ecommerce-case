using Berkay.ECommerceCase.Domain.Entities;
using Berkay.ECommerceCase.Domain.Events;
using Berkay.ECommerceCase.Persistance.Wrappers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Berkay.ECommerceCase.Application.Features.ProductFeature.Commands
{
    public class CartItemAddCommand : IRequest<CustomResult<int>>
    {
        [Required]
        public string? ProductId { get; set; }
        [Required]
        public string? CartId { get; set; }
        [Required]
        public int Quantity { get; set; }
        public partial class CartItemAddCommandHandler : IRequestHandler<CartItemAddCommand, CustomResult<int>>
        {
            private readonly IECommerceDbContext _context;

            public CartItemAddCommandHandler(IECommerceDbContext context)
            {
                _context = context;
            }

            public async Task<CustomResult<int>> Handle(CartItemAddCommand request, CancellationToken cancellationToken)
            {
                CartItem cartItem = new()
                {
                    CartId = request.CartId,
                    ProductId = request.ProductId,
                    Quantity = request.Quantity,
                };
                var stock = await _context.Products.Where(p => p.Id == request.ProductId).Select(p => p.Stock).FirstOrDefaultAsync();
                if (stock < request.Quantity)
                {
                    return await CustomResult<int>.FailAsync($" there is not enough product in stock. only {stock} product exixts");
                }
                _context.CartItems.Add(cartItem);
                var result = await _context.SaveChangesAsync(cancellationToken);

                return await CustomResult<int>.SuccessAsync(result);
            }
        }
    }
}

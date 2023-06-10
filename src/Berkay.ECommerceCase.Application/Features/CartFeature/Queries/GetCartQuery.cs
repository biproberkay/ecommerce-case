using Berkay.ECommerceCase.Application.Services;
using Berkay.ECommerceCase.Domain.Entities;
using Berkay.ECommerceCase.Domain.Events;
using Berkay.ECommerceCase.Persistance.Wrappers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Berkay.ECommerceCase.Application.Features.CartFeature.Queries
{
    public class GetCartQuery : IRequest<CustomResult<Cart>>
    {
        public string? UserId { get; set; }
        public partial class GetCartQueryHandler : IRequestHandler<GetCartQuery, CustomResult<Cart>>
        {
            private readonly IECommerceDbContext _context;
            private readonly ICurrentUserService _currentUserService;

            public GetCartQueryHandler(IECommerceDbContext context, ICurrentUserService currentUserService )
            {
                _context = context;
                _currentUserService = currentUserService;
            }

            public async Task<CustomResult<Cart>> Handle(GetCartQuery request, CancellationToken cancellationToken)
            {
                var cart = await _context.Carts.Include(c=>c.CartItems).ThenInclude(i=>i.Product).AsNoTracking()
                    .FirstAsync(c => c.UserId == _currentUserService.UserId, cancellationToken);
                cart = HandleDiscounts(cart);

                return await CustomResult<Cart>.SuccessAsync(cart);
            }
            public Cart HandleDiscounts(Cart cart)
            {
                if (cart.CartItems is not null)
                {
                    var discountByCategories = _context.DiscountByCategories.AsNoTracking().ToList();
                    var discountByCart = _context.DiscountByCarts.AsNoTracking()
                                        .Where(d => cart.CalculatedAmount >= d.MinTotalPrice)
                                        .OrderByDescending(d => (double)d.MinTotalPrice)
                                        .First();
                    foreach (var discountByCategory in discountByCategories)
                    {
                        if (cart.CartItems is not null)
                            if (cart.CartItems.Any(i => i.CategoryId == discountByCategory.CategoryId))
                            {
                                int totalQuantity = cart.CartItems.Where(i => i.CategoryId == discountByCategory.CategoryId).Sum(i => i.Quantity);
                                if (totalQuantity >= discountByCategory.MinQuantity)
                                {

                                    var cheapestOne = cart.CartItems.OrderBy(i => i.ProductPrice).First();//find lowest price
                                    if (cheapestOne is not null)
                                    {
                                        // discount only for one quantity of a cartitem object. not for all quantities 
                                        cheapestOne.DiscountAmount = cheapestOne.ProductPrice * discountByCategory.Percentage;

                                        var indexof = cart.CartItems.IndexOf(cart.CartItems.First(i => i.Id == cheapestOne.Id));
                                        cart.CartItems[indexof] = cheapestOne;
                                    }
                                }
                            }
                    }

                    //cart.CalculatedAmount = cart.CartItems.Sum(i => i.CalculatedAmount);

                    // only for the highest 
                    cart.DiscountAmount = cart.CalculatedAmount * discountByCart.Percentage;
                }
                return cart;
            }
        }
    }
}

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
                var cart = await _context.Carts
                                            .Include(c=>c.CartItems)
                                                .ThenInclude(i=>i.Product)
                                            .AsNoTracking()
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
                                        .FirstOrDefault();
                    foreach (var discountByCategory in discountByCategories)
                    {
                            if (cart.CartItems.Any(i => i.CategoryId == discountByCategory.CategoryId))
                            {
                                int totalQuantity = cart.CartItems.Where(i => i.CategoryId == discountByCategory.CategoryId).Sum(i => i.Quantity);
                                if (totalQuantity >= discountByCategory.MinQuantity)
                                {

                                    var categoryCheapestOne = cart.CartItems.Where(i=>i.CategoryId==discountByCategory.CategoryId).OrderBy(i => i.ProductPrice).FirstOrDefault();//find lowest price
                                    if (categoryCheapestOne?.ProductPrice is not null)
                                    {
                                        // discount only for one quantity of a cartitem object. not for all quantities 
                                        categoryCheapestOne.DiscountAmount = (decimal)categoryCheapestOne.ProductPrice * discountByCategory.Percentage;
                                    }
                                }
                            }
                    }

                    //cart.CalculatedAmount = cart.CartItems.Sum(i => i.CalculatedAmount);

                    // only for the highest 
                    if(cart.CalculatedAmount is not null)
                    {
                        if(discountByCart is not null)
                        {
                            cart.DiscountAmount = (decimal)cart.CalculatedAmount * discountByCart.Percentage;
                        }
                    }
                }
                return cart;
            }
        }
    }
}

using Berkay.ECommerceCase.Domain.Entities;
using Berkay.ECommerceCase.Persistance.Wrappers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Berkay.ECommerceCase.Application.Features.ProductFeature.Queries
{
    public class GetProductsQuery : IRequest<CustomResult<List<Product>>>
    {
        public partial class GetProductsQueryHandler :
            IRequestHandler<GetProductsQuery, CustomResult<List<Product>>>
        {
            private readonly IECommerceDbContext _context;

            public GetProductsQueryHandler(IECommerceDbContext context)
            {
                _context = context;
            }

            public async Task<CustomResult<List<Product>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
            {
                var data = await _context.Products.AsNoTracking().ToListAsync();
                return await CustomResult<List<Product>>.SuccessAsync(data);
            }
        }
    }
}

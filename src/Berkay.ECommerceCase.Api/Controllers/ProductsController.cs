using Berkay.ECommerceCase.Application.Features.ProductFeature.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Berkay.ECommerceCase.Api.Controllers
{
    /// <summary>
    /// Controller for Product entity
    /// </summary>
    public class ProductsController : ApiControllerBase
    {
        /// <summary>
        /// the endpoint for fetching all products
        /// </summary>
        /// <returns>Status 200 OK with data</returns>
        [HttpGet]
        public async Task<ActionResult> GetProducts()
        {
            return Ok(await Mediator.Send(new GetProductsQuery()));
        }
    }
}

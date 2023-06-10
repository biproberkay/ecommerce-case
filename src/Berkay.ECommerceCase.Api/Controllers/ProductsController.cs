using Berkay.ECommerceCase.Application.Features.CartFeature.Queries;
using Berkay.ECommerceCase.Application.Features.ProductFeature.Commands;
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

        /// <summary>
        /// the endpoint for add a product to Cart
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Status 200 OK</returns>
        [HttpPost("addtocart")]
        public async Task<ActionResult> AddToCart( CartItemAddCommand command)
        {
            return Ok( await Mediator.Send(command));
        }

        /// <summary>
        /// this endpoint gets cart while calculating discounts
        /// </summary>
        /// <returns>cart data with including CartItems</returns>
        [HttpGet("cart")]
        public async Task<ActionResult> Cart()
        {
            return Ok(await Mediator.Send(new GetCartQuery()));
        }
    }
}

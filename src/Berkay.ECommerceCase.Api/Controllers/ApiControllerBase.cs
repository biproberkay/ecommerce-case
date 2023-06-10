using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Berkay.ECommerceCase.Api.Controllers
{
    /// <summary>
    /// abstract base controoler
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ApiControllerBase : ControllerBase
    {
        private ISender? _mediator;
        /// <summary>
        /// sender mediator
        /// </summary>
        protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
    }
}

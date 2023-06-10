using Berkay.ECommerceCase.Application.HttpModels.Requests;
using Berkay.ECommerceCase.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Berkay.ECommerceCase.Api.Controllers
{
    /// <summary>
    /// Token controller
    /// </summary>
    [Route("api/token")]
    [ApiController]
    public class TokenController: ControllerBase
    {
        private readonly ITokenService _identityService;
        /// <summary>
        /// constractor
        /// </summary>
        /// <param name="identityService"></param>
        public TokenController(ITokenService identityService)
        {
            _identityService = identityService;
        }

        /// <summary>
        /// Get Token (Email, Password)
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Status 200 OK</returns>
        /// <remarks>
        /// Sample value of message https://medium.com/c-sharp-progarmming/xml-comments-swagger-net-core-a390942d3329
        /// 
        ///     {
        ///        "email": "john@anymail.com",
        ///        "password": "Qwerty123+"
        ///     }
        ///     
        /// </remarks>
        [HttpPost]
        public async Task<ActionResult> Get(TokenRequest model)
        {
            var response = await _identityService.LoginAsync(model);
            return Ok(response);
        }

        /// <summary>
        /// Refresh Token
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Status 200 OK</returns>
        [HttpPost("refresh")]
        public async Task<ActionResult> Refresh([FromBody] RefreshTokenRequest model)
        {
            var response = await _identityService.GetRefreshTokenAsync(model);
            return Ok(response);
        }
    }
}

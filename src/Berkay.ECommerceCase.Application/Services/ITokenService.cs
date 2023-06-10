using Berkay.ECommerceCase.Application.HttpModels.Requests;
using Berkay.ECommerceCase.Application.HttpModels.Responses;
using Berkay.ECommerceCase.Persistance.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Berkay.ECommerceCase.Application.Services
{
    public interface ITokenService
    {
        Task<CustomResult<TokenResponse>> LoginAsync(TokenRequest model);

        Task<CustomResult<TokenResponse>> GetRefreshTokenAsync(RefreshTokenRequest model);
    }
}

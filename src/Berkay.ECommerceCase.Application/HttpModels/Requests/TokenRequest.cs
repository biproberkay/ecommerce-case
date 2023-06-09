using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Berkay.ECommerceCase.Application.HttpModels.Requests
{
    /// <summary>
    /// Token Request
    /// </summary>
    public class TokenRequest
    {
        /// <summary>
        /// abc@abs.com
        /// </summary>
        [Required]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Pasword, just more than 6 character
        /// </summary>
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}

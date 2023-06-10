using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Berkay.ECommerceCase.Domain.Contracts
{
    public interface IDiscountable
    {
        decimal DiscountAmount { get; set; }
    }
}

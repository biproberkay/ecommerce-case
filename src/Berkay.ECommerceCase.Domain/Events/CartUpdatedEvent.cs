using Berkay.ECommerceCase.Domain.Contracts;
using Berkay.ECommerceCase.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Berkay.ECommerceCase.Domain.Events
{
    public class CartUpdatedEvent : BaseEvent
    {
        public CartUpdatedEvent(Cart item)
        {
            Item = item;
        }

        public Cart Item { get; }
    }
}

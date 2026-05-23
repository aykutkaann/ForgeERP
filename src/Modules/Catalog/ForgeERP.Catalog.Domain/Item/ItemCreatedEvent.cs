using ForgeERP.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForgeERP.Catalog.Domain.Item
{
    public class ItemCreatedEvent :IDomainEvent
    {
        public ItemId ItemId { get; }
        public DateTime OccurredOn { get; }

        public ItemCreatedEvent(ItemId itemId)
        {
            ItemId = itemId ?? throw new ArgumentNullException(nameof(itemId));
            OccurredOn = DateTime.UtcNow; 
        }
    }
}

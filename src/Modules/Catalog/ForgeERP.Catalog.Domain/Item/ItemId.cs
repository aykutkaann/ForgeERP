using ForgeERP.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForgeERP.Catalog.Domain.Item
{
    public class ItemId :StronglyTypedId
    {
        public ItemId(Guid value) : base(value) { }

        public static ItemId New() => new(Guid.NewGuid());

        private ItemId() { }
    }
}

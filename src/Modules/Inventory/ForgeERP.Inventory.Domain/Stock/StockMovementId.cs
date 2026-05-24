using ForgeERP.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForgeERP.Inventory.Domain.Stock
{
    public class StockMovementId : StronglyTypedId
    {
        private StockMovementId() { }

        public StockMovementId(Guid value) : base(value) { }

        public static StockMovementId New() => new(Guid.NewGuid());
    }
}

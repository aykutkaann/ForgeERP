using ForgeERP.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForgeERP.Inventory.Domain
{
    public class StockBalanceId :StronglyTypedId
    {
        private StockBalanceId() { }

        public StockBalanceId(Guid value) : base(value) { }

        public static StockBalanceId New() => new(Guid.NewGuid());
    }
}

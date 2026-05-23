using ForgeERP.Catalog.Domain.Item;
using ForgeERP.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForgeERP.Catalog.Domain.BOM
{
    public class BomLine: Entity<BomLineId>
    {
        public BomId BomId { get; private set; }
        public ItemId ComponentItemId { get; private set; }
        public decimal Quantity { get; private set; }
        public int Position { get; private set; }

        private BomLine() { }

        internal BomLine(BomId bomId, ItemId componentItemId, decimal quantity, int position)
            : base(BomLineId.New())
        {
            BomId = bomId;
            ComponentItemId = componentItemId ?? throw new ArgumentNullException(nameof(componentItemId));
            Quantity = quantity;
            Position = position;
        }


        internal Result UpdateQuantity(decimal quantity)
        {
            if (quantity <= 0)
                return Result.Failure("quantity must be greater than 0.");

            Quantity = quantity;

            return Result.Success();
        }

        internal Result UpdatePosition(int position)
        {
            
            Position = position;

            return Result.Success();
        }


    }
}

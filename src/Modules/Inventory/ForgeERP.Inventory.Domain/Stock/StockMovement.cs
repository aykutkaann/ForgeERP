using ForgeERP.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForgeERP.Inventory.Domain.Stock
{
    public class StockMovement : Entity<StockMovementId>
    {
        public StockBalanceId StockBalanceId { get; private set; } //which balance is affects
        public MovementType MovementType { get; private set; } // receipt, issue, adjustment
        public decimal Quantity { get; private set; } //How many
        public string Reason { get; private set; } // Why happened
        public DateTime OccurredOn { get; private set; } //when

        private StockMovement() { }

        internal StockMovement(StockBalanceId balanceId, MovementType movementType, decimal quantity, string reason)
            :base(StockMovementId.New())
        {
            StockBalanceId = balanceId;
            MovementType = movementType;
            Quantity = quantity;
            Reason = reason;
            OccurredOn = DateTime.UtcNow;
        }


    }
}

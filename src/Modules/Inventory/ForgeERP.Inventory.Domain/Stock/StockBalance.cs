using ForgeERP.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForgeERP.Inventory.Domain.Stock
{
    public class StockBalance :AggregateRoot<StockBalanceId>
    {
        private readonly List<StockMovement> _stockMovements = new();

        public Guid ItemId { get; private set; }
        public string Warehouse { get; private set; }
        public decimal QuantityReserved { get; private set; }
        public decimal QuantityOnHand { get; private set; }
        public decimal QuantityAvailable
        {
            get
            {
                return QuantityOnHand - QuantityReserved;
            }
        }

        public IReadOnlyList<StockMovement> StockMovements => _stockMovements.AsReadOnly();


        private StockBalance() { }

        private StockBalance(StockBalanceId balanceId,Guid itemId, string warehouse) : base(balanceId)
        {
            ItemId = itemId;
            Warehouse = warehouse;
            QuantityOnHand = 0;
            QuantityReserved = 0;
        }

        public static Result<StockBalance> Create(Guid itemId,string warehouse)
        {
            if (itemId == Guid.Empty)
                return Result<StockBalance>.Failure("Item id is required.");

            if (string.IsNullOrWhiteSpace(warehouse))
                return Result<StockBalance>.Failure("Warehouse is required.");

            var balance = new StockBalance(StockBalanceId.New(), itemId, warehouse);

            return Result<StockBalance>.Success(balance);
        }

        public  Result Receive(decimal quantity,string reason)
        {
            if (quantity <= 0)
                return Result.Failure("Balance must be positive.");
            if (string.IsNullOrWhiteSpace(reason))
                return Result.Failure("Reason cannot be empty.");

            QuantityOnHand += quantity;
            _stockMovements.Add(new StockMovement(Id,MovementType.Receipt,quantity,reason));

            return Result.Success();

        }

        public Result Issue(decimal quantity, string reason)
        {
            if (quantity <= 0)
                return Result.Failure("Balance must be positive.");

            if (quantity > QuantityAvailable)
                return Result.Failure("insufficient stock");
            if (string.IsNullOrWhiteSpace(reason))
                return Result.Failure("Reason cannot be empty.");

            QuantityOnHand -= quantity;

            _stockMovements.Add(new StockMovement(Id, MovementType.Issue, -quantity, reason));

            return Result.Success();

        }

        public Result Reserve(decimal quantity)
        {
            if (quantity <= 0)
                return Result.Failure("Balance must be positive.");

            if (quantity > QuantityAvailable)
                return Result.Failure("Not enogh stock to reserve");

            QuantityReserved += quantity;

            return Result.Success();
        }

        public Result ReleaseReservation(decimal quantity)
        {
            if (quantity <= 0)
                return Result.Failure("Balance must be positive.");

            if (quantity > QuantityReserved)
                return Result.Failure("Cant release more than what you have.");

            QuantityReserved -= quantity;
            return Result.Success();
        }

    }
}

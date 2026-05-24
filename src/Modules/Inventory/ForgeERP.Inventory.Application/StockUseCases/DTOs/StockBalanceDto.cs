using System;
using System.Collections.Generic;
using System.Text;

namespace ForgeERP.Inventory.Application.StockUseCases.DTOs
{
    public class StockBalanceDto
    {
        public Guid Id { get; init; }
        public Guid ItemId { get; init; }
        public string WareHouse { get; init; }
        public decimal QuantityOnHand { get; init; }
        public decimal QuantityReserved { get; init; }
        public decimal QuantityAvailable { get; init; }
    }
}

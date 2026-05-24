using ForgeERP.Inventory.Application.StockUseCases.DTOs;
using ForgeERP.Inventory.Application.StockUseCases.Interfaces;
using ForgeERP.Inventory.Application.StockUseCases.Queries;
using ForgeERP.Inventory.Domain;
using ForgeERP.SharedKernel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForgeERP.Inventory.Application.StockUseCases.Handlers
{
    public class GetStockBalanceHandler :IRequestHandler<GetStockBalanceQuery, Result<StockBalanceDto>>
    {
        private readonly IStockBalanceRepository _stockBalance;

        public GetStockBalanceHandler(IStockBalanceRepository stockBalance)
        {
            _stockBalance = stockBalance;
        }

        public async Task<Result<StockBalanceDto>> Handle(GetStockBalanceQuery request, CancellationToken cancellationToken)
        {
            var balance = await _stockBalance.GetByIdAsync(new StockBalanceId(request.Id), cancellationToken);
            if (balance == null)
                return Result<StockBalanceDto>.Failure("Cannot find stock balance.");

            var dto = new StockBalanceDto
            {
                Id = balance.Id.Value,
                ItemId = balance.ItemId,
                WareHouse = balance.Warehouse,
                QuantityOnHand = balance.QuantityOnHand,
                QuantityReserved = balance.QuantityReserved,
                QuantityAvailable = balance.QuantityAvailable
            };

            return Result<StockBalanceDto>.Success(dto);
        }
    }
}

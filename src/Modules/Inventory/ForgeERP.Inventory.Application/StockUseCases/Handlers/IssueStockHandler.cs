using ForgeERP.Inventory.Application.StockUseCases.Commands;
using ForgeERP.Inventory.Application.StockUseCases.DTOs;
using ForgeERP.Inventory.Application.StockUseCases.Interfaces;
using ForgeERP.Inventory.Domain;
using ForgeERP.Inventory.Domain.Stock;
using ForgeERP.SharedKernel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForgeERP.Inventory.Application.StockUseCases.Handlers
{
    public class IssueStockHandler : IRequestHandler<IssueStockCommand, Result<StockBalanceDto>>
    {
        private readonly IStockBalanceRepository _stockBalance;
        private readonly IInventoryUnitOfWork _uow;

        public IssueStockHandler(IStockBalanceRepository stockBalance, IInventoryUnitOfWork uow)
        {
            _stockBalance = stockBalance;
            _uow = uow;
        }

        public async Task<Result<StockBalanceDto>> Handle(IssueStockCommand request, CancellationToken cancellationToken)
        {
            var stockBalance = await _stockBalance.GetByIdAsync(new StockBalanceId(request.StockBalanceId), cancellationToken);
            if (stockBalance == null)
                return Result<StockBalanceDto>.Failure("Could not find stock balance.");

            var result = stockBalance.Issue(request.Quantity, request.Reason);

            if (result.IsFailure)
                return Result<StockBalanceDto>.Failure(result.Error);
     

            await _uow.SaveChangesAsync(cancellationToken);

            var dto = new StockBalanceDto
            {
                Id = stockBalance.Id.Value,
                ItemId = stockBalance.ItemId,
                WareHouse = stockBalance.Warehouse,
                QuantityOnHand = stockBalance.QuantityOnHand,
                QuantityReserved = stockBalance.QuantityReserved,
                QuantityAvailable = stockBalance.QuantityAvailable
            };

            return Result<StockBalanceDto>.Success(dto);
        }
    }
}

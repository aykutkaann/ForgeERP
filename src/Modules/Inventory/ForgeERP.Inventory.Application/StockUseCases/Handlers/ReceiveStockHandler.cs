using ForgeERP.Inventory.Application.StockUseCases.Commands;
using ForgeERP.Inventory.Application.StockUseCases.DTOs;
using ForgeERP.Inventory.Application.StockUseCases.Interfaces;
using ForgeERP.Inventory.Domain;
using ForgeERP.SharedKernel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForgeERP.Inventory.Application.StockUseCases.Handlers
{
    public class ReceiveStockHandler :IRequestHandler<ReceiveStockCommand, Result<StockBalanceDto>>
    {
        private readonly IStockBalanceRepository _stockBalance;
        private readonly IInventoryUnitOfWork _uow;

        public ReceiveStockHandler(IStockBalanceRepository stockBalance, IInventoryUnitOfWork uow)
        {
            _stockBalance = stockBalance;
            _uow = uow;
        }

        public async Task<Result<StockBalanceDto>> Handle(ReceiveStockCommand request, CancellationToken cancellationToken)
        {
            var balance = await _stockBalance.GetByIdAsync(new StockBalanceId(request.StockBalanceId), cancellationToken);

            if (balance == null)
                return Result<StockBalanceDto>.Failure("Cannot find stock balance.");

            var result = balance.Receive(request.Quantity,request.Reason);

            if (result.IsFailure)
                return Result<StockBalanceDto>.Failure(result.Error);
            

            await _uow.SaveChangesAsync(cancellationToken);

            var dto = new StockBalanceDto
            {
                Id = balance.Id.Value,
                ItemId =balance.ItemId,
                WareHouse = balance.Warehouse,
                QuantityOnHand = balance.QuantityOnHand,
                QuantityReserved = balance.QuantityReserved,
                QuantityAvailable = balance.QuantityAvailable
            };
    
            return Result<StockBalanceDto>.Success(dto);


        }
    }
}

using ForgeERP.Inventory.Application.StockUseCases.Commands;
using ForgeERP.Inventory.Application.StockUseCases.DTOs;
using ForgeERP.Inventory.Application.StockUseCases.Interfaces;
using ForgeERP.Inventory.Domain.Stock;
using ForgeERP.SharedKernel;
using MediatR;


namespace ForgeERP.Inventory.Application.StockUseCases.Handlers
{
    public class CreateStockBalanceHandler :IRequestHandler<CreateStockBalanceCommand, Result<StockBalanceDto>>
    {
        private readonly IStockBalanceRepository _stockBalance;
        private readonly IUnitOfWork _uow;
        public CreateStockBalanceHandler(IStockBalanceRepository stockBalance, IUnitOfWork uow)
        {
            _stockBalance = stockBalance;
            _uow = uow;
        }

        public async Task<Result<StockBalanceDto>> Handle(CreateStockBalanceCommand request, CancellationToken cancellationToken)
        {
            var existing = await _stockBalance.GetByItemAndWarehouseAsync(request.ItemId, request.Warehouse);
            if (existing != null)
                return Result<StockBalanceDto>.Failure("Already has an stock record for this item and warehouse.");

            var result = StockBalance.Create(request.ItemId, request.Warehouse);

            if (result.IsFailure)
                return Result<StockBalanceDto>.Failure(result.Error);

            var stockBalance = result.Value;

            await _stockBalance.AddAsync(stockBalance, cancellationToken);
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

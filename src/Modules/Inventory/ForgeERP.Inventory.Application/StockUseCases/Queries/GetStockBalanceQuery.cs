using ForgeERP.Inventory.Application.StockUseCases.DTOs;
using ForgeERP.SharedKernel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForgeERP.Inventory.Application.StockUseCases.Queries
{

    public record GetStockBalanceQuery(Guid Id) : IRequest<Result<StockBalanceDto>>;
}

using ForgeERP.Inventory.Application.StockUseCases.DTOs;
using ForgeERP.SharedKernel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForgeERP.Inventory.Application.StockUseCases.Commands
{

    public record IssueStockCommand(Guid StockBalanceId, decimal Quantity, string Reason) : IRequest<Result<StockBalanceDto>>;
}

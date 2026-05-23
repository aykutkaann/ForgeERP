using ForgeERP.Catalog.Application.BomUseCases.DTOs;
using ForgeERP.SharedKernel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForgeERP.Catalog.Application.BomUseCases.Commands
{

    public record AddBomLineCommand(Guid BomId, Guid ComponentItemId, decimal Quantity, int Position) : IRequest<Result<BomLineDto>>;
}

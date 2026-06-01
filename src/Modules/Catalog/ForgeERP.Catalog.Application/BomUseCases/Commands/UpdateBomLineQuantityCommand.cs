using ForgeERP.Catalog.Application.BomUseCases.DTOs;
using ForgeERP.SharedKernel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForgeERP.Catalog.Application.BomUseCases.Commands
{

    public record UpdateBomLineQuantityCommand(Guid BomId ,Guid ComponentId, decimal Quantity) : IRequest<Result<BomLineDto>>;
}

using ForgeERP.Catalog.Application.BomUseCases.DTOs;
using ForgeERP.SharedKernel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForgeERP.Catalog.Application.BomUseCases.Queries
{
    public record GetBomByIdQuery(Guid Id) : IRequest<Result<BomDto>>;
    
    
}

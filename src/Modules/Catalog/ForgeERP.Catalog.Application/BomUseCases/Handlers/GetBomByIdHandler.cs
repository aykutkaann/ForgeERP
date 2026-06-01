using ForgeERP.Catalog.Application.BomUseCases.DTOs;
using ForgeERP.Catalog.Application.BomUseCases.Interfaces;
using ForgeERP.Catalog.Application.BomUseCases.Queries;
using ForgeERP.Catalog.Domain.BOM;
using ForgeERP.SharedKernel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForgeERP.Catalog.Application.BomUseCases.Handlers
{
    public class GetBomByIdHandler :IRequestHandler<GetBomByIdQuery, Result<BomDto>>
    {
        private readonly IBomRepository _bomRepo;

        public GetBomByIdHandler(IBomRepository bomRepo)
        {
            _bomRepo = bomRepo;
        }

        public async Task<Result<BomDto>> Handle(GetBomByIdQuery request, CancellationToken cancellationToken)
        {
            var bomId = new BomId(request.Id);
            var bom = await _bomRepo.GetByIdAsync(bomId, cancellationToken);


            if (bom == null)
                return Result<BomDto>.Failure("BOM not found.");

            var lines = bom.BomLines.Select(line => new BomLineDto
            {
                Id = line.Id.Value,
                ComponentItemId = line.ComponentItemId.Value,
                Quantity = line.Quantity,
                Position = line.Position
            }).ToList();

            var dto = new BomDto
            {
                Id = bom.Id.Value,
                ItemId = bom.ItemId.Value,
                Name = bom.Name,
                Lines = lines

            };

            return Result<BomDto>.Success(dto);

        }
    }
}

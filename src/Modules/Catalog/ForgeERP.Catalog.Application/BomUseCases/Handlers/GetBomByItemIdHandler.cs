using ForgeERP.Catalog.Application.BomUseCases.DTOs;
using ForgeERP.Catalog.Application.BomUseCases.Interfaces;
using ForgeERP.Catalog.Application.BomUseCases.Queries;
using ForgeERP.Catalog.Domain.Item;
using ForgeERP.SharedKernel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForgeERP.Catalog.Application.BomUseCases.Handlers
{
    public class GetBomByItemIdHandler :IRequestHandler<GetBomByItemIdQuery, Result<BomDto>>
    {
        private readonly IBomRepository _bomRepo;

        public GetBomByItemIdHandler(IBomRepository bomRepo)
        {
            _bomRepo = bomRepo;
        }

        public async Task<Result<BomDto>> Handle(GetBomByItemIdQuery request, CancellationToken cancellationToken)
        {
            var itemId = new ItemId(request.ItemId);

            var bom = await _bomRepo.GetByItemIdAsync(itemId, cancellationToken);

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

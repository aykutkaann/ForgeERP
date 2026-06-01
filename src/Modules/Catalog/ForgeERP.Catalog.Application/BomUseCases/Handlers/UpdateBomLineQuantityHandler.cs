using ForgeERP.Catalog.Application.BomUseCases.Commands;
using ForgeERP.Catalog.Application.BomUseCases.DTOs;
using ForgeERP.Catalog.Application.BomUseCases.Interfaces;
using ForgeERP.Catalog.Domain.BOM;
using ForgeERP.Catalog.Domain.Item;
using ForgeERP.SharedKernel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForgeERP.Catalog.Application.BomUseCases.Handlers
{
    public class UpdateBomLineQuantityHandler : IRequestHandler<UpdateBomLineQuantityCommand, Result<BomLineDto>>
    {
        private readonly IBomRepository _bomRepo;
        private readonly ICatalogUnitOfWork _uow;

        public UpdateBomLineQuantityHandler(IBomRepository bomRepo, ICatalogUnitOfWork uow)
        {
            _bomRepo = bomRepo;
            _uow = uow;
        }

        public async Task<Result<BomLineDto>> Handle(UpdateBomLineQuantityCommand request, CancellationToken cancellationToken)
        {
            var bomId = new BomId(request.BomId);
            var bom = await _bomRepo.GetByIdAsync(bomId, cancellationToken);
            if (bom == null)
                return Result<BomLineDto>.Failure("BOM not Found.");

            var componentId = new ItemId(request.ComponentId);

            var result = bom.UpdateLineQuantity(componentId, request.Quantity);

            if (result.IsFailure)
                return Result<BomLineDto>.Failure(result.Error);

            await _uow.SaveChangesAsync(cancellationToken);


            var updatedLine = bom.BomLines.First(x => x.ComponentItemId == componentId);

            var dto = new BomLineDto
            {
                Id = bom.Id.Value,
                ComponentItemId = bom.ItemId.Value,
                Quantity = updatedLine.Quantity,
                Position = updatedLine.Position

            };

            return Result<BomLineDto>.Success(dto);
        }
    }
}

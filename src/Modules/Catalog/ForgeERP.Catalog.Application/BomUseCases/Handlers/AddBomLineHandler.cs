using ForgeERP.Catalog.Application.BomUseCases.Commands;
using ForgeERP.Catalog.Application.BomUseCases.DTOs;
using ForgeERP.Catalog.Application.BomUseCases.Interfaces;
using ForgeERP.Catalog.Application.ItemUseCases.DTOs;
using ForgeERP.Catalog.Application.ItemUseCases.Interfaces;
using ForgeERP.Catalog.Domain.BOM;
using ForgeERP.Catalog.Domain.Item;
using ForgeERP.SharedKernel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForgeERP.Catalog.Application.BomUseCases.Handlers
{
    public class AddBomLineHandler : IRequestHandler<AddBomLineCommand, Result<BomLineDto>>
    {

        private readonly IBomRepository _bomRepository;
        private readonly ICatalogUnitOfWork _uow;
        public AddBomLineHandler(IBomRepository bomRepository, ICatalogUnitOfWork uow)
        {
            _bomRepository = bomRepository;
            _uow = uow;
        }

        public async Task<Result<BomLineDto>> Handle(AddBomLineCommand request, CancellationToken cancellationToken)
        {
            var bomId = new BomId(request.BomId);
            var bom = await _bomRepository.GetByIdAsync(bomId, cancellationToken);

            if (bom == null)
                return Result<BomLineDto>.Failure("BOM not Found.");

            var componentItemId = new ItemId(request.ComponentItemId);

            var result = bom.AddLine(componentItemId, request.Quantity, request.Position);
            if (result.IsFailure)
                return Result<BomLineDto>.Failure(result.Error);

            await _uow.SaveChangesAsync(cancellationToken);

            var newLine = result.Value;
            var bomLineDto = new BomLineDto
            {
                Id = newLine.Id.Value,
                ComponentItemId = newLine.ComponentItemId.Value,
                Quantity = newLine.Quantity,
                Position = newLine.Position
            };

            return Result<BomLineDto>.Success(bomLineDto);
        }
    }
}

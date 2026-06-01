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
    public class RemoveBomLineHandler :IRequestHandler<RemoveBomLineCommand, Result>
    {
        private readonly IBomRepository _bomRepo;
        private readonly ICatalogUnitOfWork _uow;


        public RemoveBomLineHandler(IBomRepository bomRepo, ICatalogUnitOfWork uow)
        {
            _bomRepo = bomRepo;
            _uow = uow;
        }

        public async Task<Result> Handle(RemoveBomLineCommand request, CancellationToken cancellationToken)
        {
            var bomId = new BomId(request.BomId);
            var bom = await _bomRepo.GetByIdAsync(bomId, cancellationToken);
            if (bom == null)
                return Result<BomLineDto>.Failure("BOM not Found.");

            var componentId = new ItemId(request.ComponentId);

            var result = bom.RemoveLine(componentId);

            if (result.IsFailure)
                return Result<BomLineDto>.Failure(result.Error);

            await _uow.SaveChangesAsync(cancellationToken);
      

            return Result.Success();

        }
    }
}

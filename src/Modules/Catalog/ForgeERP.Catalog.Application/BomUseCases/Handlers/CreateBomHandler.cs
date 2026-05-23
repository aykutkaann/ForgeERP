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
    public class CreateBomHandler: IRequestHandler<CreateBomCommand, Result<BomDto>>
    {
        private readonly IBomRepository _bomRepository;
        private readonly IUnitOfWork _uow;

        public CreateBomHandler(IBomRepository bomRepository, IUnitOfWork uow)
        {
            _bomRepository = bomRepository;
            _uow = uow;
        }

        public async Task<Result<BomDto>> Handle(CreateBomCommand request, CancellationToken cancellationToken)
        {

            var itemId = new ItemId(request.itemId);
            var bomResult = Bom.Create(itemId, request.name);

            if (bomResult.IsFailure)
                return Result<BomDto>.Failure(bomResult.Error);

            await _bomRepository.AddAsync(bomResult.Value,cancellationToken);

            await _uow.SaveChangesAsync(cancellationToken);

            var bom = bomResult.Value;

            var bomDto = new BomDto
            {
                Id = bom.Id.Value,
                ItemId = bom.ItemId.Value,
                Name = bom.Name,
                Lines = new List<BomLineDto>()
            };

            return Result<BomDto>.Success(bomDto);

        }
    }
}

using ForgeERP.Catalog.Application.ItemUseCases.Commands;
using ForgeERP.Catalog.Application.ItemUseCases.DTOs;
using ForgeERP.Catalog.Application.ItemUseCases.Interfaces;
using ForgeERP.Catalog.Domain.Item;
using ForgeERP.SharedKernel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForgeERP.Catalog.Application.ItemUseCases.Handlers
{

    public class ActivateItemHandler : IRequestHandler<ActivateItemCommand, Result<ItemDto>>
    {
        private readonly IItemRepository _itemRepo;
        private readonly ICatalogUnitOfWork _uow;

        public ActivateItemHandler(IItemRepository itemRepo, ICatalogUnitOfWork uow)
        {
            _itemRepo = itemRepo;
            _uow = uow;
        }

        public async Task<Result<ItemDto>> Handle(ActivateItemCommand request, CancellationToken cancellationToken)
        {
            var itemId = new ItemId(request.Id);

            var item = await _itemRepo.GetByIdAsync(itemId, cancellationToken);

            if (item == null)
                return Result<ItemDto>.Failure("Item not found.");

            item.Activate();

            await _uow.SaveChangesAsync(cancellationToken);

            var dto = new ItemDto
            {
                Id = item.Id.Value,
                ItemCode = item.ItemCode,
                Name = item.Name,
                UnitOfMeasure = item.UnitOfMeasure,
                ItemType = item.ItemType.ToString(),
                IsActive = item.IsActive
            };

            return Result<ItemDto>.Success(dto);
        }
    }
}

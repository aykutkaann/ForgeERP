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
    public class UpdateItemHandler : IRequestHandler<UpdateItemCommand, Result<ItemDto>>
    {

        private readonly IItemRepository _itemRepository;
        private readonly ICatalogUnitOfWork _uow;

        public UpdateItemHandler(IItemRepository itemRepository, ICatalogUnitOfWork uow)
        {
            _itemRepository = itemRepository;
            _uow = uow;

        }

        public async Task<Result<ItemDto>> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
        {
            var itemId = new ItemId(request.Id);
            var existingItem = await _itemRepository.GetByIdAsync(itemId, cancellationToken);
            if (existingItem == null)
                return Result<ItemDto>.Failure("Item not found");

           var result =   existingItem.UpdateDetails(request.Name, request.UnitOfMeasure);

            if (result.IsFailure)
                return Result<ItemDto>.Failure(result.Error);
            await _uow.SaveChangesAsync(cancellationToken);

            var dto = new ItemDto
            {
                Id = existingItem.Id.Value,
                ItemCode = existingItem.ItemCode,
                Name = existingItem.Name,
                UnitOfMeasure = existingItem.UnitOfMeasure,
                ItemType = existingItem.ItemType.ToString(),
                IsActive = existingItem.IsActive

            };

            return Result<ItemDto>.Success(dto);
        }

    }
}

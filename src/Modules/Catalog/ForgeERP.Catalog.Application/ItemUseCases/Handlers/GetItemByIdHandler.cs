using ForgeERP.Catalog.Application.ItemUseCases.DTOs;
using ForgeERP.Catalog.Application.ItemUseCases.Interfaces;
using ForgeERP.Catalog.Application.ItemUseCases.Queries;
using ForgeERP.Catalog.Domain.Item;
using ForgeERP.SharedKernel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForgeERP.Catalog.Application.ItemUseCases.Handlers
{
    public class GetItemByIdHandler : IRequestHandler<GetItemByIdQuery ,Result<ItemDto>>
    {
        private readonly IItemRepository _itemRepository;

        public GetItemByIdHandler(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task<Result<ItemDto>> Handle(GetItemByIdQuery request, CancellationToken cancellationToken)
        {
            var itemId = new ItemId(request.Id);
            var item = await _itemRepository.GetByIdAsync(itemId,cancellationToken);

            if (item == null)
                return Result<ItemDto>.Failure("Item not found.");

            var itemDto = new ItemDto
            {
                Id = item.Id.Value,
                ItemCode = item.ItemCode,
                Name = item.Name,
                UnitOfMeasure = item.UnitOfMeasure,
                ItemType = item.ItemType.ToString(),
                IsActive = item.IsActive
            };

            return Result<ItemDto>.Success(itemDto);
        }
    }
}

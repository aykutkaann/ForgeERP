using ForgeERP.Catalog.Application.ItemUseCases.DTOs;
using ForgeERP.Catalog.Application.ItemUseCases.Interfaces;
using ForgeERP.Catalog.Application.ItemUseCases.Queries;
using ForgeERP.SharedKernel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForgeERP.Catalog.Application.ItemUseCases.Handlers
{
    public class ListItemHandler : IRequestHandler<ListItemsQuery, Result<List<ItemDto>>>
    {
        private readonly IItemRepository _itemRepository;

        public ListItemHandler(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task<Result<List<ItemDto>>> Handle(ListItemsQuery request, CancellationToken cancellationToken)
        {
            var items = await _itemRepository.GetAllAsync(cancellationToken);


            var itemsDto = items.Select(item => new ItemDto
            {
                Id = item.Id.Value,
                ItemCode = item.ItemCode,
                Name = item.Name,
                UnitOfMeasure = item.UnitOfMeasure,
                IsActive = item.IsActive

            }).ToList();

            return Result<List<ItemDto>>.Success(itemsDto);
        }
    }
}

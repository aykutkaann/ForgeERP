using ForgeERP.Catalog.Application.ItemUseCases.Commands;
using ForgeERP.Catalog.Application.ItemUseCases.Interfaces;
using ForgeERP.Catalog.Domain.Item;
using ForgeERP.SharedKernel;
using MediatR;


namespace ForgeERP.Catalog.Application.ItemUseCases.Handlers
{
    public class CreateItemHandler : IRequestHandler<CreateItemCommand, Result<Guid>>
    {
        private readonly IItemRepository _itemRepository;
        private readonly ICatalogUnitOfWork _uow;
        public CreateItemHandler(IItemRepository itemRepository,ICatalogUnitOfWork uow)
        {
            _itemRepository = itemRepository;
            _uow = uow;
        }

        public async Task<Result<Guid>> Handle(CreateItemCommand request, CancellationToken cancellationToken)
        {
            var existingItem = await _itemRepository.GetByCodeAsync(request.ItemCode, cancellationToken);

            if (existingItem != null)
                return Result<Guid>.Failure($"Item that has item code {request.ItemCode} not found. ");

            var itemResult = Item.Create(request.ItemCode, request.Name, request.ItemType, request.UnitOfMeasure);

            if (itemResult.IsFailure)
                return Result<Guid>.Failure(itemResult.Error);

            await _itemRepository.AddAsync(itemResult.Value,cancellationToken);

            await _uow.SaveChangesAsync(cancellationToken);

            return Result<Guid>.Success(itemResult.Value.Id.Value);
        }
    }
}

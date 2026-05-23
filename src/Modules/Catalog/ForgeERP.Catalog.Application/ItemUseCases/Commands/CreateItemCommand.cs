using ForgeERP.Catalog.Domain.Item;
using ForgeERP.SharedKernel;
using MediatR;

namespace ForgeERP.Catalog.Application.ItemUseCases.Commands
{

    public record CreateItemCommand(string ItemCode, string Name, ItemType ItemType, string UnitOfMeasure) : IRequest<Result<Guid>>;
}

using ForgeERP.Catalog.Domain.Item;
using ForgeERP.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForgeERP.Catalog.Domain.Routing
{
    public class Routing :AggregateRoot<RoutingId>
    {

        private readonly List<Operation> _operations = new();

        public ItemId ItemId { get; private set; }
        public string Name { get; private set; }


        private Routing() { }

        private Routing(RoutingId routingId ,ItemId itemId, string name) : base(routingId)
        {
            ItemId = itemId ?? throw new ArgumentNullException(nameof(itemId));
            Name = name;
        }

        public static Result<Routing> Create(ItemId itemId, string name)
        {
            if (itemId == null)
                return Result<Routing>.Failure("item not found.");

            if (string.IsNullOrWhiteSpace(name))
                return Result<Routing>.Failure("Name is required.");

            return Result<Routing>.Success(new Routing(RoutingId.New(), itemId, name));
        }

        public Result AddOperation(string name, int position, string workCenter, int estimatedDurationMinutes)
        {
            if (name == null)
                return Result.Failure("Name cannot be empty.");

            if (position <= 0)
                return Result.Failure("Position must be greated than 0.");

            if (workCenter == null)
                return Result.Failure("Work center is required.");

            if (estimatedDurationMinutes <= 0)
                return Result.Failure("Duration minutes must be greater than 0.");

            if (_operations.Any(x => x.Position == position))
                return Result.Failure("Positions cannot be same.");

            var newOperation = new Operation(Id, name,position, workCenter, estimatedDurationMinutes);

            _operations.Add(newOperation);
            return Result.Success();
        }


        public Result RemoveOperation(OperationId operationId)
        {
            var operation = _operations.FirstOrDefault(x => x.Id == operationId);

            if (operation == null)
                return Result.Failure("Operation not found.");

            _operations.Remove(operation);

            return Result.Success();
        }

        public Result UpdateOperation(OperationId operationId, string name, string workCenter, int estimatedDurationMinutes)
        {
            var operation = _operations.FirstOrDefault(x => x.Id == operationId);
            if (operation == null)
                return Result.Failure("Operation not found.");

            return operation.UpdateOperation(name, workCenter, estimatedDurationMinutes);
        }
    }
}

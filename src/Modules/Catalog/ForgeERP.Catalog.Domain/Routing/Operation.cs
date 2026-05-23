using ForgeERP.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForgeERP.Catalog.Domain.Routing
{
    public class Operation: Entity<OperationId>
    {
        public RoutingId RoutingId { get; private set; }
        public string Name { get;private set; }
        public int Position { get; private set; }
        public string WorkCenter { get; private set; }
        public int EstimatedDurationMinutes { get; private set; }

        private Operation() { }

        internal Operation(RoutingId routingId, string name, int position, string workCenter, int estimatedDurationMinutes) : base(OperationId.New())
        {
            RoutingId = routingId;
            Name = name;
            Position = position;
            WorkCenter = workCenter;
            EstimatedDurationMinutes = estimatedDurationMinutes;
        }

        internal Result UpdateOperation(string name, string workCenter, int estimatedDurationMinutes)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Result.Failure("Name cannot be empty.");

            if (string.IsNullOrWhiteSpace(workCenter))
                return Result.Failure("Work center is required.");

            if (estimatedDurationMinutes <= 0)
                return Result.Failure("Duration must be greater than 0.");

            Name = name;
            WorkCenter = workCenter;
            EstimatedDurationMinutes = estimatedDurationMinutes;

            return Result.Success();
        }
    }
}

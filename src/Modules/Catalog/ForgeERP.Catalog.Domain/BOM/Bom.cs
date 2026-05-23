using ForgeERP.Catalog.Domain.Item;
using ForgeERP.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForgeERP.Catalog.Domain.BOM
{
    public class Bom :AggregateRoot<BomId>
    {

        private readonly List<BomLine> _bomLines = new();

        public ItemId ItemId { get; private set; }
        public string Name { get; private set; }

        public IReadOnlyCollection<BomLine> BomLines => _bomLines.AsReadOnly();

        private Bom() { }

        private Bom(BomId bomId, ItemId itemId, string name): base(bomId)
        {
            ItemId = itemId ?? throw new ArgumentNullException(nameof(itemId));
            Name = name;
        }


        public static Result<Bom> Create(ItemId itemId, string name)
        {
            if (itemId == null)
                return Result<Bom>.Failure("It should be indicated which product the product tree belongs to.");

            if (string.IsNullOrWhiteSpace(name))
                return Result<Bom>.Failure("BOM name cannot be empty.");

            return Result<Bom>.Success(new Bom(BomId.New(), itemId, name));
        }

        public Result AddLine(ItemId componentItemId, decimal quantity, int position)
        {
            if (componentItemId == null)
                return Result.Failure("Component Id is required.");

            if (quantity <= 0)
                return Result.Failure("BOM line has to be greater than  0");

            if (componentItemId == ItemId)
                return Result.Failure("An item cannot be used for its own raw materials");

            if (_bomLines.Any(x => x.ComponentItemId == componentItemId))
                return Result.Failure("This component is already added to this BOM");

            var newLine = new BomLine(Id, componentItemId, quantity, position);

            _bomLines.Add(newLine);
            return Result.Success();
        }

        public Result RemoveLine(ItemId componentItemId)
        {
            var line = _bomLines.FirstOrDefault(x => x.ComponentItemId == componentItemId);

            if (line == null)
                return Result.Failure("Component not found.");

            _bomLines.Remove(line);

            return Result.Success();
        }

        public Result UpdateLineQuantity(ItemId componentItemId, decimal newQuantity)
        {
            if (newQuantity <= 0)
                return Result.Failure("New quantity has to be greater than 0");

            var line = _bomLines.FirstOrDefault(x => x.ComponentItemId == componentItemId);
            if (line == null)
                return Result.Failure("Component not found.");

            line.UpdateQuantity(newQuantity);

            return Result.Success();

        }

        public Result UpdateLinePosition(ItemId componentItemId, int newPosition)
        {

            var oldPosition = _bomLines.Where(x => x.ComponentItemId == componentItemId).Select(x => x.Position).FirstOrDefault();

            if (newPosition == oldPosition)
                return Result.Failure("New position has to be different than old.");

            var line = _bomLines.FirstOrDefault(x => x.ComponentItemId == componentItemId);
            if (line == null)
                return Result.Failure("Component not found.");

            line.UpdatePosition(newPosition);

            return Result.Success();

        }

    }
}

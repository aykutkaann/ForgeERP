using ForgeERP.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForgeERP.Catalog.Domain.Item
{
    public class Item : AggregateRoot<ItemId>
    {
        public string ItemCode { get; private set; }
        public string Name { get; private set; }
        public ItemType ItemType { get; private set; }
        public string UnitOfMeasure { get; private set; }
        public bool IsActive { get; private set; }

        private Item() { }

        private Item(ItemId id, string itemCode, string name, ItemType itemType, string unitOfMeasure ) : base(id)
        {
            ItemCode = itemCode;
            Name = name;
            ItemType = itemType;
            UnitOfMeasure = unitOfMeasure;
            IsActive = true;
        }

        public static Result<Item> Create (string itemCode, string name, ItemType itemType, string unitOfMeasure)
        {
            if (string.IsNullOrWhiteSpace(itemCode))
                return Result<Item>.Failure("Item code cannot be empty");

            if (string.IsNullOrWhiteSpace(name))
                return Result<Item>.Failure("Item name cannot be empty");

            if (string.IsNullOrWhiteSpace(unitOfMeasure))
                return Result<Item>.Failure("Unit of measure cannot be empty");

            var item = new Item(ItemId.New(), itemCode.Trim(), name.Trim(), itemType, unitOfMeasure);

            return Result<Item>.Success(item);
        }

        public Result UpdateDetails(string name, string unitOfMeasure)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Item name cannot be empty.", nameof(name));

            if (string.IsNullOrWhiteSpace(unitOfMeasure))
                throw new ArgumentException("Unit of measure cannot be empty.", nameof(unitOfMeasure));

            Name = name.Trim();
            UnitOfMeasure = unitOfMeasure.Trim();

            return Result.Success();
        }

        public void Deactivate()
        {
            if (!IsActive) return;
            IsActive = false;
        }

        public void Activate()
        {
            if (IsActive) return;
            IsActive = true;
        }
    }
}

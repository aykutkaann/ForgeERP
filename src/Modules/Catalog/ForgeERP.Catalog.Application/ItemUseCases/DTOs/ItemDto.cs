using System;
using System.Collections.Generic;
using System.Text;

namespace ForgeERP.Catalog.Application.ItemUseCases.DTOs
{

    public class ItemDto
    {
        public Guid Id { get; init; }
        public string ItemCode { get; init; }
        public string Name { get; init; }
        public string ItemType { get; init; } 
        public string UnitOfMeasure { get; init; }
        public bool IsActive { get; init; }
    }

}

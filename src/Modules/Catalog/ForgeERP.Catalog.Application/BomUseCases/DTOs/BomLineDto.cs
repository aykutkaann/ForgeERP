using System;
using System.Collections.Generic;
using System.Text;

namespace ForgeERP.Catalog.Application.BomUseCases.DTOs
{
    public class BomLineDto
    {
        public Guid Id { get; init; }
        public Guid ComponentItemId { get; init; }
        public decimal Quantity { get; init; }
        public int Position { get; init; }
    }
}

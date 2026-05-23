using ForgeERP.Catalog.Domain.BOM;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForgeERP.Catalog.Application.BomUseCases.DTOs
{
    public class BomDto
    {
        public Guid Id { get; init; }
        public Guid ItemId { get; init; }
        public string Name { get; init; }
        public List<BomLineDto> Lines { get; init; }
    }
}

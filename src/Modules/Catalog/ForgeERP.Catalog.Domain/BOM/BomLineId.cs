using ForgeERP.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForgeERP.Catalog.Domain.BOM
{
    public class BomLineId :StronglyTypedId
    {
        public BomLineId(Guid value) : base(value) { }

        public static BomLineId New() => new(Guid.NewGuid());
    }
}

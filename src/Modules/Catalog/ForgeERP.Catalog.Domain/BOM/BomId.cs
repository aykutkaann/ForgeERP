using ForgeERP.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForgeERP.Catalog.Domain.BOM
{
    public class BomId :StronglyTypedId
    {
        public BomId(Guid value) : base(value) { }

        public static BomId New() => new(Guid.NewGuid());
    }
}

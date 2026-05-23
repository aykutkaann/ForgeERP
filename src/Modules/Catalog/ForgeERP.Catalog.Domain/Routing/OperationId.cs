using ForgeERP.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForgeERP.Catalog.Domain.Routing
{
    public class OperationId : StronglyTypedId
    {
        public OperationId(Guid value) : base(value) { }

        public static OperationId New() => new(Guid.NewGuid());

        private OperationId() { }
    }
}

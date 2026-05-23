using ForgeERP.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForgeERP.Catalog.Domain.Routing
{
    public class RoutingId : StronglyTypedId
    {
        public RoutingId(Guid value) : base(value) { }

        public static RoutingId New() => new(Guid.NewGuid());

        private RoutingId() { }


    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace ForgeERP.SharedKernel
{
    public abstract class AggregateRoot<TId> : Entity<TId>
    {


        protected AggregateRoot() { }
        protected AggregateRoot(TId id)
        {
            Id = id;
        }
    }
}

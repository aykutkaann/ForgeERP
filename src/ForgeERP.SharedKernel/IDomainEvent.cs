using System;
using System.Collections.Generic;
using System.Text;

namespace ForgeERP.SharedKernel
{
    public interface IDomainEvent
    {
        public DateTime OccurredOn { get; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace ForgeERP.SharedKernel
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}

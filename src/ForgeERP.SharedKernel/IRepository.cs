using System;
using System.Collections.Generic;
using System.Text;

namespace ForgeERP.SharedKernel
{
    public interface IRepository<T, TId> where T : AggregateRoot<TId> 
    {
        Task<T?> GetByIdAsync(TId id, CancellationToken cancellationToken);
        Task AddAsync(T entity, CancellationToken cancellationToken);

        void Update(T entity);
        void Delete(T entity);

    }
}

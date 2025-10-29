using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API_PJ01_Domain.Entities;

namespace API_PJ01_Domain.Contracts
{
    public interface IUnitOfWork
    {
        // Generate Repository
        IGenericRepository<TKey, TEntity> GetRepository<TKey, TEntity>() where TEntity : BaseEntity<TKey>;
        // Save Changes
        Task<int> SaveChangesAsync();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using API_PJ01_Domain.Entities;

namespace API_PJ01_Domain.Contracts
{
    public interface ISpecifications<TKey, TEntity> where TEntity : BaseEntity<TKey>
    {
        List<Expression<Func<TEntity, object>>> Includes { get; set; }
        Expression<Func<TEntity, bool>>? Criteria { get; set; }
        public Expression<Func<TEntity, object>>? OrderBy { get; set; }
        public Expression<Func<TEntity, object>>? OrderByDesc { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using API_PJ01_Domain.Contracts;
using API_PJ01_Domain.Entities;

namespace API_PJ01_Services.Specifications
{
    public class BaseSpecifications<TKey, TEntity> : ISpecifications<TKey, TEntity> where TEntity : BaseEntity<TKey>
    {
        public List<Expression<Func<TEntity, object>>> Includes { get; set; } = new List<Expression<Func<TEntity, object>>>();
        public Expression<Func<TEntity, bool>>? Criteria { get; set; }
        public Expression<Func<TEntity, object>>? OrderBy { get; set; }
        public Expression<Func<TEntity, object>>? OrderByDesc { get; set; }

        public BaseSpecifications(Expression<Func<TEntity, bool>> expression)
        {
            Criteria = expression;
        }

        public void AddOrderBy(Expression<Func<TEntity, object>>? expression)
        {
            OrderBy = expression;
        }

        public void AddOrderByDescending(Expression<Func<TEntity, object>>? expression)
        {
            OrderByDesc = expression;
        }
    }
}

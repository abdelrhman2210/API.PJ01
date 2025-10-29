using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API_PJ01_Domain.Contracts;
using API_PJ01_Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace API_PJ01_Persistence
{
    public static class SpecificationsEvaluator
    {
        // Generate Dynamic Query
        public static IQueryable<TEntity> GetQuery<TKey, TEntity>(IQueryable<TEntity> inputQuery, ISpecifications<TKey, TEntity> spec) where TEntity : BaseEntity<TKey>
        {
            var query = inputQuery; // __context.Products

            if (spec.Criteria is not null)
            {
                query = query.Where(spec.Criteria); // __context.Products.Where(P => P.id == 12)
            }

            // __context.Products.Where(P => P.id == 12).Include(P => P.Brand)
            // __context.Products.Where(P => P.id == 12).Include(P => P.Brand).Include(P => P.Type)
            query = spec.Includes.Aggregate(query, (query, includeExpression) => query.Include(includeExpression));

            return query;
        }
    }
}

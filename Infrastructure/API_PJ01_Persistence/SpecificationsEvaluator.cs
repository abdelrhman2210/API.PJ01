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

            //check if there is criteria to filter
            if (spec.Criteria is not null)
            {
                query = query.Where(spec.Criteria); 
            }



            //check if there is order by expression to sort the data 
            if (spec.OrderBy is not null)
            {
                query = query.OrderBy(spec.OrderBy);
            }
            else if (spec.OrderByDesc is not null)
            {
                query = query.OrderByDescending(spec.OrderByDesc);
            }


            if (spec.IsPagination)
            {
                query = query.Skip(spec.Skip).Take(spec.Take); 
            }



            // __context.Products.Where(P => P.id == 12).Include(P => P.Brand)
            // __context.Products.Where(P => P.id == 12).Include(P => P.Brand).Include(P => P.Type)
            query = spec.Includes.Aggregate(query, (query, includeExpression) => query.Include(includeExpression));

            return query;
        }
    }
}

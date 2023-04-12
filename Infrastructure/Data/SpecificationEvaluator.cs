using Core;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class SpecificationEvaluator<T> where T : BaseEntity
{
    public static IQueryable<T> GetQuery (IQueryable<T> query , ISpecification<T> specification)
    {
        var qu = query;
        if(specification.Criteria != null)
        {
            qu = qu.Where(specification.Criteria);
        }
        if (specification.OrderBy != null)
        {
            qu = qu.OrderBy(specification.OrderBy);
        }
        if (specification.OrderByDesc != null)
        {
            qu = qu.OrderByDescending(specification.OrderByDesc);
        }
        if(specification.IsPagingEnabled)
        {
            qu = qu.Skip(specification.Skip).Take(specification.Take);
        }
        qu = specification.Includes.Aggregate(qu , (current , include)=>current.Include(include));
        return qu;

    }
}

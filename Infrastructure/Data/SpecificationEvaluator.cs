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
        qu = specification.Includes.Aggregate(qu , (current , include)=>current.Include(include));
        return qu;

    }
}

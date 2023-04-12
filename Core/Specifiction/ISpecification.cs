using System.Linq.Expressions;

namespace Core;

public interface ISpecification<T>
{
    Expression<Func<T, bool>> Criteria { get; }
    Expression<Func<T, object>> OrderBy { get; }
    Expression<Func<T, object>> OrderByDesc { get; }
    List<Expression<Func<T, object>>> Includes { get; }
    int Take { get; }
    int Skip { get; }
    public bool IsPagingEnabled { get; }
}

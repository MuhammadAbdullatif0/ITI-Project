using Core;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class GenericRepo<T> : IGenericRepo<T> where T : BaseEntity
{
    private readonly StoreContext context;

    public GenericRepo(StoreContext context)
    {
        this.context = context;
    }
    public async Task<IReadOnlyList<T>> GetAsync()
    {
        return await context.Set<T>().ToListAsync();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await context.Set<T>().FindAsync(id);
    }

    public async Task<T> GetEntityWithSpecification(ISpecification<T> specification)
    {
        return await ApplySpecification(specification).FirstOrDefaultAsync();
    }

    public  async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> specification)
    {
        return await ApplySpecification(specification).ToListAsync();
    }

    private IQueryable<T> ApplySpecification(ISpecification<T> specification)
    {
        return SpecificationEvaluator<T> .GetQuery(context.Set<T>().AsQueryable(), specification);
    }
}

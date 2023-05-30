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

    public void Add(T entity)
    {
        context.Set<T>().Add(entity);
        context.SaveChanges();

    }

    public async Task<int> CountAsync(ISpecification<T> specification)
    {
        return await ApplySpecification(specification).CountAsync();
    }

    public void Delete(T entity)
    {
        context.Set<T>().Remove(entity);
        context.SaveChanges();

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

    public void Update(T entity)
    {
        context.Set<T>().Attach(entity);
        context.Entry(entity).State = EntityState.Modified;
        context.SaveChanges();
    }

    private IQueryable<T> ApplySpecification(ISpecification<T> specification)
    {
        return SpecificationEvaluator<T> .GetQuery(context.Set<T>().AsQueryable(), specification);
    }
}

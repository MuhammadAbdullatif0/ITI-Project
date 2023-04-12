namespace Core;

public interface IGenericRepo<T> where T : BaseEntity
{
    Task<T?> GetByIdAsync(int id);
    Task<IReadOnlyList<T>> GetAsync();
    Task<T> GetEntityWithSpecification(ISpecification<T> specification );
    Task<IReadOnlyList<T>> ListAsync(ISpecification<T> specification);
    Task<int> CountAsync(ISpecification<T> specification);
}

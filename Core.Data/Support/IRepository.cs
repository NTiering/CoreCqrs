namespace Core.Data.Support;

public interface IRepository<T> where T : class, IDataModel
{
    Task<T?> Add(T model);
    Task<bool> Delete(T model);
    Task<T[]> Find(Func<T, bool> selector);
    Task<T?> Get(Guid id);
    IDataSet<T> GetDataSet();
    Task<T?> Update(Guid id, Action<T> mutator);
}
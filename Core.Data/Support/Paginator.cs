namespace Core.Data.Support;

public class Paginator<T>
     where T : class, IDataModel
{
    public Paginator(IQueryable<T> data, int pageSize = 25, int pageCount = 0)
    {
        TotalPages = (data.Count() / pageSize) + 1;
        Page = pageCount + 1;
        Data = data.Skip(25 * pageCount).Take(pageSize);
    }

    public int TotalPages { get; }
    public int Page { get; }
    public IQueryable<T> Data { get; }
}
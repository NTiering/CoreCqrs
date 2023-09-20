namespace Core.Data.Support;

public static class Ext
{
    public static Paginator<T> AsPaginated<T>(this IQueryable<T> data, int pageSize = 25, int pageCount = 0)
        where T : class, IDataModel
    {
        var rtn = new Paginator<T>(data, pageSize, pageCount);
        return rtn;
    }
}
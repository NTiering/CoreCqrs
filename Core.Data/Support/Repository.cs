using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Core.Data.Support;

public abstract class Repository<T> : IRepository<T> 
    where T : class, IDataModel
{

    public async Task<T?> Get(Guid id)
    {
        using var ds = GetDataSet();
        var rtn = await ds.DataSet.FirstOrDefaultAsync(x => x.Id == id);
        return rtn;
    }
    public async Task<T?> Add(T model)
    {
        if (model == null) return null;

        using var ds = GetDataSet();
        await ds.DataSet.AddAsync(model);
        await ds.SaveChanges();
        return model;
    }

    public async Task<bool> Delete(T model)
    {
        if (model == null) return false;
        using var ds = GetDataSet();
        var entity = await ds.DataSet.FirstOrDefaultAsync(x => x.Id == model.Id);
        if (entity == null) return false;
        ds.DataSet.Remove(entity);
        await ds.SaveChanges();
        return true;
    }

    public async Task<T?> Update(Guid id, Action<T> mutator)
    {
        using var ds = GetDataSet();
        var entity = await ds.DataSet.FirstOrDefaultAsync(x => x.Id == id);
        if (entity == null) return null;
        mutator(entity);
        await ds.SaveChanges();
        return entity;
    }

    public async Task<T[]> Find(Func<T, bool> selector)
    {
        using var ds = GetDataSet();
        var entity = await ds.DataSet.Where(x => selector(x)).ToArrayAsync();
        return entity;
    }
    public abstract IDataSet<T> GetDataSet();
}
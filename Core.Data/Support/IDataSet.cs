using Microsoft.EntityFrameworkCore;

namespace Core.Data.Support
{
    public interface IDataSet<T> : IDisposable
        where T : class, IDataModel
    {
        public DbSet<T> DataSet { get; }
        public Task<int> SaveChanges();
    }
}
namespace Core.Data.Support;

public abstract class DataModel : IDataModel
{
    public Guid Id { get; set; }
    public DateTime CreatedOn { get; set; }
    public DataModel()
    {
        Id = Guid.NewGuid();
        CreatedOn = DateTime.Now;
    }
}
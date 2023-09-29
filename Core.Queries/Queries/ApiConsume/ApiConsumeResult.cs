using Core.Data.Datamodels;

namespace Core.Queries.Queries.ApiConsume;
public class ApiConsumeResult : BaseQueryResult
{
    public string Author { get; set; } = string.Empty;
    public string Quote { get; set; } = string.Empty;
}

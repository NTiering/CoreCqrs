namespace Core.Queries.Queries.Auth
{
    public class GetUserResult : BaseQueryResult
    {
        public Guid UserId { get; set; } 
        public string Username { get; set; } = string.Empty;
    }

}

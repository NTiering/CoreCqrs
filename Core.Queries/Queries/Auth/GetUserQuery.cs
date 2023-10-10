namespace Core.Queries.Queries.Auth
{
    public class GetUserQuery : BaseQuery<GetUserResult> 
    { 
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

}

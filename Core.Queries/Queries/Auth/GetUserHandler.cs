using Microsoft.Extensions.Logging;

namespace Core.Queries.Queries.Auth
{
    public class GetUserHandler : BaseQueryHandler<GetUserQuery, GetUserResult>
    {

        public GetUserHandler(ILogger<GetUserQuery> logger)
            : base(logger)
        {
           
        }
        

        protected override Task HandleQuery(GetUserQuery query, GetUserResult result, CancellationToken cancellationToken)
        {
            result.UserId = Guid.Parse("6ec313ff-e764-4132-aca9-6fcaca215ea4");
            result.Username = "Test Tester";
            return Task.CompletedTask;

        }
    }

}

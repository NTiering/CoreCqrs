using Core.Queries.Support;


namespace Core.Tests.Queries;


public class BaseQueryHandlerTest
{   

    [Test]
    public void HandlerCanBeConstructed()
    {
        new TestQuery.Handler(Logger)
          .Should()
          .NotBeNull();
    }

    [Test]
    public async Task HandlerCanHandleQueries()
    {
        // arrange 
        var hdlr = new TestQuery.Handler(Logger);
        var qry = Query;

        // act 
        var result = await hdlr.Handle(qry, CancellationToken.None);

        // assert 
        result.Should().NotBeNull();
        result.Name.Should().BeSameAs(qry.Input);
    }

    [Test]
    public async Task HandlerTimesQueries()
    {
        // arrange 
        var delayInMs = 100;
        var hdlr = new TestQuery.Handler(Logger)
        {
            DelayinMs = delayInMs
        };

        // act 
        var result = await hdlr.Handle(Query, CancellationToken.None);

        // assert 
        result.DurationInMs.Should().BeGreaterThanOrEqualTo(delayInMs);
    }




    //  ----------------------------------------------------
    // --------------    Helpers                -------------
    //  ----------------------------------------------------


    private static ILogger<TestQuery.Query> Logger => Substitute.For<ILogger<TestQuery.Query>>();

    private static TestQuery.Query Query => new();

    public class TestQuery
    {
        public class Query : BaseQuery<Result> 
        { 
            public string Input { get; } = Guid.NewGuid().ToString();
        }
        public class Result : BaseQueryResult
        {
            public string Name { get; set; } = string.Empty;
        }

        public class Handler : BaseQueryHandler<Query, Result>
        {
            public int DelayinMs { get; set; } 
            public Handler(ILogger<Query> logger)
                : base(logger)
            {

            }

            protected override async Task HandleQuery(Query query, Result result, CancellationToken cancellationToken)
            {
                result.Name = query.Input;
                await Task.Delay(TimeSpan.FromMilliseconds(DelayinMs), cancellationToken);
           
            }
        }
    }
}

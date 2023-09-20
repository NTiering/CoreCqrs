
# CoreCqrs

A starting point for dotnet projects that use minimal api and mediatr asa CQRS platform. Designed to allow expansion without a steep learning curve.

# Quick start

## Adding a new Query endpoint 

### 1) Add a new CQRS query (see [this](https://github.com/NTiering/CoreCqrs/blob/main/Core.Queries/Queries/GetWidget.cs))

### 2) Create a Query class (to transmit the query params) 
```
 public class Query : BaseQuery<Result>
 { 
     public string Param1 {get;set;}
     public string Param2 {get;set;}
 }
```
### 3) Create a Result class (to transmit the resultset)
```
public class Result : BaseQueryResult
{
    public DataSet? MyDataset { get; set; } // this will carry the results
}
```
### 4) Create a Handler class (with a HandleQuery method) to 'do the work'
```
public class Handler : BaseQueryHandler<Query, Result>
{
    public Handler(ILogger<Query> logger)
        :base(logger) 
    {
        
    }

    protected override Task HandleQuery(Query query, Result result, CancellationToken cancellationToken)
    {
        /* Do the query here, put the results in the results object 
        E.G. result.DataSet = GetDataSet(query.Param2,query.Param2)
            */
    }
}
```
### 5) Add a new endpoint (see [this](https://github.com/NTiering/CoreCqrs/blob/main/Core/Endpoints.cs)) 

```
app.MapGet("<my endpoint>", async (HttpContext httpContext, IMediator mediator) =>
            {
                var result = await mediator.Send(new <My Query>.Query());
                var rtn = result.Format();
                return rtn;
            })
             .WithName("<my endpoint name>")
            .WithOpenApi();
```

Everything should be automatically detected and run.


## Adding a new Command endpoint 

### 1) Add a new CQRS command (see https://github.com/NTiering/CoreCqrs/blob/main/Core.Commands/Commands/AddWidget.cs)

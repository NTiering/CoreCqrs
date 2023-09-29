
# CoreCqrs

A starting point for dotnet projects that use minimal api and mediatr as a CQRS platform. Designed to allow expansion without a steep learning curve.


Events are also raised after command completion 

## Core Features 
* Separation of concerns (API, Queries, Commands & event handlers)
* Shallow learning curve.
* Built in post-command event
* built in query / command duration logging    

## Installation
Use the solution [file](https://github.com/NTiering/CoreCqrs/blob/main/Core/Core.sln) 

You'll need [docker](https://www.docker.com/)

And dotnet [7.0](https://dotnet.microsoft.com/en-us/download/dotnet/7.0) or later

## How it works

### Running a query 

**Api endpoint → Query → QueryHandler → QueryResult →  Api endpoint**

### Running a command

**Api endpoint → Command → CommandValidator → CommandHandler → CommandResult →  Api endpoint**

simple eh ? 

### Making a new query
1. Add a new Endpoint
2. Add a new Query Result
3. Add a new Query
4. Add a new Query Handler

Everything should be automatically registered

### Add_a_new_endpoint

Add a new [query endpoint](https://github.com/NTiering/CoreCqrs/blob/main/Core/Endpoints.cs) to the AddQueries method 
```csharp
app.MapGet("/<my endpoint>", async (HttpContext httpContext, IMediator mediator) =>
{
       var result = await mediator.Send(new <my query>());
       var rtn = result.Format();
       return rtn;
       })
      .WithName("<my query name>")
     .WithOpenApi();
}
```
### Add_a_new_query_result

Add a new [query result](https://github.com/NTiering/CoreCqrs/blob/main/Core.Queries/Queries/Widgets/GetWidgetResult.cs) that inherits from **BaseQueryResult** in a folder below **/Core.Queries/Queries/** 

```csharp
﻿using Core.Data.Datamodels;

namespace Core.Queries.Queries.<my query>;

public class <my query>: BaseQueryResult
{
    // properties to carry result values
}
```

### Add_a_new_query

Add a new [query](https://github.com/NTiering/CoreCqrs/blob/main/Core.Queries/Queries/Widgets/GetWidgetQuery.cs) that inherits from **BaseQuery**  in a folder below **/Core.Queries/Queries/** 

```csharp
﻿namespace Core.Queries.Queries.<my query>;

public class <my query>: BaseQuery<my query result> { }
```

### Add_a_new_query_handler

Add a new [query handler](https://github.com/NTiering/CoreCqrs/blob/main/Core.Queries/Queries/Widgets/GetWidgetHandler.cs) that inherits from BaseQueryHandler in a folder below **/Core.Queries/Queries/** 

```csharp
﻿using Core.Data.Datamodels;
using Microsoft.Extensions.Logging;

﻿namespace Core.Queries.Queries.<my query>;

public class <my query handler>: BaseQueryHandler<<my query>, <my query result>>
{
    public GetWidgetHandler(ILogger<<my query>> logger)
        : base(logger)
    {

    }

    protected override Task HandleQuery(<my query>query, 
                                        <my query result> result, 
                                        CancellationToken cancellationToken)
    {
        // do your work here
    }
}
```

### Making a new command
1. Add a new Endpoint
2. Add a new Command Result
3. Add a new Command
4. Add a new Command Validator
5. Add a new Command Handler

Everything should be automatically registered


Add a new [command endpoint](https://github.com/NTiering/CoreCqrs/blob/main/Core/Endpoints.cs) to the AddCommands method 
```csharp
app.mapPost("/<my endpoint>", async (HttpContext httpContext, IMediator mediator) =>
{
       var result = await mediator.Send(new <my command>());
       var rtn = result.Format();
       return rtn;
       })
      .WithName("<my command name>")
     .WithOpenApi();
}
```
Add a new Command Result

Add a new [command result](https://github.com/NTiering/CoreCqrs/blob/main/Core.Commands/Commands/Widgets/AddWidgetResult.cs) that inherits from **BaseComandResult** in a folder below **/Core.Commands/Commands/** 

```csharp
﻿using Core.Data.Datamodels;

namespace Core.Commands.Commands.<my command>;

public class <my command>: BaseComandResult
{
    // properties to carry result values
}
```

Add a new Command

Add a new [command](https://github.com/NTiering/CoreCqrs/blob/main/Core.Commands/Commands/Widgets/AddWidgetCommand.cs) record that inherits from **BaseComandR** in a folder below **/Core.Commands/Commands/** 

```csharp
﻿namespace Core.Commands.Commands.Widgets;

/// <summary>
/// Command used to pass in values 
/// </summary>
/// <param name="Name"></param>
public record my command(string Name) : BaseCommand<My command result>;
```

Add a new Command Validator 

Add a new [validator](https://github.com/NTiering/CoreCqrs/blob/main/Core.Commands/Commands/Widgets/AddWidgetValidator.cs) class that inherits from **AbstractValidator** in a folder below **/Core.Commands/Commands/**
This uses FluentValidation to enforce rules 

```csharp
﻿namespace Core.Commands.Commands.Widgets;

/// <summary>
/// Ensures the correctness of the values passed in
/// </summary>
public class <my command>Validator : AbstractValidator<my command>
{
    public  <my command>Validator()
    {
        RuleFor(x => x.Name).NotEmpty().WithErrorCode("bad-1");
    }
}
```

Add a new Command Handler 

Add a new [handler ](https://github.com/NTiering/CoreCqrs/blob/main/Core.Commands/Commands/Widgets/AddWidgetHandler.css) class that inherits from **BaseCommandHandler** in a folder below **/Core.Commands/Commands/**
This uses FluentValidation to enforce rules 

```csharp
﻿using Microsoft.Extensions.Logging;

namespace Core.Commands.Commands.Widgets;

/// <summary>
/// Executes the operation on the values in the command
/// </summary>
public class <my command>Handler : BaseCommandHandler<<my command>Command, <my command>Result>
{
    public <my command>Handler(IValidator<AddWidgetCommand> validator, ILogger<AddWidgetHandler> logger, IMediator mediator)
        : base(validator, logger, mediator)
    { }

    protected override Task<<my command>Result> HandleCommand(<my command> request, <my command>Result result, CancellationToken cancellationToken)
    {        
        // do work
    }
}
```


## Contributing

Pull requests are welcome. For major changes, please open an issue first
to discuss what you would like to change.

Please make sure to update tests as appropriate.

## License

[MIT](https://choosealicense.com/licenses/mit/)

﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Commands;

public static class Startup
{
    public static void Main(IApplicationBuilder app)
    {

    }

    public static void Main(IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Startup).Assembly));
        services.AddValidatorsFromAssemblyContaining(typeof(Startup));
    }
}

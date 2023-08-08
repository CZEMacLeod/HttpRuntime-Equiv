Host.CreateDefaultBuilder()
    .ConfigureWebHost(builder =>
    {
        builder.ConfigureAppConfiguration((context, builder) =>
        {
            var appPath = context.Configuration["IIS:FullApplicationPath"];
            if (appPath is not null)
            {
                var last = appPath.Split('\\', StringSplitOptions.RemoveEmptyEntries).Last();
                builder.AddJsonFile($"appsettings.{last}.json", true, true);
            }
        })
        .UseStartup<Startup>()
        .UseIIS(iis =>
        {
            builder.UseSetting("IIS:FullApplicationPath", iis.FullApplicationPath);
        });
    })
    .Build()
    .Run();

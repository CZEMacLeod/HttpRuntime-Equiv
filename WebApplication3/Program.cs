using Microsoft.AspNetCore;

var builder = WebHost.CreateDefaultBuilder()
    .ConfigureAppConfiguration((context, builder) =>
    {
        var appPath = context.Configuration["IIS:FullApplicationPath"];
        if (appPath is not null)
        {
            var last = appPath.Split('\\', StringSplitOptions.RemoveEmptyEntries).Last();
            builder.AddJsonFile($"appsettings.{last}.json", true, true);
        }
    })
    .UseStartup<Startup>()
    .UseIIS((builder, iis) =>
    {
        builder.UseSetting("IIS:FullApplicationPath", iis.FullApplicationPath);
    })
    .Build();

builder.Run();
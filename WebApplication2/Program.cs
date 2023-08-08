internal class Program
{
    private static void Main(string[] args)
    {
        Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration((context, builder) =>
            {
                var appPath = context.Configuration["IIS:FullApplicationPath"];
                if (appPath is not null)
                {
                    var last = appPath.Split('\\', StringSplitOptions.RemoveEmptyEntries).Last();
                    builder.AddJsonFile($"appsettings.{last}.json", true, true);
                }
            })
            .ConfigureWebHost(builder =>
            {
                builder
                    .UseStartup<Startup>()
                    .UseIIS(iis =>
                    {
                        builder.UseSetting("IIS:FullApplicationPath", iis.FullApplicationPath);
                    });
            })
            .Build()
            .Run();
    }
}

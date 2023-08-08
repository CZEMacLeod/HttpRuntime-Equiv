using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration) => _configuration = configuration;

    public void Configure(IApplicationBuilder app)
    {
        app.Run(async context => {
            var hello = _configuration["Hello"];
            await context.Response.WriteAsync($"Hello {hello}");
        });
    }
}
using IISHacks;
using Microsoft.AspNetCore.Hosting;

namespace Microsoft.AspNetCore.Hosting;

public static class IISExtensions
{
    public static IWebHostBuilder UseIIS(this IWebHostBuilder hostBuilder, Action<IIISEnvironmentFeature> action)
    {
        if (hostBuilder == null)
        {
            throw new ArgumentNullException(nameof(hostBuilder));
        }

        // Check if in process
        if (OperatingSystem.IsWindows() && NativeMethods.IsAspNetCoreModuleLoaded())
        {
            var iisConfigData = NativeMethods.HttpGetApplicationProperties();
            action(iisConfigData);
        }

        return hostBuilder.UseIIS();
    }

    public static IWebHostBuilder UseIIS(this IWebHostBuilder hostBuilder, Action<IWebHostBuilder, IIISEnvironmentFeature> action)
    {
        if (hostBuilder == null)
        {
            throw new ArgumentNullException(nameof(hostBuilder));
        }

        // Check if in process
        if (OperatingSystem.IsWindows() && NativeMethods.IsAspNetCoreModuleLoaded())
        {
            var iisConfigData = NativeMethods.HttpGetApplicationProperties();
            action(hostBuilder, iisConfigData);
        }

        return hostBuilder.UseIIS();
    }
}
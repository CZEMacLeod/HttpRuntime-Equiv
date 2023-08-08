using System.Reflection;

namespace IISHacks;

internal class NativeMethods
{
    private static readonly Type nm = Type.GetType("Microsoft.AspNetCore.Server.IIS.NativeMethods, Microsoft.AspNetCore.Server.IIS")!;
    private static readonly MethodInfo loaded = nm.GetMethod("IsAspNetCoreModuleLoaded", BindingFlags.Public | BindingFlags.Static)!;
    private static readonly Func<bool> isAspNetCoreModuleLoaded = (Func<bool>)Delegate.CreateDelegate(typeof(Func<bool>), null, loaded, false)!;

    private static readonly MethodInfo properties = nm.GetMethod("HttpGetApplicationProperties", BindingFlags.NonPublic | BindingFlags.Static)!;
    private static readonly Type iiscd = Type.GetType("Microsoft.AspNetCore.Server.IIS.Core.IISConfigurationData, Microsoft.AspNetCore.Server.IIS")!;
    private static readonly Type funciiscs = typeof(Func<>).MakeGenericType(iiscd);
    private static readonly Delegate getProperties = Delegate.CreateDelegate(funciiscs, null, properties, false)!;

    private static readonly FieldInfo fap = iiscd.GetField("pwzFullApplicationPath", BindingFlags.Instance | BindingFlags.Public)!;

    public static bool IsAspNetCoreModuleLoaded() => isAspNetCoreModuleLoaded();
    public static IISConfigurationData HttpGetApplicationProperties() {
        object iisConfigData = getProperties.DynamicInvoke() ?? throw new NullReferenceException();
        return new()
        {
            FullApplicationPath = (string)fap.GetValue(iisConfigData)!
        };
    }
}
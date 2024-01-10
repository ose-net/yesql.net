using CPlugin.Net;
using PluginApp.Contracts;
using PluginApp.HelloPlugin;

[assembly: Plugin(typeof(PluginStartup))]

namespace PluginApp.HelloPlugin;

public class PluginStartup : IPluginStartup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddTransient<HelloService>();
    }
}

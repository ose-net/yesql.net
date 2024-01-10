using CPlugin.Net;
using PluginApp.Contracts;
using PluginApp.UserPlugin;

[assembly: Plugin(typeof(PluginStartup))]

namespace PluginApp.UserPlugin;

public class PluginStartup : IPluginStartup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddTransient<UserSqlService>();
    }
}

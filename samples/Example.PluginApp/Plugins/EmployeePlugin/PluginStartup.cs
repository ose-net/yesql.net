using CPlugin.Net;
using PluginApp.Contracts;
using PluginApp.EmployeePlugin;

[assembly: Plugin(typeof(PluginStartup))]

namespace PluginApp.EmployeePlugin;

public class PluginStartup : IPluginStartup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddTransient<EmployeeSqlService>();
    }
}

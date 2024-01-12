using Microsoft.Extensions.DependencyInjection;

namespace PluginApp.Contracts;

public interface IPluginStartup
{
    void ConfigureServices(IServiceCollection services);
}

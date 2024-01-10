using Microsoft.AspNetCore.Mvc;

namespace PluginApp.HelloPlugin;

[Route("api/[controller]")]
[ApiController]
public class HelloController : ControllerBase
{
    [HttpGet]
    public string GetCodeSql(HelloService service)
        => service.Greet();
}

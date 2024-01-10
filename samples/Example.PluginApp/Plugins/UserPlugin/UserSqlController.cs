using Microsoft.AspNetCore.Mvc;

namespace PluginApp.UserPlugin;

[Route("api/[controller]")]
[ApiController]
public class UserSqlController : ControllerBase
{
    [HttpGet]
    public string GetSqlCode(UserSqlService service)
        => service.GetSqlCode();
}

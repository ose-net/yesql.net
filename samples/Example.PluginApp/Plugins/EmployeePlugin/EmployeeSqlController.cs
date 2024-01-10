using Microsoft.AspNetCore.Mvc;

namespace PluginApp.EmployeePlugin;

[Route("api/[controller]")]
[ApiController]
public class EmployeeSqlController : ControllerBase
{
    [HttpGet]
    public string GetCodeSql(EmployeeSqlService service)
        => service.GetSqlCode();
}

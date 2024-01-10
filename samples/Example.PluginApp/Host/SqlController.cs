using Microsoft.AspNetCore.Mvc;
using YeSql.Net;

namespace PluginApp.Host;

[Route("api/[controller]")]
[ApiController]
public class SqlController : ControllerBase
{
    [HttpGet("OrderSql")]
    public string GetOrdersSql(IYeSqlCollection sqlCollection)
        => sqlCollection["GetOrders"];
}

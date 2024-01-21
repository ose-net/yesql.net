using Microsoft.AspNetCore.Mvc;
using YeSql.Net;

namespace Example.AspNetCore;

[ApiController]
[Route("api/[controller]")]
public class SqlController : ControllerBase
{
    private readonly ISqlCollection _sqlCollection;
    public SqlController(ISqlCollection sqlCollection) => _sqlCollection = sqlCollection;

    [HttpGet("GetUsersSql")]
    public string GetUsersSql() 
        => _sqlCollection["GetUsers"];

    [HttpGet("GetRolesSql")]
    public string GetRolesSql()
        => _sqlCollection["GetRoles"];

    [HttpGet("GetProductsSql")]
    public string GetProductsSql() 
        => _sqlCollection["GetProducts"];

    [HttpGet("GetCustomersSql")]
    public string GetCustomersSql() 
        => _sqlCollection["GetCustomers"];

    [HttpGet("GetOrdersSql")]
    public string GetOrdersSql() 
        => _sqlCollection["GetOrders"];

    [HttpGet("GetPermissionsSql")]
    public string GetPermissionsSql() 
        => _sqlCollection["GetPermissions"];

    [HttpGet("GetThirdPartiesSql")]
    public string GetThirdPartiesSql() 
        => _sqlCollection["GetThirdParties"];

    [HttpGet("GetReceiptsSql")]
    public string GetReceiptsSql()
        => _sqlCollection["GetReceipts"];

    [HttpGet("GetReceiptDetailsSql")]
    public string GetReceiptDetailsSql()
        => _sqlCollection["GetReceiptDetails"];
}

using YeSql.Net;

namespace PluginApp.EmployeePlugin;

public class EmployeeSqlService(IYeSqlCollection sqlCollection)
{
    public string GetSqlCode()
        => sqlCollection["GetEmployees"];
}

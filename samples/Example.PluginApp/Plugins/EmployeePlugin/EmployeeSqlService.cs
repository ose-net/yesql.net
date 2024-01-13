using YeSql.Net;

namespace PluginApp.EmployeePlugin;

public class EmployeeSqlService(ISqlCollection sqlCollection)
{
    public string GetSqlCode()
        => sqlCollection["GetEmployees"];
}

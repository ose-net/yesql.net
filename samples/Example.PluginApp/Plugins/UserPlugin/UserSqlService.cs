using YeSql.Net;

namespace PluginApp.UserPlugin;

public class UserSqlService(IYeSqlCollection sqlCollection)
{
    public string GetSqlCode()
        => sqlCollection["GetUsers"];
}

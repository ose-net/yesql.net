using YeSql.Net;

namespace PluginApp.UserPlugin;

public class UserSqlService(ISqlCollection sqlCollection)
{
    public string GetSqlCode()
        => sqlCollection["GetUsers"];
}

using YeSql.Net;

var sqlStatements = new YeSqlLoader().Load();
Console.Write(sqlStatements["GetUsers"]);
Console.Write(sqlStatements["GetRoles"]);
Console.Write(sqlStatements["GetProducts"]);
Console.Write(sqlStatements["GetCustomers"]);
Console.Write(sqlStatements["GetOrders"]);
Console.Write(sqlStatements["GetPermissions"]);
Console.Write(sqlStatements["GetThirdParties"]);

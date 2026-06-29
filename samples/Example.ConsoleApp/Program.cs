using YeSql.Net;

ISqlCollection sqlStatements = new YeSqlLoader()
    .Exclude("client.sql", "employee.sql")
    .LoadFromDirectories("yesql");

Console.Write(sqlStatements["GetUsers"]);
Console.Write(sqlStatements["GetRoles"]);
Console.Write(sqlStatements["GetProducts"]);
Console.Write(sqlStatements["GetCustomers"]);
Console.Write(sqlStatements["GetOrders"]);
Console.Write(sqlStatements["GetPermissions"]);
Console.Write(sqlStatements["GetThirdParties"]);
Console.Write(sqlStatements["GetReceipts"]);
Console.Write(sqlStatements["GetReceiptDetails"]);

Console.WriteLine(sqlStatements.TryGetStatement("GetClients", out var sql)
    ? sql
    : "'GetClients' is unavailable because its SQL file was excluded.");

Console.WriteLine(sqlStatements.TryGetStatement("GetEmployees", out sql)
    ? sql
    : "'GetEmployees' is unavailable because its SQL file was excluded.");

using YeSql.Net;

var sqlStatements = new YeSqlLoader().Load(new [] { "./sample.sql" });

Console.Write(sqlStatements["GetUsers"]);
Console.Write(sqlStatements["GetRoles"]);
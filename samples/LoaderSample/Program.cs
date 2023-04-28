using YeSql.Net;

var loader = new YeSqlLoader();
var sqlStatements = loader.LoadFromFiles("./sample");
loader.LoadFromDirectories("./sql");

Console.Write(sqlStatements["GetUsers"]);
Console.Write(sqlStatements["GetRoles"]);
Console.Write(sqlStatements["GetProducts"]);
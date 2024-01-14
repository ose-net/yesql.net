using YeSql.Net;

var source = 
"""
    -- name: GetUsers
    -- Gets user records.
    SELECT* FROM user;

    -- name: GetRoles
    -- Gets role records.
    SELECT* FROM role;
""";

ISqlCollection sqlStatements = new YeSqlParser().ParseAndThrow(source);
Console.Write(sqlStatements["GetUsers"]);
Console.Write(sqlStatements["GetRoles"]);
using YeSql.Net;

var source = @"
    -- name: GetUsers
    -- Gets user records.
    SELECT* FROM user;

    -- name: GetRoles
    -- Gets role records.
    SELECT* FROM role;
";

var sqlStatements = new YeSqlParser().Parse(source, out var validationResult);

if (validationResult.HasError())
{
    Console.Write(validationResult.ErrorMessages);
}
else 
{
    Console.Write(sqlStatements["GetUsers"]);
    Console.Write(sqlStatements["GetRoles"]);
}
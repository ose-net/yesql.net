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
    Console.WriteLine(validationResult.ErrorMessages);
}
else 
{
    Console.WriteLine(sqlStatements["GetUsers"]);
    Console.WriteLine(sqlStatements["GetRoles"]);
}
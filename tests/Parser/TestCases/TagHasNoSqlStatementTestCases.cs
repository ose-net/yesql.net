namespace YeSql.Net.Tests.Parser.TestCases;

public class TagHasNoSqlStatementTestCases : IEnumerable
{
    public IEnumerator GetEnumerator()
    {
        yield return new object[]
        {
            """
            -- name: GetProducts
            SELECT id, name, price FROM products;

            -- name: GetUsers
               
                             
            -- name: GetRoles
            """
        };

        yield return new object[]
        {
            """
            -- name: GetUsers
               
                             
            -- name: GetRoles

            -- name: GetProducts
            SELECT id, name, price FROM products;
            """
        };

        yield return new object[]
        {
            """
            -- name: GetUsers
             
            -- name: GetProducts
            SELECT id, name, price FROM products;
                             
            -- name: GetRoles
            """
        };
    }
}

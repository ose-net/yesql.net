namespace YeSql.Net.Tests.Parser.TestCases;

public class LineIsCommentWithoutTagTestCases : IEnumerable
{
    public IEnumerator GetEnumerator()
    {
        yield return new object[]
        {
            """
            -- This is an comment without tag.
            -- SELECT id, name FROM products;
            """,
            new Dictionary<string, string>()
        };

        yield return new object[]
        {
            """
            -- Queries:
                   
            -- Gets a set of products
            -- name: GetProducts
            -- This query returns only two columns:
            -- name, price
            SELECT name, price FROM products;
            """,
           new Dictionary<string, string>
           {
               { "GetProducts", $"SELECT name, price FROM products;{NewLine}" }
           }
        };

        yield return new object[]
        {
            """
            -- Queries:
                   
            -- Gets a set of products
            -- name: GetProducts
            -- This query returns only two columns:
            -- name, price
            SELECT name, price FROM products;

            -- Gets a set of users
            -- name: GetUsers
            -- This query returns only two columns:
            -- id, name
            SELECT id, name FROM users;
            """,
           new Dictionary<string, string>
           {
               { "GetProducts", $"SELECT name, price FROM products;{NewLine}" },
               { "GetUsers", $"SELECT id, name FROM users;{NewLine}" }
           }
        };

        yield return new object[]
        {
            """
            -- Queries:
                   
            -- Gets a set of products
            -- name: GetProducts
            -- This query returns only two columns:
            -- name, price
            SELECT name, price FROM products;

            -- Gets a set of users
            -- name: GetUsers
            -- This query returns only two columns:
            -- id, name
            SELECT id, name FROM users;

            -- Gets a set of roles
            -- name: GetRoles
            -- This query returns only two columns:
            -- id, name
            SELECT id, name FROM roles;
            """,
           new Dictionary<string, string>
           {
               { "GetProducts", $"SELECT name, price FROM products;{NewLine}" },
               { "GetUsers", $"SELECT id, name FROM users;{NewLine}" },
               { "GetRoles", $"SELECT id, name FROM roles;{NewLine}" }
           }
        };

        yield return new object[]
        {
           $"""
            -- Queries:
                   
               -- Gets a set of products
            -- name: GetProducts
               -- This query returns only two columns:
               -- name, price
            SELECT name, price FROM products;

                  -- Gets a set of users
            -- name: GetUsers
            {"\t"}-- This query returns only two columns:
                  -- id, name
            SELECT id, name FROM users;

            {"\t"}{"\t"} -- Gets a set of roles
            -- name: GetRoles
            {"\t"}{"\t"} -- This query returns only two columns:
            {"\t"}{"\t"} -- id, name
            SELECT id, name FROM roles;
            """,
           new Dictionary<string, string>
           {
               { "GetProducts", $"SELECT name, price FROM products;{NewLine}" },
               { "GetUsers", $"SELECT id, name FROM users;{NewLine}" },
               { "GetRoles", $"SELECT id, name FROM roles;{NewLine}" }
           }
        };
    }
}

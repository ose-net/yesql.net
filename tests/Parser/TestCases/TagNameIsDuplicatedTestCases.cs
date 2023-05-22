namespace YeSql.Net.Tests.Parser.TestCases;

public class TagNameIsDuplicatedTestCases : IEnumerable
{
    public IEnumerator GetEnumerator()
    {
        yield return new object[]
        {
            """
            -- name: GetProducts
            SELECT
            name,
            price
            FROM products;
            
            -- name: GetUsers
            SELECT id, name FROM users;
            
            -- name: GetUsers           
            SELECT email, name FROM users;
              
            -- name: GetUsers           
            SELECT email FROM users;
                 
            """,
           new List<string>
           {
               $"Parsing error (line 10, col 9): error: {string.Format(ExceptionMessages.DuplicateTagNameMessage, "GetUsers")}",
               $"Parsing error (line 13, col 9): error: {string.Format(ExceptionMessages.DuplicateTagNameMessage, "GetUsers")}"
           }
        };

        yield return new object[]
        {
            """
            -- name: GetUsers
            SELECT id, name FROM users;
            
            -- name: GetUsers           
            SELECT email, name FROM users;
              
            -- name: GetUsers           
            SELECT email FROM users;

            -- name: GetProducts
            SELECT
            name,
            price
            FROM products;
                 
            """,
           new List<string>
           {
               $"Parsing error (line 4, col 9): error: {string.Format(ExceptionMessages.DuplicateTagNameMessage, "GetUsers")}",
               $"Parsing error (line 7, col 9): error: {string.Format(ExceptionMessages.DuplicateTagNameMessage, "GetUsers")}"
           }
        };

        yield return new object[]
        {
            """
            -- name: GetUsers
            SELECT id, name FROM users;
            
            -- name: GetUsers           
            SELECT email, name FROM users;
              
            -- name: GetProducts
            SELECT
            name,
            price
            FROM products;
              
            -- name: GetUsers           
            SELECT email FROM users;
                   
            """,
           new List<string>
           {
               $"Parsing error (line 4, col 9): error: {string.Format(ExceptionMessages.DuplicateTagNameMessage, "GetUsers")}",
               $"Parsing error (line 13, col 9): error: {string.Format(ExceptionMessages.DuplicateTagNameMessage, "GetUsers")}"
           }
        };

        yield return new object[]
        {
            """
            -- name: GetUsers
            SELECT id, name FROM users;
            
            -- name: GetProducts
            SELECT
            name,
            price
            FROM products;

            -- name: GetUsers           
            SELECT email, name FROM users;
              
            -- name: GetUsers           
            SELECT email FROM users;
                    
            """,
           new List<string>
           {
               $"Parsing error (line 10, col 9): error: {string.Format(ExceptionMessages.DuplicateTagNameMessage, "GetUsers")}",
               $"Parsing error (line 13, col 9): error: {string.Format(ExceptionMessages.DuplicateTagNameMessage, "GetUsers")}"
           }
        };
    }
}

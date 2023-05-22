namespace YeSql.Net.Tests.Parser.TestCases;

public class LineIsNotAssociatedWithAnyTagTestCases : IEnumerable
{
    public IEnumerator GetEnumerator()
    {
        yield return new object[]
        {
            """
            This should generate an error.
            SELECT price FROM products;
              
            -- name: GetProducts
            SELECT
            name,
            price
            FROM products;
            
            -- name:
            SELECT name FROM products;
            """,
           new List<string>
           {
               $"Parsing error (line 1, col 1): error: {string.Format(ExceptionMessages.LineIsNotAssociatedWithAnyTagMessage, "This should generate an error.")}",
               $"Parsing error (line 2, col 1): error: {string.Format(ExceptionMessages.LineIsNotAssociatedWithAnyTagMessage, "SELECT price FROM products;")}",
               $"Parsing error (line 10, col 9): error: {ExceptionMessages.TagIsEmptyOrWhitespaceMessage}",
               $"Parsing error (line 11, col 1): error: {string.Format(ExceptionMessages.LineIsNotAssociatedWithAnyTagMessage, "SELECT name FROM products;")}"
           }
        };

        yield return new object[]
        {
            """
            This should generate an error.
            SELECT price FROM products;
              
            -- name:
            SELECT name FROM products;

            -- name: GetProducts
            SELECT
            name,
            price
            FROM products;
            """,
           new List<string>
           {
               $"Parsing error (line 1, col 1): error: {string.Format(ExceptionMessages.LineIsNotAssociatedWithAnyTagMessage, "This should generate an error.")}",
               $"Parsing error (line 2, col 1): error: {string.Format(ExceptionMessages.LineIsNotAssociatedWithAnyTagMessage, "SELECT price FROM products;")}",
               $"Parsing error (line 4, col 9): error: {ExceptionMessages.TagIsEmptyOrWhitespaceMessage}",
               $"Parsing error (line 5, col 1): error: {string.Format(ExceptionMessages.LineIsNotAssociatedWithAnyTagMessage, "SELECT name FROM products;")}"
           }
        };
    }
}

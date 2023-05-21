namespace YeSql.Net.Tests.Parser.TestCases;

public class TagIsEmptyOrWhitespaceTestCases : IEnumerable
{
    public IEnumerator GetEnumerator()
    {
        yield return new object[]
        {
           $"""
            -- name: GetProducts
            SELECT
            name,
            price
            FROM products;
            
            -- name:
            SELECT name FROM products;
            
            -- name:            
            SELECT price FROM products;
            
            -- name:
                          
            
            -- name:{"\t"}{"\t"}
            """,
            new List<string>
            {
                $"Parsing error (line 7, col 9): error: {ExceptionMessages.TagIsEmptyOrWhitespaceMessage}",
                $"Parsing error (line 8, col 1): error: {string.Format(ExceptionMessages.LineIsNotAssociatedWithAnyTagMessage, "SELECT name FROM products;")}",
                $"Parsing error (line 10, col 9): error: {ExceptionMessages.TagIsEmptyOrWhitespaceMessage}",
                $"Parsing error (line 11, col 1): error: {string.Format(ExceptionMessages.LineIsNotAssociatedWithAnyTagMessage, "SELECT price FROM products;")}",
                $"Parsing error (line 13, col 9): error: {ExceptionMessages.TagIsEmptyOrWhitespaceMessage}",
                $"Parsing error (line 16, col 9): error: {ExceptionMessages.TagIsEmptyOrWhitespaceMessage}"
            }
        };

        yield return new object[]
        {
           $"""
            -- name:
            SELECT name FROM products;
            
            -- name:            
            SELECT price FROM products;
            
            -- name:
                          
            
            -- name:{"\t"}{"\t"}


            -- name: GetProducts
            SELECT
            name,
            price
            FROM products;
            """,
            new List<string>
            {
                $"Parsing error (line 1, col 9): error: {ExceptionMessages.TagIsEmptyOrWhitespaceMessage}",
                $"Parsing error (line 2, col 1): error: {string.Format(ExceptionMessages.LineIsNotAssociatedWithAnyTagMessage, "SELECT name FROM products;")}",
                $"Parsing error (line 4, col 9): error: {ExceptionMessages.TagIsEmptyOrWhitespaceMessage}",
                $"Parsing error (line 5, col 1): error: {string.Format(ExceptionMessages.LineIsNotAssociatedWithAnyTagMessage, "SELECT price FROM products;")}",
                $"Parsing error (line 7, col 9): error: {ExceptionMessages.TagIsEmptyOrWhitespaceMessage}",
                $"Parsing error (line 10, col 9): error: {ExceptionMessages.TagIsEmptyOrWhitespaceMessage}"
            }
        };

        yield return new object[]
        {
           $"""
            -- name:
            SELECT name FROM products;
            
            -- name:            
            SELECT price FROM products;
            
            -- name: GetProducts
            SELECT
            name,
            price
            FROM products;

            -- name:
                          
            
            -- name:{"\t"}{"\t"}

              
            """,
            new List<string>
            {
                $"Parsing error (line 1, col 9): error: {ExceptionMessages.TagIsEmptyOrWhitespaceMessage}",
                $"Parsing error (line 2, col 1): error: {string.Format(ExceptionMessages.LineIsNotAssociatedWithAnyTagMessage, "SELECT name FROM products;")}",
                $"Parsing error (line 4, col 9): error: {ExceptionMessages.TagIsEmptyOrWhitespaceMessage}",
                $"Parsing error (line 5, col 1): error: {string.Format(ExceptionMessages.LineIsNotAssociatedWithAnyTagMessage, "SELECT price FROM products;")}",
                $"Parsing error (line 13, col 9): error: {ExceptionMessages.TagIsEmptyOrWhitespaceMessage}",
                $"Parsing error (line 16, col 9): error: {ExceptionMessages.TagIsEmptyOrWhitespaceMessage}"
            }
        };
    }
}

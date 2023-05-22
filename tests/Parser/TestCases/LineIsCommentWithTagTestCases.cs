namespace YeSql.Net.Tests.Parser.TestCases;

public class LineIsCommentWithTagTestCases : IEnumerable
{
    public IEnumerator GetEnumerator()
    {
        yield return new object[]
        {
            """
            -- name: GetProducts
            SELECT 
            id, 
            name, 
            price 
            FROM products;
               
            -- name: GetUsers
            SELECT 
            id,  
            name, 
            email 
            FROM users;
              
            -- name: GetRoles
            SELECT
            id, 
            name 
            FROM roles;
            """
        };

        yield return new object[]
        {
            """
            --name: GetProducts
            SELECT 
            id, 
            name, 
            price 
            FROM products;
                
            -- name:    GetUsers      
            SELECT 
            id,  
            name, 
            email 
            FROM users;
               
               --   name:  GetRoles    
            SELECT
            id, 
            name 
            FROM roles;
            """
        };
    }
}

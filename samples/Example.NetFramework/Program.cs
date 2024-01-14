using System;
using YeSql.Net;

namespace Example.NetFramework
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ISqlCollection sqlStatements = new YeSqlLoader().LoadFromDefaultDirectory();
            Console.Write(sqlStatements["GetUsers"]);
            Console.Write(sqlStatements["GetRoles"]);
            Console.Write(sqlStatements["GetProducts"]);
            Console.Write(sqlStatements["GetCustomers"]);
            Console.Write(sqlStatements["GetOrders"]);
            Console.Write(sqlStatements["GetPermissions"]);
            Console.Write(sqlStatements["GetThirdParties"]);
            Console.ReadLine();
        }
    }
}

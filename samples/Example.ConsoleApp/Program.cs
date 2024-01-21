﻿using YeSql.Net;

ISqlCollection sqlStatements = new YeSqlLoader().LoadFromDefaultDirectory();
Console.Write(sqlStatements["GetUsers"]);
Console.Write(sqlStatements["GetRoles"]);
Console.Write(sqlStatements["GetProducts"]);
Console.Write(sqlStatements["GetCustomers"]);
Console.Write(sqlStatements["GetOrders"]);
Console.Write(sqlStatements["GetPermissions"]);
Console.Write(sqlStatements["GetThirdParties"]);
Console.Write(sqlStatements["GetReceipts"]);
Console.Write(sqlStatements["GetReceiptDetails"]);

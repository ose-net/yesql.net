# YeSQL.NET

[![YeSql.Net](https://img.shields.io/nuget/vpre/YeSql.Net?label=YeSql.Net%20-%20nuget&color=red)](https://www.nuget.org/packages/YeSql.Net)
[![downloads](https://img.shields.io/nuget/dt/YeSql.Net?color=yellow)](https://www.nuget.org/packages/YeSql.Net)

[![CopySqlFilesToOutputDirectory](https://img.shields.io/nuget/vpre/CopySqlFilesToOutputDirectory?label=CopySqlFilesToOutputDirectory%20-%20nuget&color=red)](https://www.nuget.org/packages/CopySqlFilesToOutputDirectory)
[![downloads](https://img.shields.io/nuget/dt/CopySqlFilesToOutputDirectory?color=yellow)](https://www.nuget.org/packages/CopySqlFilesToOutputDirectory)

[![yesql.net](https://raw.githubusercontent.com/ose-net/yesql.net/master/yesql-logo.png)](https://github.com/ose-net/yesql.net)

YeSQL.NET is a class library for loading SQL statements from .sql files instead of writing SQL code in your C# source files.

- This library was inspired by [krisajenkins/yesql](https://github.com/krisajenkins/yesql). 
- [YepSQL](https://github.com/LionsHead/YepSQL) library has been used as a reference for the creation of the parser.

See the [API documentation](https://ose-net.github.io/yesql.net/api/YeSql.Net.html) for more information on this project.

## Index

- [Advantages](#advantages)
- [Installation](#installation)
- [Overview](#overview)
  - [Creating .sql file](#creating-sql-file)
  - [Load from a default directory](#load-from-a-default-directory)
  - [Accessing SQL statements](#accessing-sql-statements)
  - [Load from a set of directories](#load-from-a-set-of-directories)
  - [Load from a set of files](#load-from-a-set-of-files)
  - [Parsing .sql files](#parsing-sql-files)
  - [Copy .sql files to the publish directory](#copy-sql-files-to-the-publish-directory)
- [Samples](#samples)
- [Contribution](#contribution)

## Advantages

By keeping the SQL and C# separate you get:

- Better editor support. Your editor probably already has great SQL support. By keeping the SQL as SQL, you get to use it.
- Query reuse. Drop the same SQL files into other projects, because they're simply SQL. Share them as a module.
  - See this [sample](https://github.com/ose-net/yesql.net/tree/master/samples/Example.QueryReuse) on how to distribute SQL files in a nuget package.
- Team interoperability. Your DBAs can read and write the SQL you use in your .NET project.
- Separation of concerns. Since your SQL statements are not embedded (hard-coded) directly in the application code, you can make minor changes to the SQL file without having to open the C# source file.
  - Any changes you make to the SQL file should not affect the C# source file unless it is a major change (e.g., renaming a column).
- Independent development. A developer can create the SQL statements without waiting for another developer to create the C# source file with the proposed feature.

## Installation

If you're want to install the package from Visual Studio, you must open the project/solution in Visual Studio, and open the console using the **Tools** > **NuGet Package Manager** > **Package Manager Console** command and run the install command:
```sh
Install-Package YeSql.Net
```
If you are making use of the dotnet CLI, then run the following in your terminal:
```sh
dotnet add package YeSql.Net
```

## Overview

You must import the namespace types at the beginning of your class file:
```cs
using YeSql.Net;
```

This library provides three main types:
- `YeSqlLoader`
- `YeSqlParser`
- `ISqlCollection`

See the [API documentation](https://ose-net.github.io/yesql.net/api/YeSql.Net.html) for more information on these types.

### Creating .sql file

Create a file with **.sql** extension containing the SQL statements.

**Example:**
```sql
-- name: GetUsers
-- Gets user information.
SELECT 
id, 
username, 
email 
FROM users;

-- name: GetRoles
-- Gets role information.
SELECT 
id,
name
FROM roles;
```
Each SQL statement must be associated with a tag using the `name:` prefix and must begin with a comment. This is necessary so that the parser can uniquely identify each SQL statement by its tag.

For example, in this case the tag is `GetRoles` and the SQL statement would be `SELECT id, name FROM roles;`.

You should also note that comments that do not use the `name:` prefix will be ignored by the parser.

### Load from a default directory

You can load SQL statements from a default directory called `yesql`.
```cs
ISqlCollection sqlStatements = new YeSqlLoader().LoadFromDefaultDirectory();
```
This method starts searching from the current directory where the application is running (e.g. bin/Debug/net8.0).

It is recommended to install the nuget package called [CopySqlFilesToOutputDirectory](https://www.nuget.org/packages/CopySqlFilesToOutputDirectory) to copy the .sql files from the project folder to the output directory (e.g. bin/Debug/net8.0). 
This will create a folder called `yesql` in the output directory where all the .sql files will be. 
From there, the `LoadFromDefaultDirectory` method will start loading the SQL files.

You can install the package from the terminal:
```sh
dotnet add package CopySqlFilesToOutputDirectory
```

### Accessing SQL statements

You can access SQL statements using the `ISqlCollection` interface.
```cs
string tagName = "GetUsers";
string sqlCode = sqlStatements[tagName];
```
But you must specify the tag name to access the SQL statement. Remember that each SQL code is identified with its respective tag.

### Load from a set of directories

You can load SQL statements from all the SQL files in the specified directories.
```cs
var directories = new[]
{
  "./sql/reports",
  "/home/admin/MyApp2/reports"
};
ISqlCollection sqlStatements = new YeSqlLoader().LoadFromDirectories(directories);
```
You can attach the absolute or relative path to the directory name. If the path is relative, then the method will start searching from the current directory where the application is running (e.g. bin/Debug/net8.0).

### Load from a set of files

You can load SQL statements from the specified files.
```cs
var sqlFiles = new[]
{
  "./reports/users.sql",
  "/home/admin/MyApp2/products.sql"
};
ISqlCollection sqlStatements = new YeSqlLoader().LoadFromFiles(sqlFiles);
```
You can attach the absolute or relative path to the file name. If the path is relative, then the method will start searching from the current directory where the application is running (e.g. bin/Debug/net8.0).

### Parsing .sql files

You can use the parser to extract SQL statements from any data source.
```cs
var source =
"""
-- name: GetUsers
-- Gets user information.
SELECT 
id, 
username, 
email 
FROM users;
""";
YeSqlValidationResult validationResult;
ISqlCollection sqlStatements = new YeSqlParser().Parse(source, out validationResult);
if(validationResult.HasError())
{
  // Some code to handle the error.
}
```
If you do not want to handle the error, you can use the `ParseAndThrow` method.
```cs
ISqlCollection sqlStatements = new YeSqlParser().ParseAndThrow(source);
```
This method throws an exception of type `YeSqlParserException` when the parser encounters an error.

### Copy .sql files to the publish directory

[CopySqlFilesToOutputDirectory](https://www.nuget.org/packages/CopySqlFilesToOutputDirectory) package is also used to copy the .sql files from the project folder to the publish directory when using the **dotnet publish** command. 
This will create a folder called `yesql` in the publish directory where all the .sql files will be.

It is recommended to publish the application in a directory other than the project folder (where the project file is located).

**Example:**
```sh
dotnet publish -o /home/admin/out/PublishedApp -c Release
```
For more information, see this issue: [Recommendations for publishing the application](https://github.com/ose-net/yesql.net/issues/106).

## Samples

You can find a complete and functional example in these projects:

- [Example.ConsoleApp](https://github.com/ose-net/yesql.net/tree/master/samples/Example.ConsoleApp)
- [Example.AspNetCore](https://github.com/ose-net/yesql.net/tree/master/samples/Example.AspNetCore)
- [Example.Parsing](https://github.com/ose-net/yesql.net/tree/master/samples/Example.Parsing)
- [Example.QueryReuse](https://github.com/ose-net/yesql.net/tree/master/samples/Example.QueryReuse)
- [Example.PluginApp](https://github.com/ose-net/yesql.net/tree/master/samples/Example.PluginApp)
- [Example.NetFramework](https://github.com/ose-net/yesql.net/tree/master/samples/Example.NetFramework)
- [Example.AspNetFramework](https://github.com/ose-net/yesql.net/tree/master/samples/Example.AspNetFramework)

## Contribution

Any contribution is welcome! Remember that you can contribute not only in the code, but also in the documentation or even improve the tests.

Follow the steps below:

- Fork it
- Create your feature branch (git checkout -b my-new-change)
- Commit your changes (git commit -am 'Add some change')
- Push to the branch (git push origin my-new-change)
- Create new Pull Request

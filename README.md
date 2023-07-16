# YeSQL.NET

[![yesql.net](https://img.shields.io/badge/.NET%20Standard-2.0-red)](https://github.com/ose-net/yesql.net)
[![yesql.net](https://img.shields.io/badge/License-MIT-green)](https://raw.githubusercontent.com/ose-net/yesql.net/master/LICENSE)
[![yesql.net](https://img.shields.io/badge/Project-Class%20Library-yellow)](https://github.com/ose-net/yesql.net)
[![Nuget-Badges](https://buildstats.info/nuget/yesql.net?includePreReleases=true)](https://www.nuget.org/packages/yesql.net/)

[![yesql.net](https://raw.githubusercontent.com/ose-net/yesql.net/master/yesql-logo.png)](https://github.com/ose-net/yesql.net)

YeSQL.NET is a class library for loading SQL statements from .sql files instead of writing SQL code in your C# source files.

- This library was inspired by [krisajenkins/yesql](https://github.com/krisajenkins/yesql). 
- [YepSQL](https://github.com/LionsHead/YepSQL) library has been used as a reference for the creation of the parser.

## Important

This project is still in the development phase, so it is not yet ready for production use.
> Please note that the current API may change in future versions.

## Advantages

By keeping the SQL and C# separate you get:

- Better editor support. Your editor probably already has great SQL support. By keeping the SQL as SQL, you get to use it.
- Query reuse. Drop the same SQL files into other projects, because they're just plain ol' SQL. Share them as a submodule.
- Team interoperability. Your DBAs can read and write the SQL you use in your .NET project.
- Separation of concerns. Since your SQL statements are not embedded (hard-coded) directly in the application code, you can make minor changes to the SQL file without having to open the C# source file.
  - Any changes you make to the SQL file should not affect the C# source file unless it is a major change (e.g., renaming a column).
- Independent development. A developer can create the SQL statements without waiting for another developer to create the C# source file with the proposed feature.

## Installation

If you're want to install the package from Visual Studio, you must open the project/solution in Visual Studio, and open the console using the **Tools** > **NuGet Package Manager** > **Package Manager Console** command and run the install command:
```
Install-Package YeSql.Net -IncludePrerelease
```
If you are making use of the dotnet CLI, then run the following in your terminal:
```
dotnet add package YeSql.Net --prerelease
```

## Overview

Create a file with .sql extension containing the SQL statements:
```sql
-- name: GetUsers
-- Gets user information.
SELECT 
id, 
username, 
email 
FROM users;

-- name: GetRoles
SELECT * FROM roles;
```
Each SQL statement must be associated with a tag using the `name:` prefix and must begin with a comment.

This is necessary so that the parser can uniquely identify each SQL statement by its tag.

You must then import the namespace types at the beginning of your class file:
```cs
using YeSql.Net;
```

### Load from files

You can load SQL statements from the specified files:
```cs
var sqlStatements = new YeSqlLoader().LoadFromFiles("users.sql", "sample.sql");
```

### Load from directories

You can load SQL statements from all the SQL files in the specified directories:
```cs
var sqlStatements = new YeSqlLoader().LoadFromDirectories("sql", "samples");
```

### Parsing .sql files

You can use the parser to extract SQL statements from any data source:
```cs
var source =
"""
-- name: GetUsers
SELECT id, username, email FROM users;
""";
YeSqlValidationResult validationResult;
var sqlStatements = new YeSqlParser().Parse(source, out validationResult);
if(validationResult.HasError())
{
    // Some code to handle the error.
}
```
If you want to handle the error in another way, you can use the `ParseAndThrow` method:
```cs
var sqlStatements = new YeSqlParser().ParseAndThrow(source);
```
This method throws an exception of type `YeSqlParserException` when the parser encounters an error.

### Accessing SQL statements

You can access SQL statements using the `IYeSqlCollection` interface:
```cs
// Tag: GetUsers
string sql = sqlStatements["GetUsers"];
```
But you must specify the tag name to access the SQL statement. Remember that each SQL code is identified with its respective tag.

## Copying .sql files to the output directory

It is recommended to copy the .sql files to the output directory because currently the loader search for the .sql files in the current directory where the application is running, so to avoid possible errors, it is better to copy the following instruction to your project file (.csproj):
```xml
<ItemGroup>
  <Content 
    Include="**\*.sql"
    Exclude="bin\**"
    CopyToOutputDirectory="PreserveNewest" 
    TargetPath="sql\%(Filename)%(Extension)" 
  />
</ItemGroup>
```
This will copy the .sql files into an `sql` folder in the output directory (e.g., `/bin/Debug/net7.0`).

## Contribution

Any contribution is welcome! Remember that you can contribute not only in the code, but also in the documentation or even improve the tests.

Follow the steps below:

- Fork it
- Create your feature branch (git checkout -b my-new-feature)
- Commit your changes (git commit -am 'Added some feature')
- Push to the branch (git push origin my-new-feature)
- Create new Pull Request

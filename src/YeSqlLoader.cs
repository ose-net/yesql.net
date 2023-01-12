using System;
using System.Collections.Generic;
using System.Text;

namespace YeSql.Net;

public class YeSqlLoader
{
    private YeSqlParser _parser;

    private YeSqlValidationResult _validationResult;

    public YeSqlLoader()
    {
        _parser = new YeSqlParser();
        _validationResult = new YeSqlValidationResult();
    }

    public IYeSqlCollection Load()
    {
        throw new NotImplementedException();
    }

    public IYeSqlCollection Load(params string[] files)
    {
        throw new NotImplementedException();
    }

    public IYeSqlCollection Load(string directoryName)
    {
        throw new NotImplementedException();
    }
}

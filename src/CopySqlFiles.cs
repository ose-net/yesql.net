using System;
using System.IO;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace YeSql.Net;

public class CopySqlFiles : Task
{
    [Required]
    public string SourceFolder { get; set; }

    [Required]
    public string DestinationFolder { get; set; }

    public override bool Execute()
    {
        if (string.IsNullOrEmpty(SourceFolder))
        {
            Log.LogError("Source folder not specified");
            return false;
        }
        if (string.IsNullOrEmpty(DestinationFolder))
        {
            Log.LogError("Destination folder not specified");
            return false;
        }

        try
        {
            string[] sqlFiles = Directory.GetFiles(SourceFolder, "*.sql", SearchOption.AllDirectories);
            foreach (string sqlFile in sqlFiles)
            {
                string destinationFile = Path.Combine(DestinationFolder, Path.GetFileName(sqlFile));
                File.Copy(sqlFile, destinationFile, overwrite: true);
            }

        }
        catch(Exception e)
        {
            Log.LogError(e.Message);
            return false;
        }

        return true;
    }
}

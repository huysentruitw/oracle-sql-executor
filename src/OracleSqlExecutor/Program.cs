using System;
using System.Data.Common;
using System.IO;
using System.Linq;
using Oracle.ManagedDataAccess.Client;

namespace OracleSqlExecutor
{
    class Program
    {
        static int Main(string[] args)
        {
            Console.WriteLine("Oracle SQL Executor v1.0");
            Console.WriteLine();

            if (args.Length != 2)
            {
                PrintUsage();
                return -1;
            }

            try
            {
                FindAndExecuteScripts(args[0], args[1]);
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return -2;
            }
        }

        static void PrintUsage()
        {
            Console.WriteLine("Usage:");
            Console.WriteLine("  dotnet OracleSqlExecutor.dll \"<scripts_path>\" \"<connection_string>\"");
            Console.WriteLine();
            Console.WriteLine("Where:");
            Console.WriteLine("  scripts_path       Path to folder containing SQL scripts (with .sql as extension)");
            Console.WriteLine("  connection_string  The database connection string");
            Console.WriteLine();
        }

        static void FindAndExecuteScripts(string scriptsPath, string connectionString)
        {
            var scriptFilePaths = Directory.GetFiles(scriptsPath, "*.sql");
            if (scriptFilePaths.Length <= 0)
                throw new InvalidOperationException($"The folder \"{scriptsPath}\" does not contain any .sql scripts");

            using (DbConnection connection = new OracleConnection(connectionString))
            {
                connection.Open();
                foreach (var scriptFilePath in scriptFilePaths.OrderBy(x => x))
                {
                    Console.WriteLine($"Executing script '{Path.GetFileName(scriptFilePath)}'");
                    var sqlScript = File.ReadAllText(scriptFilePath);
                    ExecuteScript(connection, sqlScript);
                }
            }
        }

        static void ExecuteScript(DbConnection connection, string sqlScript)
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = sqlScript;
                command.ExecuteNonQuery();
            }
        }
    }
}

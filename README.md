# Oracle SQL Executor

[![Build status](https://ci.appveyor.com/api/projects/status/avon0u74md3i4ftm/branch/master?svg=true)](https://ci.appveyor.com/project/huysentruitw/oracle-sql-executor/branch/master)

Executes SQL scripts in a folder against an Oracle database.

## Get it on NuGet

    PM> Install-Package OracleSqlExecutor

## Usage

    dotnet OracleSqlExecutor.dll "scriptsFolder" "connectionString"

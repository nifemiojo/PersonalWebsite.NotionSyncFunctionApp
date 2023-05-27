using System.Data;
using System.Data.Common;
using Npgsql;

namespace PersonalWebsite.NotionSyncFunctionApp.Infrastructure.Postgres.Connection;

public class PostgreSqlDatabaseConnectionFactory : IDatabaseConnectionFactory
{
	private readonly string _connectionString;

	public PostgreSqlDatabaseConnectionFactory(string connectionString)
	{
		_connectionString = connectionString;
	}

	public DbConnection GetConnection()
	{
		return new NpgsqlConnection(_connectionString);
	}
}
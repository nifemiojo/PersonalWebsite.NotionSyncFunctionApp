using System.Data;
using System.Data.Common;
using Npgsql;

namespace PersonalWebsite.NotionSyncFunctionApp.Infrastructure.Postgres.Connection;

internal class DatabaseConnectionFactory : IDatabaseConnectionFactory
{
	private readonly string _connectionString;

	public DatabaseConnectionFactory(string connectionString)
	{
		_connectionString = connectionString;
	}

	public DbConnection GetConnection()
	{
		return new NpgsqlConnection(_connectionString);
	}
}
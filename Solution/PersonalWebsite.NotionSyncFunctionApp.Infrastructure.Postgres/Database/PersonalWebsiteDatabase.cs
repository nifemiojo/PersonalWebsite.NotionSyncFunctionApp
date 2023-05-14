using Dapper;
using System.Data;
using PersonalWebsite.NotionSyncFunctionApp.Infrastructure.Postgres.DTOs;
using PersonalWebsite.NotionSyncFunctionApp.Infrastructure.Postgres.Connection;

namespace PersonalWebsite.NotionSyncFunctionApp.Infrastructure.Postgres.Database;

public class PersonalWebsiteDatabase : IDatabase
{
    private readonly IDatabaseConnectionFactory _databaseConnectionFactory;

    public PersonalWebsiteDatabase(IDatabaseConnectionFactory databaseConnectionFactory)
    {
        _databaseConnectionFactory = databaseConnectionFactory;
    }

    public async Task UpsertCategoriesStoredProcedureAsync(List<CategoryDto> categoryDtos)
    {
        var parameters = new { rows_to_insert = categoryDtos.Select(x => new { x.NotionId, x.Name }).ToArray() };

        await using var connection = _databaseConnectionFactory.GetConnection();

        await connection.ExecuteAsync("blog.upsert_categories", parameters, commandType: CommandType.StoredProcedure);
    }
}
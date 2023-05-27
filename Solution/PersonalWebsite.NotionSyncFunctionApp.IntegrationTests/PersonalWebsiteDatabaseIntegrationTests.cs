using Dapper;
using FluentAssertions;
using NUnit.Framework;
using PersonalWebsite.NotionSyncFunctionApp.Infrastructure.Postgres.Connection;
using PersonalWebsite.NotionSyncFunctionApp.Infrastructure.Postgres.Database;
using PersonalWebsite.NotionSyncFunctionApp.Infrastructure.Postgres.DTOs;

namespace PersonalWebsite.NotionSyncFunctionApp.IntegrationTests;

internal class PersonalWebsiteDatabaseIntegrationTests
{
	private PersonalWebsiteDatabase _database;
	private IDatabaseConnectionFactory _databaseConnectionFactory;

	[SetUp]
	public void Setup()
	{
		// Set up the database connection factory for testing purposes
		_databaseConnectionFactory = GetTestDatabaseConnectionFactory();

		// Create an instance of the PersonalWebsiteDatabase class
		_database = new PersonalWebsiteDatabase(_databaseConnectionFactory);
	}

	private IDatabaseConnectionFactory GetTestDatabaseConnectionFactory()
	{
		string connectionString = "";
		return new PostgreSqlDatabaseConnectionFactory(connectionString);
	}

	private void ClearCategoryTable()
	{
		using var connection = _databaseConnectionFactory.GetConnection();
		connection.Execute("TRUNCATE TABLE blog.category CASCADE");
	}

	[Test]
	public async Task UpsertCategoriesStoredProcedure_Test()
	{
		ClearCategoryTable();

		// Arrange
		var testCategories = new List<CategoryDto>
		{
			new CategoryDto { NotionEntityId = "notion_entity_id_1", Name = "Category 1" },
			new CategoryDto { NotionEntityId = "notion_entity_id_2", Name = "Category 2" },
		};

		// Act
		// Execute the stored procedure
		await _database.UpsertCategoriesStoredProcedureAsync(testCategories);

		// Assert
		// Verify the data in the database
		await using var connection = _databaseConnectionFactory.GetConnection();
		var retrievedCategories = await connection.QueryAsync<CategoryDto>("SELECT category_name AS Name, notion_entity_id AS NotionId FROM blog.category");

		// Assert that the retrieved categories match the expected values
		retrievedCategories.Should().HaveCount(testCategories.Count);

		foreach (var expectedCategory in testCategories)
		{
			retrievedCategories
				.Should()
				.ContainSingle(databaseCategory => databaseCategory.NotionEntityId == expectedCategory.NotionEntityId)
				.Which.Name.Should().Be(expectedCategory.Name);
		}
	}

}
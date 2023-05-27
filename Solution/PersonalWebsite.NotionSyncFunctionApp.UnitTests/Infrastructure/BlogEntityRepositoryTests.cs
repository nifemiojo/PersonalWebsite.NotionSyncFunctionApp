using NSubstitute;
using NUnit.Framework;
using PersonalWebsite.NotionSyncFunctionApp.Domain;
using PersonalWebsite.NotionSyncFunctionApp.Infrastructure.Postgres.Database;
using PersonalWebsite.NotionSyncFunctionApp.Infrastructure.Postgres.Dtos;
using PersonalWebsite.NotionSyncFunctionApp.Infrastructure.Postgres.DTOs;
using PersonalWebsite.NotionSyncFunctionApp.Infrastructure.Postgres.Repository;

namespace PersonalWebsite.NotionSyncFunctionApp.UnitTests.Infrastructure;

[TestFixture]
public class BlogEntityRepositoryTests
{
	private IDatabase _database;
	private IBlogEntityToDtoMapper _blogEntityToDtoMapper;
	private BlogEntityRepository _repository;

	[SetUp]
	public void SetUp()
	{
		_database = Substitute.For<IDatabase>();
		_blogEntityToDtoMapper = Substitute.For<IBlogEntityToDtoMapper>();
		_repository = new BlogEntityRepository(_database, _blogEntityToDtoMapper);
	}

	[Test]
	public async Task UpsertAsync_WithCategoryEntity_CallsUpsertCategoriesStoredProcedureAsync()
	{
		// Arrange
		var entitiesToUpsert = new List<BlogEntity> { new Category() };

		var categoryDto = new CategoryDto();
		_blogEntityToDtoMapper.MapToDtos(entitiesToUpsert).Returns(new List<BlogEntityDto> { categoryDto });

		// Act
		await _repository.UpsertAsync<Category>(entitiesToUpsert);

		// Assert
		await _database.Received(1).UpsertCategoriesStoredProcedureAsync(Arg.Is<List<CategoryDto>>(list =>
			list.Count == 1 && list.Contains(categoryDto)));
	}
}

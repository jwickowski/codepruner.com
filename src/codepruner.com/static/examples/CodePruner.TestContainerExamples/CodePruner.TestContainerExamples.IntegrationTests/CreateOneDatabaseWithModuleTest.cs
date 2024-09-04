using CodePruner.TestContainerExamples.EF;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Testcontainers.MsSql;

namespace CodePruner.TestContainerExamples.IntegrationTests;

public class CreateOneDatabaseWithModuleTest(DatabaseContainerModuleFixture fixture) : IClassFixture<DatabaseContainerModuleFixture>
{
    [Fact]
    public async Task insert_value_to_database()
    {
        await using var dbContext = fixture.CreateDbContext();
        dbContext.Articles.Add(new Article
        {
            Id = Guid.NewGuid(),
            Url = "the-best-way-for-integrations-tests-with-testcontainers",
            Title = "The best way for integrations tests with testcontainers",
            Content = "Content..."
        });
        await dbContext.SaveChangesAsync();
    }

    [Fact]
    public async Task insert_and_select_value_to_database()
    {
        await using (var dbContextToSave = fixture.CreateDbContext())
        {
            dbContextToSave.Articles.Add(new Article
            {
                Id = Guid.NewGuid(),
                Url = "example-url",
                Title = "The best way for integrations tests with testcontainers",
                Content = "Content..."
            });
            await dbContextToSave.SaveChangesAsync();
        }

        await using (var dbContextToRead = fixture.CreateDbContext())
        {
            Assert.Equal(1, dbContextToRead.Articles.Count(x => x.Url == "example-url"));
        }
    }

    [Fact]
    public async Task double_insert_with_unique_constraint_to_database()
    {
        var url = "url-should-be-unique";
        await using (var dbContextToSave = fixture.CreateDbContext())
        {
            dbContextToSave.Articles.Add(new Article
            {
                Id = Guid.NewGuid(),
                Url = url,
                Title = "title one",
                Content = "Content one..."
            });
            await dbContextToSave.SaveChangesAsync();
            dbContextToSave.Articles.Add(new Article
            {
                Id = Guid.NewGuid(),
                Url = url,
                Title = "title two",
                Content = "Content two..."
            });
            var exception = await Assert.ThrowsAsync<DbUpdateException>(async () => await dbContextToSave.SaveChangesAsync());
            Assert.Contains("IX_Articles_Url", exception.InnerException!.Message);
        }
    }
}

public class DatabaseContainerModuleFixture
{
    public string ConnectionString { get; private set; } = "";
    public DatabaseContainerModuleFixture()
    {
        InitSql().Wait();
        RunMigration().Wait();
    }
    #region init_sql
    private async Task InitSql()
    {
        var container = new MsSqlBuilder()
        .WithImage("mcr.microsoft.com/mssql/server:2019-CU18-ubuntu-20.04")
        .Build();
        await container.StartAsync();


        var connectionStringBuilder = new SqlConnectionStringBuilder(container.GetConnectionString())
        {
            InitialCatalog = "test-database",
        };

        ConnectionString = connectionStringBuilder.ConnectionString;
    }
    #endregion

    private async Task RunMigration()
    {
        await using var dbContext = CreateDbContext();
        await dbContext.Database.EnsureCreatedAsync();
    }

    public CodePrunerDbContext CreateDbContext()
    {
        var optionsBuilder = new DbContextOptionsBuilder<CodePrunerDbContext>();
        optionsBuilder.UseSqlServer(ConnectionString);

        var dbContext = new CodePrunerDbContext(optionsBuilder.Options);
        return dbContext;
    }
}
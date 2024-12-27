using CodePruner.TestContainerExamples.EF;
using DotNet.Testcontainers.Builders;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CodePruner.TestContainerExamples.IntegrationTests;

public class CreateDatabaseInTestClassTest
{
    #region tests
    [Fact]
    public async Task insert()
    {
        var connectionstring = await InitSql();
        await RunMigration(connectionstring);

        await using var dbContext = CreateDbContext(connectionstring);
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
    public async Task insert_and_select()
    {
        var connectionstring = await InitSql();
        await RunMigration(connectionstring);

        await using (var dbContextToSave = CreateDbContext(connectionstring))
        {
            dbContextToSave.Articles.Add(new Article
            {
                Id = Guid.NewGuid(),
                Url = "the-best-way-for-integrations-tests-with-testcontainers",
                Title = "The best way for integrations tests with testcontainers",
                Content = "Content..."
            });
            await dbContextToSave.SaveChangesAsync();
        }

        await using (var dbContextToRead = CreateDbContext(connectionstring))
        {
            Assert.Equal(1, dbContextToRead.Articles.Count());
        }
    }

    [Fact]
    public async Task try_double_insert_with_unique_constraint()
    {
        var connectionstring = await InitSql();
        await RunMigration(connectionstring);
        var url = "url-should-be-unique";
        await using (var dbContextToSave = CreateDbContext(connectionstring))
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
    #endregion
    #region init_sql
    private async Task<string> InitSql()
    {
        var password = "yourStrong(!)Password";
        var container = new ContainerBuilder()
            .WithImage("mcr.microsoft.com/mssql/server:2019-CU18-ubuntu-20.04")
            .WithPortBinding(1433, true)
            .WithEnvironment("ACCEPT_EULA", "Y")
            .WithEnvironment("MSSQL_PID", "Express")
            .WithEnvironment("MSSQL_SA_PASSWORD", password)
            .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(1433))
            .Build();

        await container.StartAsync();

        var connectionStringBuilder = new SqlConnectionStringBuilder()
        {
            UserID = "sa",
            Password = password,
            InitialCatalog = "test-database",
            DataSource = $"{container.Hostname},{container.GetMappedPublicPort(1433)}",
            TrustServerCertificate = true
        };

       return  connectionStringBuilder.ConnectionString;
    }
    private CodePrunerDbContext CreateDbContext(string connectionString)
    {
        var optionsBuilder = new DbContextOptionsBuilder<CodePrunerDbContext>();
        optionsBuilder.UseSqlServer(connectionString);

        var dbContext = new CodePrunerDbContext(optionsBuilder.Options);
        return dbContext;
    }

    #endregion
    #region run_migration
    private async Task RunMigration(string connectionString)
    {
        await using var dbContext = CreateDbContext(connectionString);
        await dbContext.Database.MigrateAsync();
    }
    #endregion
   

}

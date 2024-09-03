using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace CodePruner.TestContainerExamples.EF;

public class CodePrunerDbContextFactory : IDesignTimeDbContextFactory<CodePrunerDbContext>
{
    public CodePrunerDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<CodePrunerDbContext>();
        optionsBuilder.UseSqlServer();
        return new CodePrunerDbContext(optionsBuilder.Options);
    }
}

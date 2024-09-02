using Microsoft.EntityFrameworkCore;

namespace CodePruner.TestContainerExamples.EF;

public class CodePrunerDbContext : DbContext
{
    public CodePrunerDbContext(DbContextOptions<CodePrunerDbContext> options) : base(options)
    {
    }
    
    DbSet<Article> Articles { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new ArticleConfiguration());
        base.OnModelCreating(builder);
    }
}

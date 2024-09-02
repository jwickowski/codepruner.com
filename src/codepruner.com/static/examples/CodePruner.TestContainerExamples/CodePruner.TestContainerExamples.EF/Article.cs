using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodePruner.TestContainerExamples.EF;

public class Article
{
    public Guid Id { get; set; }

    public string Title { get; set; }
    public string Content { get; set; }
    public string Url { get; set; }
}

public class ArticleConfiguration : IEntityTypeConfiguration<Article>
{
    public void Configure(EntityTypeBuilder<Article> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title).HasMaxLength(500);
        builder.Property(x=> x.Content).HasMaxLength(-1);
        builder.Property(x => x.Url).HasMaxLength(-1);
        builder.HasIndex(x => x.Url).IsUnique();

    }
}

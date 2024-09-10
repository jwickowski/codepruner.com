---
author: Jerzy Wickowski
categories:
  - dotnet
date: 2024-09-10T05:40:58.000Z
disqus_identifier: how-to-configure-entity-framework-with-migrations-tutorial
disqus_title: How to configure EntityFramework with migrations - tutorial
disqus_url: 'https://codepruner.com/how-to-configure-entity-framework-with-migrations-tutorial'
draft: false
images:
  - images/posts/2024/2024-09-10-how-to-configure-entity-framework-with-migrations-tutorial.jpg
tags:
  - dotnet
  - EntityFramework
  - code
  - implementation
  - SQL
title: How to configure EntityFramework with migrations - tutorial
type: regular
url: how-to-configure-entity-framework-with-migrations-tutorial
---
When I start a new project in .NET and I need to persist data, EntityFramework is my 1st choice. Why? It is the most common approach in .NET world and it allows to start application easily. In this article I will guide you through configuring EntityFramework with migrations including  some tricks and good practices.  

## DataModel and its configuration
The first think we need to do is creating a data model to store in the database. I have prepared `Article` class to store it:  

{{<code language="csharp" file="static/examples/CodePruner.TestContainerExamples/CodePruner.TestContainerExamples.EF/Article.cs" region="article_class" >}}

Data model is not enough. You need to add also a bit of metadata to it. You can use attributes DataAttributes or FluentConfiguration. I am the of of the second option, because I think it is cleaner and there are more capabilities. It can be like:
{{<code language="csharp" file="static/examples/CodePruner.TestContainerExamples/CodePruner.TestContainerExamples.EF/Article.cs" region="article_configuration" >}}

You can see, to go with FluentConfiguration you have to implement `IEntityTypeConfiguration<T>` and implement `Configure(EntityTypeBuilder<T> builder)` method. What you can see here:
 - Setting Id as primary key
 - Setting your column lengths
 - Adding unique index on Url column

## DbContext
Next thing you need to do is creating your own DbContext. In our case it is:
{{<code language="csharp" file="static/examples/CodePruner.TestContainerExamples/CodePruner.TestContainerExamples.EF/CodePrunerDbContext.cs" >}}

There are some facts:
1. You need to inherit from DbContext
2. Constructor should accept `DbContextOptions<CodePrunerDbContext> options`. It is required to:
    - Pass `ConnectionString` later
    - Setting database type, like `SqlServer`, `Postgres`, `SQLite`
3. Add `DbSet<Article> Articles`. Sometimes it can be omitted, but I suggest to add it by default, because it makes working with EF nicer
4. In `OnModelCreating` I have applied the configuration.
    - You can do something like: `builder.ApplyConfigurationsFromAssembly(typeof(CodePrunerDbContext).Assembly);` to avoid adding configuration in the future. They will be loaded automatically.

## Adding migration
When you have all of the previous steps done you are almost ready to add the migration. Before you do it I recommend to add context factory to simplify adding migrations, because `dotnet` will know how to construct the DbContext.
{{<code language="csharp" file="static/examples/CodePruner.TestContainerExamples/CodePruner.TestContainerExamples.EF/CodePrunerDbContextFactory.cs" >}}

Ok. Now you are ready to add the 1st migration. To do it, go to the directory with you EF project and add migration:
``` 
cd .\CodePruner.TestContainerExamples.EF
dotnet ef migrations add AddArticle
```

After it migration will be created. If you have any problem with that, let me know in the comments sections below.
The migration will create couple of files:
- `CodePrunerDbContextModelSnapshot.cs` - It is metadata information for EntityFramework to know how to create next migrations. eg. which table exists and should be deleted or altered.
- `20240903064641_AddArticle` - It is a single migration. These numbers at the beginning represent date when migration was created. It is important to keep them in order. It should look like:
{{<code language="csharp" file="static/examples/CodePruner.TestContainerExamples/CodePruner.TestContainerExamples.EF/Migrations/20240903064641_AddArticle.cs" >}}

## Creating database and executing migrations
At this moment you can create/update the database with command:
`dotnet ef database update --connection "ConnectionString"`

or when you create DbContext you can run migration:
```csharp
 private async Task RunMigration()
 {
     await using var dbContext = CreateDbContext();
     await dbContext.Database.MigrateAsync();
     // or await dbContext.Database.EnsureCreatedAsync();
 }
```

### The difference between `EnsureCreatedAsync` and `MigrateAsync`
Both of them will create database as we want, but when we go a bit deeper.
- `EnsureCreateDatabaseAsync` - Will just create database, but it wont care about any migrations. So it is a better choice for test cases when we just need a created database. Documentation describes the behavior:
   - If the database exists and has any tables, then no action is taken. Nothing is done to ensure the database schema is compatible with the Entity Framework model.
   - If the database exists but does not have any tables, then the Entity Framework model is used to create the database schema.
  - If the database does not exist, then the database is created and the Entity Framework model is used to create the database schema.
- `MigrateAsync` - It also create database if it doesn't exist, but if it does it will process migration steps to update it to the newest version. 

If you would like to see a speed comparison, let me know in the comments, then I will prepare it for you. 

## Summary 
It is everything for today. Is is useful for you? Would you like to add or ask anything? Let me know in the comment below.
See you next time.

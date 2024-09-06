---
author: Jerzy Wickowski
categories:
  - dotnet
date: 2024-09-09T05:40:58.000Z
draft: true
images:
  - images/posts/2024/XXXXXX.jpg
tags:
  - dotnet
title: How to configure EntityFramework with migrations - tutorial 
type: regular
---
When I start a new project in .NET and I need to persist data, EntityFramework is my 1st choice option. It it the most common approach in .NET world and it allows to start application easily. In this article I will guide through configuring EntityFramework with some tricks and good practices.  

## Preparing DbContext
The 1st think we need to do is creating a data model to store in the database. I have prepared `Article` class to store it:  

{{<code language="csharp" file="static/examples/CodePruner.TestContainerExamples/CodePruner.TestContainerExamples.EF/Article.cs" region="article_class" >}}




and Article table configuration:
{{<code language="csharp" file="static/examples/CodePruner.TestContainerExamples/CodePruner.TestContainerExamples.EF/Article.cs" region="article_configuration" >}}

You can see, I have added the Unique index on Url column. It will be required later to check if we really run our test on database engine.
then add DbContext:
{{<code language="csharp" file="static/examples/CodePruner.TestContainerExamples/CodePruner.TestContainerExamples.EF/CodePrunerDbContext.cs" >}}

You can see that Article and ArticleConfiguration is added.
Then we add `CodePrunerDbContextFactory` to simplify running migration when we have it then `ef migrations` will know how to create DbContext to create a migration for example.
{{<code language="csharp" file="static/examples/CodePruner.TestContainerExamples/CodePruner.TestContainerExamples.EF/CodePrunerDbContextFactory.cs" >}}

and it is almost everything.
Now you should create a migration: 
``` 
cd .\CodePruner.TestContainerExamples.EF
dotnet ef migrations add AddArticle
```

after it migration will be created. The migration will create couple of files:
- `CodePrunerDbContextModelSnapshot.cs` - It is metadata information for EntityFramework to know how to create next migrations. Which table exists and should be deleted or altered.
- `20240903064641_AddArticle` - It is a single migration. These numbers at the beginning represent date when migration was created. It is important to keep them in order. It should look like:
{{<code language="csharp" file="static/examples/CodePruner.TestContainerExamples/CodePruner.TestContainerExamples.EF/Migrations/20240903064641_AddArticle.cs" >}}

At this moment you can create/update the database with command:
`dotnet ef database update --connection "ConnectionString"`
or when you create DbCOntext you can run migration:
```csharp
 private async Task RunMigration()
 {
     await using var dbContext = CreateDbContext();
     await dbContext.Database.MigrateAsync();
     // or      await dbContext.Database.EnsureCreatedAsync();
 }
```
## The difference between `EnsureCreatedAsync` and `MigrateAsync`
Four our example the differece isn't important, because both of them will create database as we want, but when we go a bit deeper.
- `EnsureCreateDatabaseAsync` - Will just create database, but it wont care about any migrations. So it is a better choice for test cases when we just need a created database. Documentation describes the bechaviour:
   - if the database exists and has any tables, then no action is taken. Nothing is done to ensure the database schema is compatible with the Entity Framework model.
   - If the database exists but does not have any tables, then the Entity Framework model is used to create the database schema.
    - If the database does not exist, then the database is created and the Entity Framework model is used to create the database schema.
- `MigrateAsync` - It also create database if it doesn't exist, but if does it will process migration steps to update it to the newest version. 

If you would like to see a speed comparision, let me know in the comments, then I will prepare it for you. 


## Summary 
It is everything for today. Is is useful for you? Would you like to add or ask anything? Let me know in the comment below.
See you next time.

<!-- I know it was shorter material than usual, but I believe it will be expanded in the future.
If you have any question, let me know in comments section below.
If you want to be informed about new posts, subscribe. -->
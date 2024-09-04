---
author: Jerzy Wickowski
categories:
  - dotnet
date: 2024-09-04T05:40:58.000Z
draft: true
images:
  - images/posts/2024/2024-08-08-azure-machine-learning-services-for-az-900.jpg
tags:
  - devops
title: 2024-09-04-the-best-way-for-integrations-tests-with-testcontainers
type: regular
---
Testing is important. Test are more important. There are multiple ways to categorize tests like [`Unit`, `Integration`, `E2E`] or [`BlackBox`, `WhiteBox`] or [`Smoke`, `Regression`] etc. In some cases Unit Tests are enough, because   writing integration test are simples in some cases. When? For example when you use Sql or when you have a dependency to an external service like `Apache Kafka` 

## Our case for today
My idea it noMy idea it not to create a complex scenario, but to show you how to create a docker container for integration test purposes. So I will focus on the most common scenario for .NET like App + EntityFramework + SqlServer. So the scenario will contain:
1. Creating `SqlServer` container for a test case
2. Run migration to have a correct structure
3. Execute Insert SQL
4. Execute Select SQL
5. Test unique constraint in that scenario

The whole project is in this repository in [](\src\codepruner.com\static\examples\CodePruner.TestContainerExamples)

## Preparing database project
Before we start writing integration test we should prepare a database project. Let's start with Article class:
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
 }
```



- add xunit project `CodePruner.TestContainerExamples.IntegrationTests`
- add `dotnet add package Testcontainers`
- tutaj konfiguracja testów:
- przyklad z `docker ps`
```
CONTAINER ID   IMAGE                                                   COMMAND                  CREATED          STATUS          PORTS                     NAMES
1cf171523b96   mcr.microsoft.com/mssql/server:2019-CU18-ubuntu-20.04   "/opt/mssql/bin/perm…"   4 seconds ago    Up 4 seconds    0.0.0.0:50557->1433/tcp   laughing_shockley
138497c29e35   mcr.microsoft.com/mssql/server:2019-CU18-ubuntu-20.04   "/opt/mssql/bin/perm…"   18 seconds ago   Up 18 seconds   0.0.0.0:50548->1433/tcp   elastic_swanson
ca0435e51f90   mcr.microsoft.com/mssql/server:2019-CU18-ubuntu-20.04   "/opt/mssql/bin/perm…"   33 seconds ago   Up 32 seconds   0.0.0.0:50542->1433/tcp   sleepy_shannon
5abee30ab59e   testcontainers/ryuk:0.6.0                               "/bin/ryuk"              33 seconds ago   Up 33 seconds   0.0.0.0:50539->8080/tcp   testcontainers-ryuk-1e294371-ec07-4c3f-8841-e8bd5e271444
```

- test z classFixture - CreateOneDatabaseTest
- nowe docker ps:
```
CONTAINER ID   IMAGE                                                   COMMAND                  CREATED          STATUS          PORTS                     NAMES
2abd95cce2e5   mcr.microsoft.com/mssql/server:2019-CU18-ubuntu-20.04   "/opt/mssql/bin/perm…"   17 seconds ago   Up 17 seconds   0.0.0.0:62044->1433/tcp   great_visvesvaraya
243336dfd2b8   testcontainers/ryuk:0.6.0                               "/bin/ryuk"              18 seconds ago   Up 18 seconds   0.0.0.0:62041->8080/tcp   testcontainers-ryuk-35ee84c6-8647-4719-bc10-0fb85033863b
```
czyli jest tylko jeden konener na klasę

Co jest spoko! POtem może być instancja per assembly, ale to dopiero w xunit v3 => https://xunit.net/docs/getting-started/v3/whats-new


- jeszcze opcja z gotowym mofdułem => https://dotnet.testcontainers.org/modules/mssql/
https://testcontainers.com/modules/mssql/

## Summary 
<!-- I know it was shorter material than usual, but I believe it will be expanded in the future.
If you have any question, let me know in comments section below.
If you want to be informed about new posts, subscribe. -->
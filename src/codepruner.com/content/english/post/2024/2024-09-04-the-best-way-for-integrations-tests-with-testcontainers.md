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

---------------------------
Tutaj pewnie zrobimy dwa różne artykuły o EF
--------------------------

In one of the previous post I have described how to configure EntityFramework migration. We will use that code in today example, because in our environments we need a bit more then just pure database. We expect it will have schema applied. So let's begin our adventure.

## The case for today
My idea it noMy idea it not to create a complex scenario, but to show you how to create a docker container for integration test purposes. So I will focus on the most common scenario for .NET like App + EntityFramework + SqlServer. So the scenario will contain:
1. Creating `SqlServer` container for a test case
2. Run migration to have a correct structure
3. Execute Insert SQL
4. Execute Select SQL
5. Test unique constraint in that scenario

   
## The simples way of configuring TestContainers 
We will start with the simplest, but not optimal way of using testcontainers, but be calm. We will improve it later.
Let's assume you have already created a test project. Then we need to add nuget package:
```
dotnet add package Testcontainers
```
Add code for initialize container with SqlServer:
{{<code language="csharp" file="static/examples/CodePruner.TestContainerExamples/CodePruner.TestContainerExamples.IntegrationTests/CreateDatabaseInTestClassTest.cs" region="init_sql" >}}
There are some interesting things to describe:
- To set image you want to use invoke `WithImage`. In you case we use SqlServer. All de details how to configure the image you can find on [DockerHub](https://hub.docker.com/r/microsoft/mssql-server) .
- `.WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(1433))` - This line is very important, because it tells to testcontainers when the container is ready. There are much more options. YOu can find other WaitStrategies  [here](https://dotnet.testcontainers.org/api/wait_strategies/)
- Port binding - It is the part you have to care. Because there is a high chance you will create more than one container and a port can be assigned  to only one container you need to:
  - Define the port you want to expose with `.WithPortBinding(1433, true)`
  - and later get public port with `container.GetMappedPublicPort(1433)`
Then testcontainers will take care about ports. 

So when it works, let see on out test:

{{<code language="csharp" file="static/examples/CodePruner.TestContainerExamples/CodePruner.TestContainerExamples.IntegrationTests/CreateDatabaseInTestClassTest.cs" region="tests" >}}

all of them pass. IF you don't believe you can clone the repo and run it on your environemnt. As I mentioned at the beggining that approach has some vulnerabilities. I mean for each test new container is created. So when we execute: `docker ps` you will se that 3+1 containers run.
``` text
CONTAINER ID   IMAGE                                                   CREATED          PORTS
1cf171523b96   mcr.microsoft.com/mssql/server:2019-CU18-ubuntu-20.04   4 seconds ago    0.0.0.0:50557->1433/tcp
138497c29e35   mcr.microsoft.com/mssql/server:2019-CU18-ubuntu-20.04   18 seconds ago   0.0.0.0:50548->1433/tcp
ca0435e51f90   mcr.microsoft.com/mssql/server:2019-CU18-ubuntu-20.04   33 seconds ago   0.0.0.0:50542->1433/tcp
5abee30ab59e   testcontainers/ryuk:0.6.0                               33 seconds ago   0.0.0.0:50539->8080/tcp
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
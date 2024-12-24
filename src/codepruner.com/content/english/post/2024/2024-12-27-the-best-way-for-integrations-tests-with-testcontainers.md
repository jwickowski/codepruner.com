---
author: Jerzy Wickowski
categories:
  - dotnet
date: 2024-12-27T05:40:58.000Z
draft: false
images:
  - images/posts/2024/2024-12-27-azure-machine-learning-services-for-az-900.jpg
tags:
  - testcontainers
  - dotnet
  - backend
  - tests
  - c#
title: 2024-12-27-the-best-way-for-integrations-tests-with-testcontainers
type: regular
---
Testing is important. Test are more important. There are multiple ways to categorize tests like [`Unit`, `Integration`, `E2E`]. When it is possible I recommend using UnitTests, but in some cases they are not enough. In some situations writing integration test are simpler and faster. When? For example when using external services are required. you use Sql or when you have a dependency to an external service like `Apache Kafka` 

The whole project is in this repository in [](\src\codepruner.com\static\examples\CodePruner.TestContainerExamples)

In one of the previous post I have described [how to configure EntityFramework migration](<<{ relref "./2024-09-10-how-to-configure-entity-framework-with-migrations-tutorial.md"}>>). We will use that code in today example, because in our environments we need a bit more then just pure database. We expect it will have schema applied. So let's begin our adventure.

## The case for today
My idea is not to create a complex scenario, but to show you how to write integration test faster without pain in the arse. So I will focus on the most common scenario for .NET like App + EntityFramework + SqlServer. So the scenario will contain:
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
- To set image you want to use invoke `WithImage`. In you case we use SqlServer. All de details how to configure the image you can find on [DockerHub](https://hub.docker.com/r/microsoft/mssql-server).
- `.WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(1433))` - This line is very important, because it tells testcontainers when the container is ready. There are much more options. You can find other WaitStrategies [here](https://dotnet.testcontainers.org/api/wait_strategies/)
- Port binding - It is the part you have to care. Because there is a high chance you will create more than one container and a port can be assigned to only one container you need to:
  - Define the port you want to expose with `.WithPortBinding(1433, true)`
  - and later get public port with `container.GetMappedPublicPort(1433)`
Then testcontainers will take care about ports. 

So when it works, let see on our test:

{{<code language="csharp" file="static/examples/CodePruner.TestContainerExamples/CodePruner.TestContainerExamples.IntegrationTests/CreateDatabaseInTestClassTest.cs" region="tests" >}}

All of them pass. If you don't believe you can clone the repo and run it on your own environment. As I mentioned at the beginning that approach has some vulnerabilities. I mean for each test new container is created. So when we execute `docker ps` the result it:
``` text
CONTAINER ID   IMAGE                                                   CREATED          PORTS
1cf171523b96   mcr.microsoft.com/mssql/server:2019-CU18-ubuntu-20.04   4 seconds ago    0.0.0.0:50557->1433/tcp
138497c29e35   mcr.microsoft.com/mssql/server:2019-CU18-ubuntu-20.04   18 seconds ago   0.0.0.0:50548->1433/tcp
ca0435e51f90   mcr.microsoft.com/mssql/server:2019-CU18-ubuntu-20.04   33 seconds ago   0.0.0.0:50542->1433/tcp
5abee30ab59e   testcontainers/ryuk:0.6.0                               33 seconds ago   0.0.0.0:50539->8080/tcp
```
You can see 3+1 containers running. One for each test. Now it is not a problem, but when you have more and more test you will have more and more containers. So do something to reduce amount of them.

## Reduce number of containers in xUnit
I use xUnit, so I am going to describe how to achieve it with this test framework. There is a possibility to share resources between tests in one class. I know that theory tells us that tests should be independent. In the most cases it is true, but sometime we need to sacrifice something to achieve something else. It is in that scenario. We sacrifice stateless for better time and lower memory usage. Ok, so how to do it?

1. Create a FixtureClass. It is a normal class, but you need to prepare everything you want to share in constructor. Here is out example:
{{<code language="csharp" file="static/examples/CodePruner.TestContainerExamples/CodePruner.TestContainerExamples.IntegrationTests/CreateOneDatabaseTest.cs" region="fixture_class" >}}

You can ses it is the same code as previous, but in a separate class.
2. Use the fixture. To do this, you need to add interface `IClassFixture<DatabaseContainerFixture>` and pass the fixture by constructor to the test class. Here is an example:
{{<code language="csharp" file="static/examples/CodePruner.TestContainerExamples/CodePruner.TestContainerExamples.IntegrationTests/CreateOneDatabaseTest.cs" region="test_class" >}}

Fantastic. It is time to run these test and check containers with `docker ps`.

```text
CONTAINER ID   IMAGE                                                   CREATED          PORTS
2abd95cce2e5   mcr.microsoft.com/mssql/server:2019-CU18-ubuntu-20.04   17 seconds ago   0.0.0.0:62044->1433/tcp
243336dfd2b8   testcontainers/ryuk:0.6.0                               18 seconds ago   0.0.0.0:62041->8080/tcp
```
Success. There is only one container for test. Fantastic.

### More reducing
You can do more reducing byt limiting a container for assembly wih `AssemblyFixture`, but unfortunately it will be available in xUnit V3. Currently, at the moment of writing the article [it is in beta](https://xunit.net/docs/shared-context).

## Use testcontainers modules 
As you can see in my previous examples I built ConnectionString manually. It works, but some of popular services like: SqlServer, Kafka, RabbitMQ, Redis, etc. there are ready to use modules. Here is an example for SqlServer:
{{<code language="csharp" file="static/examples/CodePruner.TestContainerExamples/CodePruner.TestContainerExamples.IntegrationTests/CreateOneDatabaseWithModuleTest.cs" region="init_sql" >}}
You can read a bit more about ready to use modules on [Testcontainers modules page](https://testcontainers.com/modules/)
As you can see it is much easier.

## Summary 
It is everything for today. Is is useful for you? Would you like to add or ask anything? Let me know in the comment below.

If you have any question, let me know in comments section below.

See you next time.

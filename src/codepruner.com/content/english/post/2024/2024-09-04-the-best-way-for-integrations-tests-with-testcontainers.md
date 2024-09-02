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
My idea it not to create a complex scenario, but to show you how to create a docker container for integration test purposes. I am going to use [TestContainers](https://testcontainers.com/) for it. So out scenario will contain:
1. Creating `SqlServer` container for a test case
2. Run migration to have a correct structure
3. Execute Insert SQL
4. Execute Select SQL
5. Test unique constraint in that scenario

The whole project is in this repository in [](\src\codepruner.com\static\examples\CodePruner.TestContainerExamples)



## Preparing database project


## Summary 
<!-- I know it was shorter material than usual, but I believe it will be expanded in the future.
If you have any question, let me know in comments section below.
If you want to be informed about new posts, subscribe. -->
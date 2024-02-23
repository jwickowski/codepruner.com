---
author: Jerzy Wickowski
categories:
  - dotnet
date: 2024-02-22T11:40:58.000Z
draft: false
images:
  - images/posts/2024/2024-02-22-the-easiest-way-to-run-background-task-in-ASP-Net-Core.jpg
tags:
  - dotNet
  - aspNetCore
  - backgroundTask
  - hangfire
title: The easiest way to run background task in ASP .Net Core
type: trending

---

It is common situation that we would like to run a background tasks in our application. There are many ways to do it, but we will focus on the easiest one.

## Problem to solve
The best option is to show a real life scenario. I have a web application and that allows a user to upload a file. After uploading the file I need to do two things immediately: create a unique FileId and store the file somewhere. After it, I need to process the file, but it can take a bit of time, so I don't want to block the client. I can prepare the response for the client with the FileId, maybe a status, and start processing it in the background. 

## Some ideas
Ok. So we know, that I need to run a background task. What options do we have? 
- In old ASP.NET I was able to use `QueueBackgroundWorkItem`, but in ASP.NET Core it is not possible. 
- I can use `IHostedService` or `BackgroundService`, but to use it I needed to write a bit of code to handle it. I would like to use something simpler. 
- I can use an external service like Azure Functions, but it is an overkill for my simple scenario.
- I can use an library to handle it. After I bit of investigation I have found:
  - [Hangfire](https://www.hangfire.io/) 
    - It is simple to use.
    - It is simple to configure.
    - It has build in dashboard.
    - It has a great documentation.
    - Last change in GitHub was yesterday.
  - [Quartz.NET](https://www.quartz-scheduler.net/) - It is also nice, but a bit more complicated.
    - It requires a bit more configuration.
    - It requires underspending their glossary like Trigger, Job, Scheduler.
    - It also has fantastic documentation.
    - Last change in GitHub was 3 days ago. 


## Hangfire configuration
{{< notice "info" >}}
The full example you can find in the [CodePruner.com repository](https://github.com/jwickowski/codepruner.com/tree/master/src/codepruner.com/static/examples/CodePruner.Examples)
{{</ notice >}}

The 1st think you have to do it to install the Hangfire package.
``` 
Install-Package Hangfire.AspNetCore
Install-Package Hangfire.MemoryStorage
```

Then you have to configure the Hangfire service in the `Program.cs` file. 
{{<code language="csharp" file="static/examples/CodePruner.Examples/CodePruner.Examples.BackgroundTask.Hangfire/Program.cs" region="init_hangfire" >}}

and you can also add Hangfire Dashboard. It is not required, but it is nice to have it. 
{{<code language="csharp" file="static/examples/CodePruner.Examples/CodePruner.Examples.BackgroundTask.Hangfire/Program.cs" region="init_hangfire_dashboard" >}}

and... it is everything to configure Hangfire.

## Use Hangfire to run a background task
Now we can switch to the nicer part. We can create a job that will be run in the background. 
It can look like that: 
{{<code language="csharp" file="static/examples/CodePruner.Examples/CodePruner.Examples.BackgroundTask.Hangfire/Program.cs" region="example_endpoints" >}}

as you can see, there are 2 endpoints `ProcessLargeFileWithoutHangfire` and `ProcessLargeFileWithHangFire`. I have also added a logging to see the difference.

When you do a request `ProcessLargeFileWithoutHangfire` then you can see result like.
```
[12:20:27] Request start without Hangfire
[12:20:27] Processing file 249...
[12:20:37] Processed file 249
[12:20:37] Request end without Hangfire
```

but when you do a request `ProcessLargeFileWithHangFire` then you can see result like.
```
[12:21:36] Request start with Hangfire
[12:21:36] Request end with Hangfire
[12:21:36] Processing file 153...
[12:21:46] Processed file 153
```

As you can see the most important difference is that the request with Hangfire ends immediately, but the processing is done in the background. It is exactly what we wanted to achieve. Now we could add a signalR, but it is not the subject of this post. If you would like to read about it, let me know in the comments section below.notification for example 


More over, you can monitor these tasks in the Hangfire dashboard. You can see the status of the tasks, you can retry them, you can see the history of the tasks. It is really nice. and can look like:
{{< image src="images/posts/2024/2024-02-16-hangfire-dashboard-screen.png" caption="" alt="Example Hangfire dashboard" position="center" command="fill"  class="img-fluid" title="Example Hangfire dashboard" >}}

## More advanced options 
Hangfire has much more advanded capabilieties likse:
- Delayed execution
- Recurring tasks
- Storage options
- Automatic Retries
- Possibility to run tasks on different servers

... but it is a story for a different post.

## Summary
Thank you for reading this article. 
Would you like to add or ask anything? Let me know in the comments below.


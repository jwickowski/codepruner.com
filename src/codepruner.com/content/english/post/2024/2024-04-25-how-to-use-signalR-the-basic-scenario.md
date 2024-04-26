---
author: Jerzy Wickowski
categories:
  - dotnet
date: 2024-04-25T06:40:58.000Z
disqus_identifier: how-to-use-signalr-the-basic-scenario
disqus_title: How to use SignalR the basic scenario - tutorial
disqus_url: 'https://codepruner.com/how-to-use-signalr-the-basic-scenario'
draft: false
images:
  - images/posts/2024/2024-04-25-how-to-use-signalR-the-basic-scenario.jpg
tags:
  - dotNet
  - signalR
  - WebSockets
  - react
title: How to use SignalR the basic scenario - tutorial
type: regular
url: how-to-use-signalr-the-basic-scenario
---

In the previous article, I describe [the idea of SignalR and some typical scenarios where it is useful]({{< relref "./2024-03-05-why-use-signalR-and-how-it-works.md">}}). Today, I will show you a simple but practical configuration SignalR on on both the backend and frontend. I will guide you step by step how to make SignalR working for you and your users. 

## The scenario
The idea of the scenario is to find a balance between simplicity and usefulness. It must be simple so as not to overshadow the main thread, but it should be a bit complicated to find benefits of using it. Let's assume you have a long running process in the backend and you would like to inform the client about the progress. The full process looks like:

1. Client sends a request to the server to start the process
2. Server starts processing
3. Server will send a notification after each step (eg. `InQueue`, `Started`, `Fetched`, `Processed`, `Saved`, `Done`)
4. Client can show the progress to the user

## How to enable SignalR on ASP.NET Core backend

Ok. We know what we want to achieve. Let's start the configuration of the backend.

{{< notice "info" >}}
The full working example you can find in the [CodePruner.com repository](https://github.com/jwickowski/codepruner.com/tree/master/src/codepruner.com/static/examples/CodePruner.Examples).
Check projects:
- `CodePruner.Examples.SignalR.Api`
- `codepruner.examples.signalr.react.webapp`
{{</ notice >}}

### Creating Hub
The 1st thing you have to create is a `Hub`. The `Hub` is a class that inherits from `Hub` class. It is the place where you can define methods that will be used to send messages to the clients. The simple hub can be like:
{{<code language="csharp" file="static/examples/CodePruner.Examples/CodePruner.Examples.SignalR.Api/SignalRCode/ProcessingHub.cs" >}}

The above example will work, but it is the ~~standard~~ old approach to define Hubs. In the current, civilized version you can use the better approach and use generic `Hub<T>` class:
{{<code language="csharp" file="static/examples/CodePruner.Examples/CodePruner.Examples.SignalR.Api/SignalRCode/StronglyTypedProcessingHub.cs" >}}
As you can see you need additional interface to define the contract. I really prefer the second approach. It is more readable and easier to use, because it reduces a risk of typo. 

### Configuring SignalR
When you have a hub, you are ready to configure the rest of the SignalR. You have to add SignalR to services in the `Startup.cs` or `Program.cs` file:
{{<code language="csharp" file="static/examples/CodePruner.Examples/CodePruner.Examples.SignalR.Api/Program.cs" region="register_signalR_services_enum_string_serialization" >}}

As you can see I have added an additional enum configuration: `.PayloadSerializerOptions.Converters.Add(new JsonStringEnumConverter()));`. It is because I want to serialize enums as strings. It is more readable for the client and it is easier to debug and maintain.

You have to also register your `Hub` in the middleware of the application pipeline:
{{<code language="csharp" file="static/examples/CodePruner.Examples/CodePruner.Examples.SignalR.Api/Program.cs" region="map_signalR_processing_hub" >}}
I added two of them to how you that standard and generic hub are registered in the same way, but I guess in your project you will one only one. I suggest the 2nd one, because strongly typed hubs are better.

### Enabling CORS
There is one more thing to do on the backend. CORS policy must be set:
{{<code language="csharp" file="static/examples/CodePruner.Examples/CodePruner.Examples.SignalR.Api/Program.cs" region="enable_cors" >}}
The origin is the address of the frontend. In the real project it should be taken from the configuration.

## How to connect frontend client to SignalR
When everything is configured on the backend, we can switch our attention to the frontend code.

### Installing SignalR client
The 1st thing you have to do is to install the SignalR client.
``` bash
npm i @microsoft/signalr
```

When you have installed the client package you can start using it. I prefer to create a separate file to handle the connection to the SignalR hub. Then you can use with any frontend framework like React, Angular or Vue. The example code is here:
{{<code language="javascript" file="static/examples/CodePruner.Examples/codepruner.examples.signalr.react.webapp/src/signalR/SignalRConnectionInstance.ts" >}}

In shortcut you have to:
- Import the SignalR client
- Create a connection with an address to the SignalR hub
  - the `hubUrl` is the address of the SignalR hub and it should be taken from the configuration
- Start the connection
- Add listeners to handle messages from the server

Ok. We have the initialization of the SignalR. Now you have to import the connection in your application to. Here is an example of it in the React component. The example code is here:
{{<code language="javascript" file="static/examples/CodePruner.Examples/codepruner.examples.signalr.react.webapp/src/signalR/pages/SignalR-01-Processing.tsx" region="get_signalr_connection" >}}
Fantastic! Now when you start the application you should see information about the connection in the console:
``` console
[SignalConnection] Initializing SignalR connection.
[SignalConnection] Connecting to SignalR hub
[SignalConnection] Connected to SignalR hub
```

### Sending notifications from the server
There is only one missing part. When everything is configured you should send message from the server. To achieve it, create an endpoint that will be used to start the process:
{{<code language="csharp"  file="static/examples/CodePruner.Examples/CodePruner.Examples.SignalR.Api/Program.cs" region="file_processing_sync_endpoint" >}}
It imitates long run task. After sending the request it will change the status of the "process" and send notifications vis SignalR.
You can notice how `IHubContext` is configured. When you use strongly typed hubs you should inject `IHubContext<StronglyTypedProcessingHub, IProcessingClient>` instead of `IHubContext<ProcessingHub>`. Then you will be able to use previously defined  methods.

We just need to call this endpoint from the frontend. Here is an example of it in the React component::
{{<code language="tsx" file="static/examples/CodePruner.Examples/codepruner.examples.signalr.react.webapp/src/signalR/pages/SignalR-01-Processing.tsx" region="invoke_server_processing" >}}

Fantastic! Our example is ready. When you run the application and click the button you should see the information about the process in the console. It looks like:

``` console
[SignalConnection] Initializing SignalR connection.
[SignalConnection] Connecting to SignalR hub
[SignalConnection] Connected to SignalR hub
[Click] Sending request to start processing.
c32a0e10-7fc9-4179-9680-7063628d56c0 - InQueue
c32a0e10-7fc9-4179-9680-7063628d56c0 - Started
c32a0e10-7fc9-4179-9680-7063628d56c0 - Fetched
c32a0e10-7fc9-4179-9680-7063628d56c0 - Processed
c32a0e10-7fc9-4179-9680-7063628d56c0 - Saved
[Click] Request is done
c32a0e10-7fc9-4179-9680-7063628d56c0 - Done
```
## Summary

If you want to try the example you can [clone the code](https://github.com/jwickowski/codepruner.com/tree/master/src/codepruner.com/static/examples/CodePruner.Examples) and run it locally.

Do you have any questions about SignalR? Just ask below.

What is the next topic you would like to read about? Let me know in the comments bellow.

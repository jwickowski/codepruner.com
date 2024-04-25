---
author: Jerzy Wickowski
categories:
  - dotnet
date: 2024-04-25T06:40:58.000Z
draft: true
images:
  - images/posts/2024/2024-03-05-why-use-signalR-and-how-it-works.jpg
tags:
  - dotNet
  - signalR
  - WebSockets
  - react
title: How to use SignalR the basic scenario
type: epic
---

Last time I wrote about SignalR, it was about [the idea and typical scenarios where it can be useful]({{< relref "./2024-03-05-why-use-signalR-and-how-it-works.md">}}). Today I would like to show you a simple practical configuration SignalR on backend and frontend. I will guide you step by step to show you how to create a simple application with usage of SignalR. We will not create chat, because it is not very useful, but we will look at a bit more useful scenario like sending an information from backend that a process is done.

## Our scenario
The idea of the scenario is to find a balance between simplicity and usefulness. It should be simple to allow everyone to understand it, but it should be a bit complicated to find benefits of using it. So let's assume that we have a long running process in the backend. We would like to inform the client that the process is done. The client should be informed about the progress of the process. We can image multiple steps:

1. Client sends a request to the server to start the process
2. Server starts processing
3. Server will send a notification after each step of the process like: `InQueue`, `Started`, `Fetched`, `Processed`, `Saved`, `Done`
4. Client can show the progress to the user

Ok. We know what we want to achieve. Let's start with the configuring of the backend.

{{< notice "info" >}}
The full example you can find in the [CodePruner.com repository](https://github.com/jwickowski/codepruner.com/tree/master/src/codepruner.com/static/examples/CodePruner.Examples).
Check projects:
- `CodePruner.Examples.SignalR.Api`
- `codepruner.examples.signalr.react.webapp`
{{</ notice >}}

## How to enable SignalR on ASP.NET Core backend
### Creating Hub
You have to create a SignalR hub. It is a class that inherits from `Hub` class. It is the place where you can define methods that will be used to send messages to the clients. Let's create a simple hub:
{{<code language="csharp" file="static/examples/CodePruner.Examples/CodePruner.Examples.SignalR.Api/SignalRCode/ProcessingHub.cs" >}}

The above is the ~~standard~~ old approach to create a hub. The new approach is to use generic `Hub<T>` class. It can look like:
{{<code language="csharp" file="static/examples/CodePruner.Examples/CodePruner.Examples.SignalR.Api/SignalRCode/StronglyTypedProcessingHub.cs" >}}
I really prefer the second approach. It is more readable and easier to use and I am a huge fan of strongly typed code.

### Configuring SignalR
Now we should register hub in the `Startup.cs` or `Program.cs` file. To do it you have to add a lines:
{{<code language="csharp" file="static/examples/CodePruner.Examples/CodePruner.Examples.SignalR.Api/Program.cs" region="register_signalR_services_enum_string_serialization" >}}

As you can see I have added a additional line `.PayloadSerializerOptions.Converters.Add(new JsonStringEnumConverter()));`. It is because I want to serialize enums as strings. It is more readable for the client and it is easier to debug and maintain  in the future.

Now we have to add a middleware to the pipeline. It is very simple. You have to add a line:
{{<code language="csharp" file="static/examples/CodePruner.Examples/CodePruner.Examples.SignalR.Api/Program.cs" region="map_signalR_processing_hub" >}}

I have added two of them. The first one is for the standard hub and the second one is for the strongly typed hub. You can choose one of them. I prefer the second one.

There is one last think to do on the backend. you have to add a CORS policy. It is very simple. You have to add:
{{<code language="csharp" file="static/examples/CodePruner.Examples/CodePruner.Examples.SignalR.Api/Program.cs" region="enable_cors" >}}
The origin is the address of the frontend. It is important to maintain it in the future to improve the security.

Currently it everything what you need to configure SignalR on the backend for the simplest scenario. Now we can move to the frontend.

## How to connect frontend client to SignalR
### Installing SignalR client
The 1st thing you have to do is to install the SignalR client.
``` bash
npm i @microsoft/signalr
```

When you have installed the client package you can start using it. The example code is here:
{{<code language="javascript" file="static/examples/CodePruner.Examples/codepruner.examples.signalr.react.webapp/src/signalR/SignalRConnectionInstance.ts" >}}
In shortcut you have to:
- Import the SignalR client
- Create a connection with an address to the SignalR hub
- Start the connection
- Add event listeners to handle messages from the server

Ok. We have the initialization of the signalR in the ts file. Now we can use it in the React component. The example code is here:
{{<code language="javascript" file="static/examples/CodePruner.Examples/codepruner.examples.signalr.react.webapp/src/signalR/pages/SignalR-01-Processing.tsx" region="get_signalr_connection" >}}
Fantastic. Now when you start the application you should see  information about the connection in the console. It is the first step to connect to the SignalR hub. Now we can move to the next step.

``` console
[SignalConnection] Initializing SignalR connection.
[SignalConnection] Connecting to SignalR hub
[SignalConnection] Connected to SignalR hub
```

### Sending messages to the server
Now when everything is configured we have to configure sending message from the server, because it is the most important part of the scenario. At the beginning we have to add an endpoint that will be used to start the process. The example code is here:
{{<code language="csharp"  file="static/examples/CodePruner.Examples/CodePruner.Examples.SignalR.Api/Program.cs" region="enable_cors" >}}
and we have to handle it on the front end:

{{<code language="tsx" file="static/examples/CodePruner.Examples/codepruner.examples.signalr.react.webapp/src/signalR/pages/SignalR-01-Processing.tsx" region="invoke_server_processing" >}}

Fantastic! Our example is ready. When you run the application and click the button you should see the information about the process in the console. It should look like:


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

As you can see, everything is working. If you would like to run it you can [clone the code](https://github.com/jwickowski/codepruner.com/tree/master/src/codepruner.com/static/examples/CodePruner.Examples) and run it locally.
Do you have any questions about SignalR? Let me know in the comments bellow.

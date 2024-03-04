---
author: Jerzy Wickowski
categories:
  - dotnet
date: 2024-02-27T11:40:58.000Z
draft: true
images:
  - images/posts/2024/2024-02-27-how-and-why-use-signalR.jpg
tags:
  - dotNet
  - signalR
title: How and why use SignalR
type: regular
---

Everyone, who creates web applications, knows how the communication in the internet looks like. It is mostly a request and response. The client sends a request and the server returns a response. But what if you would like to reverse it. To send information from server to client or event to many clients? Let's look at this idea a bit closer. 

## When can it be useful?
Before we start talking about solutions I would like to show you a bit of real scenarios we would like to solve with SignalR.

### Real time notifications
I put it in the 1st place, because it is the most useful scenario. Let assume you have a long running process in the backend and you don't want to force the client to wait until the whole process is done. You would like to return a transactionId and inform a client about hte progress asynchronously. Example:
1. Client uploads a file to the server
2. Server returns unique file identifier
3. Server starts processing the file
4. Server sends a notification to the client about the progress of the processing
5. Client can show the progress to the user
6. When the process is done, the server sends a notification to the client that the process is done.
7. Client can show the result to the user.

It is much more useful, when you realize that the processing can be ran on a different server. So it is easier to scale.

### Real time dashboards
In financial applications or in administrations panels it is very important to display live data. To achieve it you have to feed the frontend with the most up to date data all the time. It is possible difficult with classic request-response approach and it is also not very efficient because:
- Client doesn't have the knowledge when new data are available. It can do a request in intervals.
- With request-response approach we need more resources, because then communication in two ways is required.
- Sometimes there are not new data so the request is unnecessary.

The better approach would be like:
- Server send information to the client when new data are available.
- Server doesn't send information when there are no new data.
- Client doesn't have to think about asking. Data just will be provided.

So it is better to use SignalR to solve all of these problems and every one like when charts are moving :).

### Chat 
It is the most popular SignalR example. It is a fantastic case to show, because it is really nice flow. You can see that message is send from one client to server and then it is sent to all clients. Moreover, there is important thing to mention, that it must be real time and you must be sure that message will be sent only to clients in specific chat room. But to be honest, it is not very  practical example. When did you implement chat last time? But to do it for fun. I agree :).

But be honest how often do you need to create a chat? Not often, I guess.

## How signalR works



// Myślę, że to będzie dobra opcja, żeby podzielić na dwa wpisy. 
// jeden powyzej o tym dlaczego i jak to działa na wysokim poziomie
// drugi techniczny jak to skonfigurować i jak to działa w praktyce

## How to configure it on backend


## How to configure it in frontend


## Summary
It is only the basic introduction to SignalR. I am planning to go deeper in the future. If you are interested in sepcific scenario, let me know in comment. 


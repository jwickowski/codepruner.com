---
author: Jerzy Wickowski
categories:
  - dotnet
date: 2024-03-05T06:40:58.000Z
disqus_identifier: why-use-signalr-and-how-it-works
disqus_title: Why use SignalR and how it works
disqus_url: 'https://codepruner.com/why-use-signalr-and-how-it-works'
draft: true
images:
  - images/posts/2024/2024-03-05-why-use-signalR-and-how-it-works.jpg
tags:
  - dotNet
  - signalR
  - WebSockets
title: Why use SignalR and how it works
type: regular
url: why-use-signalr-and-how-it-works
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
It is the most popular SignalR example. It is a fantastic case to show, because it is really nice flow. Message message is send from one client to server and then it is sent to all clients. Moreover, it must be done in real time and the message must be sent only to clients in specific chat room. I can see only one problem with that example. I have never had to write chat, except studies. But of course it is a fantastic flow to practice for fun.

## How SignalR works
Ok. We know when SignalR is a good choice. Let's look a bit deeper to understand how it works. It won't be a technical tutorial (I will write it in one of the post in the future), but I would like to show you the idea and mechanism hidden behind it.

The most important fact is that SignalR it is a really nice abstraction layer above the communication protocols. It can decide what connection type will be the best in current situation. It supports the following protocols:

### WebSockets
When WebSockets are used, then a communication channel between the client and the server is created. The channel is kept alive all the time and used to send messages in both ways. So you will be informed about new messages with very low latency, but also you may use the same channel to send messages to the server. 

It is the best option. When SignalR can use it, it will do it.

### Server-Sent Events
When WebSockets are not available, then SignalR have two options to keep the connection. One of then is Server-Sent Events. 

It is similar to WebSockets, but it is only one way communication, from the server to the client. With that approach the client can't send messages to the server using SSE. But of course SignalR allows the client to send messages to the server, but it will be done with a different protocol, behind the scene. 

### Long Polling
The last option is the most common, maybe it is not very efficient, but is works in every situation. In works in very easy way. The client sends a request with very long timeout to the server and the server doesn't respond until it has a message to send. When the server sends a message, then client send a new request with the same criteria.


## More nice features
There are more nice features of SignalR, that are worth to mention.

###  Automatic reconnection
When the connection is lost, SignalR tries to reconnect automatically. It is very useful, because it is very common that the connection is lost for a short time. Moreover you have the control over the reconnection process. You can decide how many times SignalR should try to reconnect and how long it should wait between attempts. 

If you wish you can also close and open the connection on demand. It is super important to remember it when you are creating SPA application and you change the active page. Then it could be a good idea to close the connection to save resources.

### Scalability
This feature is not a must for the beginning, but you have to know that there is a possibility to scale the SignalR. When you start a project it is easy, because one server is enought, but when project grows you have to think about it. SignalR allows to be ran in multiple instances and the state can be shared between them. If you want to know more about it, let me know in the comment.

## Summary
SignalR is really useful and powerful tool. It is worth to know it and use it in the right scenarios. I am planning to write more about it in the future. If you are interested in specific scenario, let me know in the comment.

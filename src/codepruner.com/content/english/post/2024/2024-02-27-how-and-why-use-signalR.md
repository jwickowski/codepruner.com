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

Everyone, who creates web applications, knows how most of the communication in the internet looks like. It is a request-response communication. The client sends a request to the server and the server responses. But what if you would like to reverse it and send information from server to client or event to many client? Let's look at this a bit closer. 

## Problem to solve
Before we start talking about solutions I would like to show you real scenarios we would like to solve with SignalR.

### Real time notifications
I put it in the 1st place, because it is the most useful scenario. Let assume you have a long running process in the backend and you don't want to force the client to wait until the whole process is done. You would like to return a transactionId and inform about progress asynchronously. Example:
1. Client uploads a file to the server
2. Server returns unique file identifier
3. Server starts processing the file
4. Server sends a notification to the client about the progress of the processing
5. Client can show the progress to the user
6. When the process is done, the server sends a notification to the client that the process is done.
7. Client can show the result to the user.

It is very useful, because you can process the file in the background and on a different server, if you need. So it is easier to scale.

### Real time dashboards
In financial applications or in admin panels it is very important to display live data. To display live data you have to send the information from server to client all the time. It is possible to do it with classic request-response communication, but it is not very efficient. It is better to use SignalR to send the data to the client when it is available.

and every one like when charts are moving :).

### Real time auctions
It is not very common scenario to implement, but really nice to show how SignalR can be used. In the auction system, you have to send information to all clients about the current price of the product. It is very important to send the information to all clients at the same time. It is not possible to do it with classic request-response communication, because the latest bid can appear the the last second.

### Chat 
It is the most popular SignalR example. It is a fantastic example, because it is really nice flow. You can see that message is send from one client to server and then it is sent to all clients. Moreover, there is important thing to mention, that it must be real time and you must be sure that message will be sent only to clients in specific chat room. But to be honest, it is not very  practical example. When did you implement chat last time? But to do it for fun. I agree :).

 But be honest how often do you need to create a chat? Not often, I guess.

## How signalR works


## How to configure it on backend


## How to configure it in frontend


## Summary
It is only the basic introduction to SignalR. I am planning to go deeper in the future. If you are interested in sepcific scenario, let me know in comment. 


---
title: "First thoughts about MartenDB"
author: "Jerzy Wickowski"
images:
  - "images/posts/2023/2023-09-29-first-thoughts-about-martenDB.png" 
date: 2023-10-02T06:40:58+01:00
draft: true
tags: ["sql", "postgres", "martemDB", "event sourcing"]
categories: ["EventSourcing"]
type: "regular"
---
Most of my development career I was working with SQLServer database from Microsoft. Mostly because my core is .NET so it was a natural choice, because all of the tools are supported very well. But in my new project I wanted to to EventSourcing. After investigation I have found MartenDB. There are some of my first thoughts about it.

### Types of data structures in MartenDB
The first thing which I have discovered is that there are three types of data structures in MartenDB. 
- tables - the standard one, which we know from SqlServer
- documents - there is a place to store JSON data
- events - a place for events as a place for events in EventSourcing

It is really nice approach, because we can have one place to store all of these things in one place. 

### Automatic structure creation
When we are using MartenDB we do not need to create data structure. It is done automatically. It is a huge advantage. We can focus on our business logic. You just create a simple class and MartenDB will create a table for you on the fly. It is great, because:

- You don't have to care about migrations. At least the the beginning of the project.
- You can just think about storing a specific type and you can do it.
- Of course can modify the structure if the default one is not enough for you. eg. you can add indexes.

I am no sure about the performance, but there are at least two pages in documentation how to deal with that. So I believe it is not a problem. 

### Events and projections
MartenDB is a tool for EventSourcing, so it is not a surprise that is has good support for events. When I was reading the documentation I was pleasantly surprised how easy it is to:
- store and load events
- create projections 

### Postgresql is used as a storage
It is a fantastic decision to use Postgresql as a backbone. I wrote in my [previous article about Postgresql in docker]({{< relref "./2023-09-22-how-to-run-postgresql-and-adminer-or-pgadmin-with-docker-compose.md">}}) that I have no experience with Postgresql, but it is a good choise bacause:
- It is a standard tool which is common used around the whole world.
- There are huge amount of tools which can help you with Postgresql.
- It is a very mature tool. It was created in 1996.




### Documentation is great
The 1st thing I did with MartemDb was reading the documentation. It is easy to read and understand. It is full of examples. Just after reading the documentation I feel I am abe to work with that. Of course there are some more advanced topics I skipped, like performance, but I know the information is there.

### Conclusion
I will start migration my project with home-made event store to MartenDB. I believe that huge amount of can will be thrown away and you can be sure I will sate it with you.

What is you feeling about MartenDB? Let me know in comments.

---
aliases:
  - post/2023/2023-10-03-five-things-you-must-know-about-martendb
author: Jerzy Wickowski
categories:
  - Craftsmanship
date: 2023-10-03T03:40:58.000Z
disqus_identifier: five-things-you-must-know-about-martendb
disqus_title: 5 things you have to know when you think about MartenDB
disqus_url: 'https://codepruner.com/five-things-you-must-know-about-martendb'
draft: false
images:
  - images/posts/2023/2023-10-03-five-things-you-must-know-about-MartenDB.png
tags:
  - sql
  - postgresql
  - postgres
  - martemDB
  - event sourcing
  - documents
  - json
title: 5 things you have to know when you think about MartenDB
type: trending
url: five-things-you-must-know-about-martendb
---

Most of my developer's career I work with SQLServer as database. Mostly because for .NET it's a natural choice and all of the tools are very well supported and integrated. But in my new project I want to use EventSourcing. After investigation I have found MartenDB. There are my first thoughts about it.

### Types of data structures in MartenDB
The first thing which I notices is that there are three types of data structures in MartenDB. 
- tables - the standard one, which I know from SqlServer
- documents - there is a place to store JSON data
- events - a place for events as a place for events in EventSourcing

It is really nice approach, because we can have one place to store all of these things in one place. 
I can imagine it will be much easier to prepare some read models or store data easly.

### Automatic structure creation
When you use MartenDB we do not need to create data structure. It is done automatically. It is a fantastic feature. You can focus on the business logic. You just create a simple class and MartenDB will create a table for you on the fly. There are some benefits of that:

- You don't have to care about migrations. At least the the beginning of the project.
- You can just think about storing a specific type and you can do it.
- Of course can modify the structure, if the default one is not enough for you. eg. you can add indexes.

I am no sure about the performance, but there are at least two pages in documentation how to deal with that. So I believe it is not a problem. 

### Events and projections
MartenDB is a tool for EventSourcing, so it is not a surprise that is has good support for events. When I was reading the documentation I was pleasantly surprised how easy it is to:
- store and load events
  - It is the base for EventSourcing. You need to store events. With MartenDB it is soo easy.
- create projections 
  - Creating and refreshing projections and read models is a crucial part of EventSourcing. MartenDB makes it simple and natural

### PostgreSQL used as a storage
It is a fantastic decision to use PostgreSQL as a backbone. I wrote in my [previous article about Postgres in docker]({{< relref "./2023-09-22-how-to-run-postgresql-and-adminer-or-pgadmin-with-docker-compose.md">}}) that I have no experience with PostgreSQL, but it is a good choice because:
- It is a standard tool which is common used around the whole world.
- There are huge amount of tools which can help you with Postgres.
- It is a very mature tool. It was created in 1996.

### Documentation is great
The 1st thing I did with MartemDB was reading the documentation. It is easy to read and understand. It is full of examples. Just after reading the documentation I feel I am abe to work with that. Of course there are some more advanced topics I skipped or missed, like performance, but I know the information is there.

### Conclusion
I will start migration my project with home-made event store to MartenDB. I believe that huge amount of can will be thrown away and you can be sure I will sate it with you.

What is you feeling about MartenDB? Let me know in comments.

---
aliases:
  - posts/ddd/why-do-i-hate-repositories
author: Jerzy Wickowski
categories:
  - ddd
date: 2021-05-23T20:16:58.000Z
disqus_identifier: why-do-i-hate-repositories
disqus_title: Why do I hate repositories?
disqus_url: 'https://codepruner.com/why-do-i-hate-repositories'
draft: false
images:
  - images/blog/red-sharp-pruner.jpg
tags:
  - ddd
  - repository
title: Why do I hate repositories?
type: trending
url: why-do-i-hate-repositories
---

I have started my developer adventure a bit more than 10 years ago and I hate repositories. ...or maybe I hated, becouse something changes, but we should start from the beginning.

# Why I hate repositories?
It is becouse of my experince. In almost all project I was, there were repositories, but there were something disturbing. I wasn't able to see the real sense of repository in that projects. Why? Probably becouse it was implemented without any sense. 

So how? In that projects repositories were used as additional layers above ORM (mosty EntityFramework) and repositories returned database rows hidden in ORM models. Why was it bad?
- There repositories was too big, becouse almost query to database was added here
- They were very repetitive. Every repository was almost the same. So always someone had idea to create a genetic BaseRepository
- These repositories was created one per table. So sometimes I need to have 4 repositories injected.
- It was difficoult to test, becouse mocking generic class that returns ORM models is not nice.
- In most cases it was useless, becouse it would be easier to use EntitiFramework directly.

I knew it was bad, so I felt that repository are another buzz work as DAL, Manager, Tools and Utils. I wanted to throw then away.

# How did I managed it?
Becouse I saw that approach was bad I was thinking and thinking... and thinking, and trying... and I discovered how to solve the problem. I did some assumptions:
- remove useless beings and layers
- keep think simple
- write code easy to test, mostly with TDD
- keep code closer to business values than technical stuff

So... I decided to not create repositories any more, but what instead? I prefare to have small readers/getters/providers instead of big repositories. Some examples:
- DeliveryDetailsReader
- UserAccountPasswordUpdater
- AvailableProductListReader

Thanks to that:
- Every of that class was simple.
- I can change methods of reading
  - To so simple insert or updates I can use EntityFramework
  - To read a gread about of data I can use Stored Procedure or raw SQL to have great performance
  - To read some data for view I can read it for different source like Documents Database
- It is very easy to mock that clases, becouse we know what they bechavior is
- We can test then using integration test to in simple contex


# But I read a book and something changed
I am currently reading Domain-Driven Design by Eric Evans and he wrote about repositories and there is a different context of that. 

The main difference is an understanding what Entity and Repository is and what are they responsibilities:
- Entity
  - It is not EntityFramework model that represents a row from database in objective world
  - It is a single item that is unique in a system. It reflects a current state of something.
- Repository
  - It is not another layer on EntityFramework that returns database rows. 
  - It is a somethings that will be able to save and recreate an Entity from a storage.

# Doeas it change anything?
Of course. 
- Now I will be able to distinguish if there is a Buzz word Repository or it was created for certain purpose.
- I will be thinking about that and maybe I will find a correct place where it could be used
- Now it sits in my mind and, probably, I will change my approach to repositories, but the real repositories. 

What about you? How do you think about repositories? Leave comment above.

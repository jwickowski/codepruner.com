---
title: "Why do I hate repositories?"
date: 2021-05-18T21:16:58+01:00
draft: true
tags: ["ddd", "repository"]
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




- I saw bad project
- I saw they are using as another layer on EntityFramerowk
- I prefare to have small readers/getters/providers instead of big repositories
- But I read DDD Evans and I can see that Repositories in that context make sense.


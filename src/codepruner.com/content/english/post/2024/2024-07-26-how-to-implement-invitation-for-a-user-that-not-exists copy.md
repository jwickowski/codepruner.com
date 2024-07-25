---
author: Jerzy Wickowski
categories:
  - ddd
date: 2024-07-26T05:40:58.000Z
draft: false
images:
  - images/posts/2024/2024-07-26-how-to-implement-invitation-for-a-user-that-not-exists.jpg
tags:
  - ddd
  - implementation
  - dotNet
  - ideas
title: how-to-implement-invitation-for-a-user-that-not-exists
type: trending

---
I am working on a project improve the cooperation between trainers, players and sport enthusiasts. On one of the step I ran into a problem of inviting not existing people, at least in the system. Let allow to me to walk you thought the problems and its solution.

## Describe the process
- You can invite through email or phone number
- How you can join to a club:
  - You can request, then someone shoudl ApproveTheRequest
  - You can be invited,
    - Then if user has and account already there is no problem, because we have his ID and we can save it
    - But when user has no account than we don't have userId(because it will be created when user register itself)

## Current solution
- just with AccountID
- describe the problem again

## Idea of the solution
- If I don't know UserID, because the user doesn;t exist (and I can't be sure that it will exist in the future) I need to:
    - save the information I have
    - find a possibility to update "the invitation" with userId when user registers itself 
      - It will be described in next POST
    - then you can procecess it as usual
## implementation of the solution

## maybe use CAP ?
// ale to już inny temat


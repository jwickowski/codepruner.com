---
title: "How to document Architectural Decision in projects? what-is-ADR-and-why-you-should-add-it-into-your-project"
date: 2021-10-18T08:40:58+01:00
draft: true
tags: ["ADR", "architecture", "cooperation"]
categories: ["Craftsmanship"]
---

ADR is Architecture Decision Record, but if we leave it like that it will not tell you anything. So we will deconstruct the topic into smaller pieces.  


# Architecture Decision
I like to describe it as: "Every decision in development team that has impact on state of the application and the way of working in concrete time". 

Ok, we have it, but i can give you some examples:
- Use TypeScript and React on frontend
- Create  backend in .NET
- Create the application for cloud. Try by agnostic if can, but currently we will use AWS
- Let's use Feature Toggles

There decisions are rather simple, but we can so obvious decision, because they can be done in the middle of the work. For example:
- All new files in frontend create in TS and if you need to touch a JS file, then migrate it to TS and add typings
  - The decision can be make when current app is node in JS and when we want start migrating to TS, but without stopping current development
  - The decision can be connected with different one about `ongoing refactoring`
- Stop using MomentJs, start using date-fns
  - The decision can be made when dev discover that momentJs is not longer supported

But decision can be not so technical. They can be about the way of working:
- Start using Trunk Based Development instead of GitFlow
- Every merge to master should be reviewed on CodeReview

I am sure that in every project you were, decision like that was done. By someone or by a team, but the question is if they were written and why we should do this.... and it is a path to next point...



# Why we need ADR
- What it should contain
- What format it should be

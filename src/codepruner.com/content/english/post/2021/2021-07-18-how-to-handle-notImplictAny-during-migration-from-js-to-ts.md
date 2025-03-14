---
aliases:
  - /posts/frontend/how-to-handle-notimplictany-during-migration-fom-js-to-ts/
  - posts/frontend/how-to-handle-notimplictany-during-migration-from-js-to-ts
author: Jerzy Wickowski
categories:
  - frontend
date: 2021-07-18T09:40:58.000Z
disqus_identifier: how-to-handle-notimplictany-during-migration-from-js-to-ts
disqus_title: How  to handle notImplictAny during migration from js to ts without time wasting
disqus_url: 'https://codepruner.com/how-to-handle-notimplictany-during-migration-from-js-to-ts'
draft: false
images:
  - images/posts/2021/2021-07-18-how-to-handle-notImplictAny-during-migration-from-js-to-ts.png
tags:
  - javascript
  - typescript
  - noImplicitAny
  - any
  - fakeAny
  - ongoing refactor
title: How  to handle notImplictAny during migration from js to ts without time wasting
type: regular
url: how-to-handle-notimplictany-during-migration-from-js-to-ts
---

When you are trying to migrate from JavaScript to TypeScript you will have an error `noImplicitAny` when you variable or parameter will not have assigned type. 

# There are to two bad solutions
## Quick and dirty

To avoid that options in quick and dirty way you can:
- Disable `noImplicitAny` in `tsconfig.json`, but I don't recommend it to you because
  - You are migrating to typescript to have types. Am I right?
  - You will have to do this in the future.
- Use `any` on any place where type is required, but:
  - There is no value for typescript if you use `any` everywhere
  - You will be not able to distinguish:
    - where `any` is used consciously 
    - where `any` is used, just to build the application

## Slow and clean

- Create a type for every variable and parameter, but:
  - It will take you a lot of time
  - It is not required if you don't have cooperate with that code
  - You will be tired, because it is very boring job

# The best solution to avoid  `noImplicitAny` error
1. Create a file `fakeAny.ts` with content:
    ```
    export type fakeAny = any
    ```
2. In any place where you have  `noImplicitAny` use `fakeAny` instead od `any`
3. Then your application will be build and you will have a huge amount of benefits

# Benefits of using `fakeĄny` instead of `any`
- You can continue your work almost immediately.  
- You know where you want to use `any` and where it is only temporary placeholder for TS compiler
- You can find all `fakeAny` occurrences easily. So you will know what parts of the code are not migrated to TypeScript completely.
  - Or you can remove `fakeAny.ts` to check how many errors you have. It is useful when you are on the end of migrating.


# A small request to you :)
If you think you will use it or you have different approach for the same problem. Let me know and leave a comment. 

Thank you

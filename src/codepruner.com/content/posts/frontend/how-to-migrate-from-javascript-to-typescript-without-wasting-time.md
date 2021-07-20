---
title: "How to migrate from javascript to typescript without wasting time"
date: 2021-07-18T10:40:58+01:00
draft: false
tags: ["javascript", "typescript", "legacy", "clean code", "ongoing refactor"]
---

I prefer typescript to javascript. There are multiple of reasons for TS, but sometimes I join to projects made in JavaScript. 

# Available paths
Then there are multiple options and path to choice. We have to consider some options. All of them has some benefits and costs:
1. Leave Javascript as is and develop the application as is.
  * \+ It is good to have one language instead of two
  * \+ You can start coding faster because you don't have to migrate
  * \+ Javascript is more flexible
  * \+ All libraries you can use in JS without any tricks
  * \+ Building is faster
  * \- You have no typescript
2. Migrate all the code to Typescript
  * \+ It is goo to have one language than two
  * \+ You get all typescript's benefit from the beginning
  * \- It can take you a lot of time to migrate everything
3. Migrate to Typescript step by step, file by file
  * \+ You can start coding new features in TS immediately
  * \+ You will not spend huge time on migration at the beginning
  * \- You have 2 languages in your project
  * \- You have to think about the migration every day
  * \- You current tasks will take you a bit more time, because of migrations
  * \+/\- You have to learn how to improve code with small steps

As you know I am a great fan of ongoing refactoring, so 3rd approach is natural for me. Now there is only one question:  

# How to migrate from JavaScript to TypeScript in small steps?

1. Add tsconfig.json file to your project.
   * Make sure that `allowJs` is set to `true`
   * You can get a simple sample template from <https://www.typescriptlang.org/docs/handbook/migrating-from-javascript.html>
2. Build the application and fix all errors if there are any
3. Don't change extensions from `.js` to `.ts` you do don't have to
4. Create a new file with `.ts` extension. 
   * and use it in current application to be sure that everything is working correctly
5. If you have to touch a `.js` file, to modify or to import from `.ts` file. Then
   * Change file extension to `.ts`
   * Try to build the application. It probably fails.
   * Everywhere where are no types use `any`
     * or I recommend to use `fakeAny`. You can read more about that in post [How to handle `noImplicitAny` during migration fom js to ts]({{< relref "./how-to-handle-notImplictAny-during-migration-from-js-to-ts.md">}})
6. If you need to use some types from migrated file or when you need to pass some strongly types parameters. Then add or import concrete types and use them instead of `any` or `fakeAny`.

# Continue migrating in next months
As you can see with that approach you will have all benefits from TypeScript without a huge amount of wasted time. But you mustn't forget to continue the migration in next month. To have more and more positive aspects of TypeScript.


## What do you think about that approach? 
Leave the comment to let me know. I will be grateful. Thank you.  
  
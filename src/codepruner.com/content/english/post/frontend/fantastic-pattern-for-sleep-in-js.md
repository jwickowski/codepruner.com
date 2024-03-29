---
aliases:
  - posts/frontend/fantastic-pattern-for-sleep-in-js
author: Jerzy Wickowski
categories:
  - frontend
date: 2021-07-27T09:40:58.000Z
disqus_identifier: fantastic-pattern-for-sleep-in-js
disqus_title: Fantastic pattern to sleep in modern javascript
disqus_url: 'https://codepruner.com/fantastic-pattern-for-sleep-in-js'
draft: true
images:
  - images/blog/red-sharp-pruner.jpg
tags:
  - javascript
  - typescript
  - sleep
  - thread
  - promise
  - async/await
title: Fantastic pattern to sleep in modern javascript
type: regular
url: fantastic-pattern-for-sleep-in-js
---

Sometimes you need to wait for something. In most cases there is something bad in your code or with your infrastructure, but sometimes it is required. When?
For example:
 - if you know that an external service should return you value, but it is still in progress, so you want to wait a bit a do another request in 2 seconds
 - if you want to implement retry pattern, but you don't want to attack the service, but you want to wait some time between calls

# But you are working in JavaScript
... and there is no something like `Thread.Sleep()`. 
... and there is no something like `Thread`
... and I can tell you something in secret: JavaScript is single-thread language

so... making a sleep is not so trivial at 1st look, but we will try to handle it.

## Bad solution found in internet
There is one solution that I found in internet. I added it here to show you what you should avoid and why:
```
export const badSleepApproach = (milliseconds: number): void => {
    const destinationMilliseconds = Date.now() + milliseconds;
    while(Date.now() < destinationMilliseconds){ }
}
```
Will it work? Of course!
Will it work well? NO. Why?

Because in that solution we will block JavaScript for whole sleeping time. Event won't be handled. UI can stop. Because all of the power will be consumed on handling empty loop. It has no sense.

## Good solution

export const promiseSleepFor = (millSeconds: number): Promise<void> => {
    return new Promise<void>((resolve, reject) => {
        setTimeout(() => {
            resolve();
        }, millSeconds);
    });
}

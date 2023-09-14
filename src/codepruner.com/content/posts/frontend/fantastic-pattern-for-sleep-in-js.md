---
title: "Fantastic pattern to sleep in modern javascript"
author: "Jerzy Wickowski"
images:
  - "images/blog/red-sharp-pruner.jpg"
url: "posts/frontend/fantastic-pattern-for-sleep-in-js"
date: 2021-07-27T10:40:58+01:00
draft: true
tags: ["javascript", "typescript", "sleep", "thread", "promise", "async/await"]
categories: ["frontend"]
type: "regular"
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

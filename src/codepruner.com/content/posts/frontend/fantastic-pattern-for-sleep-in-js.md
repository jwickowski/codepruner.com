---
title: "Fantastic pattern to sleep in modern javascript"
date: 2021-07-27T10:40:58+01:00
draft: true
tags: ["javascript", "typescript", "sleep", "thread", "promise", "async/await"]
---



export const promiseSleepFor = (millSeconds: number): Promise<void> => {
    return new Promise<void>((resolve, reject) => {
        setTimeout(() => {
            resolve();
        }, millSeconds);
    });
}
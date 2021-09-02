---
title: "Nameof in typescript"
date: 2021-09-02T10:40:58+01:00
draft: true
tags: ["typescript", "nameof", "strong types"]
---

I miss some .NET features when I work in typescript. One of that is nameof keyword in C#.

We can do something similar in TS

```
export const propertyName = <T>(propertyName: keyof T) => propertyName;
```

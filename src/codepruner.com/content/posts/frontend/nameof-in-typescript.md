---
title: "Nameof in typescript"
date: 2021-09-02T10:40:58+01:00
draft: true
tags: ["typescript", "nameof", "strong types"]
---

I miss some .NET features when I work in typescript. One of that is `nameof` keyword in C#.

We can do something similar in TS
## TL;TR;

Just do it.

```
export const propertyName = <T>(propertyName: keyof T) => propertyName;
```

and use it:
```
 propertyName<UserProfile>('UserEmail'),
```

## Longer story
We have at least two options to handle that approach in typescript. Both of them are connected with `keyof` world in Typescript. More about this you can find in [Typescript documentation](https://www.typescriptlang.org/docs/handbook/2/keyof-types.html). 

Documentation, documentation, theory. But how to do it in practice? 

Let's start the trip.

### The ugly beginning
Let's with an example in Typescript without any checking. 

```
const dataTableColumnsDefinition = [{
    field: 'Id',
    label: 'Id',
    

}]
```

We will start 
Typescript doesn't have the 
Write how keyof is working with some examples

Write multiple version of it
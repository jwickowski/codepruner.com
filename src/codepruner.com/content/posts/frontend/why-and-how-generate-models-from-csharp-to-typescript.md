---
title: "why-and-how-generate-models-from-csharp-to-typescript"
date: 2021-08-10T10:40:58+01:00
draft: true
tags: ["javascript", "typescript", "C#", "clean code", "code generator"]
---

I don't understand why developers doesn't automate their job. They can save a huge amount of time with very low effort. How?  For example, by generating models from backend to frontend. We will talk about it today.

# How the generation can look like?
The main idea is to keep backend and frontend models synchronized automatically or almost automatically. In most cases frontend ask backend about data, so the main source of truth should be at backend site. I see it in that way. When model is changed on backend then frontend models should be updated automatically or by executing simple command, like running a console app.

# Why generating is good
Before we go to details we should talk why it is soo good:
- Focus on important things
   - not on coping names from one place to another
   - We are developers. We should solve problems, not copy labels. Especially when we can avoid stupid work easily
- Less work in the future
   - To not do the same work twice, so you will save your time
- No risk of typo
   - We are humans. We make errors, like typos. We can reduce them to minimal by generating code.
- Errors on build
   - When you map  manually then you can be not synchronized and you can miss something. 
   - But when you do it automatically, then TypeScript compiler fail on build and it always better to know about an error earlier
- Better performance
   - Sometimes you need to check some data, some types, some structures, because you are not sure about it. When you generate it, they are sync. So some checking can be ignored, because compiler does it.


# Why people don't do this often?
If they are some many benefits, why this approach is so rare? 

- I have no idea :)
- People don't know how to achieve it
- People believe that front/and and backend are independent (then I suggest to use BFF (Backend For Frontend) pattern)

# How to do this
- Write it by your own, but it will be discovering a new circle
- Use complete tool
- Hybrid, write your code, but with usage of some libraries

# A real example
Here you can see a sample implementation. My approach is to create a simple version at the beginning and improve it when it is required. So that solution is not perfect, but it is simple and it will give you benefits form the 1st day of usage.

I will split into three fragments:
1. How to get metadata
2. How to generate code
3. How to run it

So let's get started
## Getting metadata

At the beginning we need a model to keep Fields in simple flat format.
{{< highlight  csharp "linenos=false,linenostart=1" >}}
{{<github file="src/examples/CodePruner.Examples/CodePruner.Examples.TypeScriptCodeGenerators/BackendField.cs" >}}
{{< / highlight >}} 

When we have model we can get it from metadata. We will use reflection for this:

{{< highlight  csharp "linenos=false,linenostart=1" >}}
{{<github file="src/examples/CodePruner.Examples/CodePruner.Examples.TypeScriptCodeGenerators/BackendFieldGetter.cs" >}}
{{< / highlight >}} 

Ok. It is done. Not we need to consume that data and create file content

{{< highlight  csharp "linenos=false,linenostart=1" >}}
{{<github file="src/examples/CodePruner.Examples/CodePruner.Examples.TypeScriptCodeGenerators/TypeScriptModelGenerator.cs" >}}
{{< / highlight >}} 

And join it as a one class to simplify the usage:


{{< highlight  csharp "linenos=false,linenostart=1" >}}
{{<github file="src/examples/CodePruner.Examples/CodePruner.Examples.TypeScriptCodeGenerators/TypeScriptContentGenerator.cs" >}}
{{< / highlight >}} 

OK. Before we start it we should write a unit test to be sure that the code is working properly"

{{< highlight  csharp "linenos=false,linenostart=1" >}}
{{<github file="src/examples/CodePruner.Examples/CodePruner.Examples.TypeScriptCodeGenerators.UnitTests/TypeScriptModelGeneratorTests.cs" >}}
{{< / highlight >}} 

And finally we can write a Runner, I mean a console app that will combine it together and we will be able to regenerate models as often as we wish:

{{< highlight  csharp "linenos=false,linenostart=1" >}}
{{<github file="src/examples/CodePruner.Examples/CodePruner.Examples.TypeScriptCodeGenerators.Runner/Program.cs" >}}
{{< / highlight >}} 


Of course there are multiple things to do with that generator like:
- Generating enums
- Importing different types
- Resolving paths
- Generating arrays

But there is an idea, but if you think it is useful you have 2 options to say me this:
1. Write a comment below. We can discuss about.
2. Create a PullRequest to that repo to improve that place or that code. Maybe we create a library for this?

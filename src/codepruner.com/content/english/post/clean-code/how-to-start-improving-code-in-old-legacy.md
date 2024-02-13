---
author: Jerzy Wickowski
categories:
  - clean-code
date: 2021-03-29T20:40:58.000Z
disqus_identifier: posts/clean-code/how-to-start-improving-code-in-old-legacy
disqus_title: How to start improving code in old lagacy project
disqus_url: >-
  https://codepruner.com/posts/clean-code/how-to-start-improving-code-in-old-legacy
draft: false
images:
  - images/blog/red-sharp-pruner.jpg
tags:
  - legacy
  - clean code
  - ideas
  - pain points
title: How to start improving code in old lagacy project
type: regular
url: posts/clean-code/how-to-start-improving-code-in-old-legacy
---

Everyone of us is joining into old ugly legacy systems. When we see it we want to fix the whole application, but it isn't so easy. 
You have to ask yourself a question

# What should I prune at the beginning?
NOTHING! It is what I learn. You must not fix nothing, at the beginning. 

You have to do 3 things:
  1. Understand how the business works
     * Then you will be able to split code into modules
     * Then you will know what is it most important and the most crucial part of the code
  2. Get information about current solution and infrastacture 
     * Then you will know where changes should be dane
     * Then you will know where are dengrous dependencies
  3. Preapre what should be fixed
     * Then you will know what is the most important to prune
     * Then you will have date to convice business why that change is inportant
     * Then you will not do blind fix, but you will aim correctly

Today we will talk a bit about the last point.

# How prepare infomrmations about prune candidates?
I would suggest to not make big refactors in first months. You should rather focus on fixing bugs and adding small features. During that task you will get information for points 1 and 2. 

But you should create a place where you and the team will write about pain items in code. Evertime when thay are irritated. Every time when you burn then time becouse of currect solution. What should be expect from that place:
* It should be easy to update
* Is should be close to the source code
* It is a draft and it can be removed in the future

So.. I suggest tocreate a file in repo. You can name something like `PainPoints.md` and write into it. What should be inside:
* A description what is bad or irritating
* A short information why it is bad
* An information how much you burn because of it. 
  * It can be summed, but it would be better to add new values
  * You will see how the value is chaning 
  * It is very IMOPRANT as an future input for discuss
* Date to know how old the issue is
* The location of that issue. eg. File, Project, Module
* How it is connected with other components

# An examlpe of that file

{{< highlight markdown "linenos=false,linenostart=1" >}}

--- 
title: EAV Structure
author: "Jerzy Wickowski"
images:
  - "images/blog/red-sharp-pruner.jpg"
url: "posts/clean-code/how-to-start-improving-code-in-old-legacy"
date: 2020.12.15
description: Entity Attribute Value Structure in database make my queries complex. 
So my work is slower and slower becaouse of it. 
Moreover I am not able to understand from db structure. 
I know that solution is elastic, but not in current implementation.

date: 2020.12.15
burn: 4h - to read current product state

date: 2021.01.03
burn: 3h - to update current client info
description: It is complicated becaouse we have to write low level sqls to manage that data and tests are difficoult
---

--- 
title: code scattering
author: "Jerzy Wickowski"
images:
  - "images/blog/red-sharp-pruner.jpg"
url: "posts/clean-code/how-to-start-improving-code-in-old-legacy"
date: 2020.11.13
description: A code for connected logic is in different places. So I had to search in multiple folders and project to do a small change.

date: 2020.11.12
burn: 2h
---
{{< / highlight >}}

# Is it working?
I will check it. It is my plan for starting new project. I will let you know about results.

Tell me what is your approach!

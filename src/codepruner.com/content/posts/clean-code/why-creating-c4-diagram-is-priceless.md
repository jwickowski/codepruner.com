---
title: "Why creating c4 diagram is priceless"
date: 2021-04-20T08:40:58+01:00
draft: false
tags: ["legacy", "clean code", "idea", "c4"]
---

I am currently joining into new project, into new company. There is knowleadgle I don't know, but I have to be familiar with that. I know and everyone knows, it is not a perfect solution, but before I suggest any changes I need to undestand what is what.

So I am going to [not change anything at the beginning](how-to-start-improving-code-in-old-legacy), analyze, understand and start suggesting later. One of fantastic tool to explore systems is creating diagrams.

# What tool should I use? 
We decided to use C4 diagrams with [PlanUML](https://github.com/plantuml-stdlib/C4-PlantUML#getting-started). How to configure, I will describe in different post, but it allows me to find what connections are between systems to ask more question to understant it deeper.

# How I create it?
It is repeitable process for every project in the, but we have to start somewhere. At the begining I am going to be focused on one concrete system, so I will stat in that place. The system, like any other system, has dependecies to multiple differens systems. My plan it is find all of that dependecies and add them into the diagaram. So I am checking every project, one by one and I am adding new componenents and relations between them. So I can see:
- What components are there
- What are connection between them
- Which database is used in which system
- Where are circular dependencies
- What external system we use
- What is buisiness a flow in technical structure

# What are benefits
- I know what dependencies are here.
- I have an image about environemtnt complexity.
- I can prepare some question about external system to understend the context deeper.
- On bug fixing it will be helpfull to localize what was the source of problem or what else can be broken.
- On planning we will need what should be maintained.
- Understating one system is not enought you have to know what is outside to make good decistion. 

# Is the diagram ready?
Yes, and no. 
Yes, because it helps you with exploring and developing the system.
No, becaouse it will never be ready. You have to:
- add new knowleadge you get
- add new systems you are working 
- refactor it to make it readible and extendible when it become more and more complex

# Do you use diagrams?
or not? 
Leave the comment why? Thank you.

---
title: "Why creating c4 diagram is priceleest"
date: 2021-04-15T21:40:58+01:00
draft: true
tags: ["legacy", "clean code", "idea", "c4"]
---

I am currently joining into new project, into new company. There is knowleadgle I don't know, but I have to be familiar with that. I know and everyone know it is not perfect, but before I suggest some changes I have to undestand what is what. 

So I am going [not change anything at the beginning](how-to-start-improving-code-in-old-legacy), analyze and suggest later. One of fantastic tool to explore systems is creating diagrams.

# What tool use? 
We decided to use C4 diagrams with [PlanUML](https://github.com/plantuml-stdlib/C4-PlantUML#getting-started). How to configure, I will describe later, but it allows me to find what connections are between systems.

# How I create it?
Now I will be focused on one concrete system, but the system has dependecies to multiple differens systems. So I am checking every project, one by one and I am adding new componenents and relations between them into diagram. So I can see:
- what components are there
- what are connection between them
- which database is used in which system
- where are circular dependencies
- what external exists

# What are benefits
- I have question what system should i understand
- On bug fixing it will be helpfull to localize what was the source of problem or what else can be broken
- On planning we will need what should be maintained
- 
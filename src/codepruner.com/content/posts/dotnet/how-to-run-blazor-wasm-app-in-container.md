---
title: "How to run blazor app in docker container"
date: 2023-09-01T14:42:00+01:00
draft: true
tags: ["dotnet", "docker", "container", "k8s", "kubernetes", "devops"]
---

In one of my project I decided to use Blazor WebAssembly and I wanted to run it in docker, but it did't work woth default confgiuration. So in this blog post I wil describe you what is wrong with default configurationa and what you need to change to be able to run Blazor WebAssembly app with docker.

# What is wrong with default config
When I created new Blazor WebAssembly project, I have selected I want to run it with docker. It generated me a file. Here you can see it:
{{<code language="docker"  file="static\examples\CodePruner.Examples\CodePruner.Examples.BlazorWasm.Containerization\default.Dockerfile" >}}
- crated with VS/rider
- maybe screenshot
- show default dockerfile
- describe what is wrong
  - that it is a static page....

So we know it is not working because it doesn't need dotnet to run. So we can make it more generic to
# Deployng static web app as container

- use nxging in dockerfile
- it will not work, because we need to config it
- config it (add filed to project and package)
- ... be happy it is working


# to do in next posts
  - configuration with docker compose
  - confgiration with k8s

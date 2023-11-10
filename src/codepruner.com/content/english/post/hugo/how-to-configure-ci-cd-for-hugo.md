---
author: Jerzy Wickowski
categories:
  - blog
date: 2021-04-07T19:40:58.000Z
disqus_identifier: 'https://codepruner.com/posts/hugo/how-to-configure-ci-cd-for-hugo'
disqus_title: How to configure CI/CD for hugo?
disqus_url: 'https://codepruner.com/posts/hugo/how-to-configure-ci-cd-for-hugo'
draft: false
images:
  - images/blog/red-sharp-pruner.jpg
tags:
  - hugo
  - docker
  - devops
title: How to configure CI/CD for hugo?
type: regular
url: posts/hugo/how-to-configure-ci-cd-for-hugo
---

# How the proces will look like
I decided to start blogging using Static Site Generator, like Hugo to create content easly and to deploy the blog easly.

What steps I need?
1. Build it
1. Deploy it

# How to build it?
I have 3 ideas in my mind how to build it

## Install hugo and build
It is the first option, but become more complicated when we are thinking about CI/CD. Becouse to reach it, we have to install hugo on CI agent. So it will take more time and it is not very funny. 

But Hugo can be installed using chocaletly, so... maybe, but there are different options

## Use CI/CD tasks to automate it
It is very good option, because it is very easy to achive. We can use an 
* Action in Github Actons
* Task in Azure DevOps Pipelines
* Any other ready task to run it

Minus of that option is: 
* You cannot reproduce these steps locally 
* Moving to different CI tools isn't simple

... and for normal project can be a bit problemiatic, but for building hugo, I believe it will be ok.

## Use docker to build it everywhere
There is another option to configure automatic build using docker. Then you will be able to run it everywhere. Locally and on CI Agent. Moreover migrating between CI tools will be easy, but... 

Do I need that? I don't thing so.

# What is my decision?
I am going to go with the 2nd option. I will use ready bricks provided by CI tool. Why?

* To publish the blog, not hide it.
* To keep it simple .
* To not spend too much time on configuring.
* Becouse runnig Hugo locally is simple.
* Becouse it is not complicated pipeline, so configurin it again, from scratch will be easy, if needed

What is your approach? Tell me in comments. Thank you.


---
author: Jerzy Wickowski
categories:
  - blog
date: 2021-04-01T05:40:58.000Z
disqus_identifier: 'https://codepruner.com/posts/hugo/how-to-deploy-hugo-website'
disqus_title: How to deploy Hugo website?
disqus_url: 'https://codepruner.com/posts/hugo/how-to-deploy-hugo-website'
draft: false
images:
  - images/blog/red-sharp-pruner.jpg
tags:
  - hugo
  - blog
  - ftp
  - azure
title: How to deploy Hugo website?
type: regular
url: posts/hugo/how-to-deploy-hugo-website
---

# I have some posts ready. What's next?
I am on that situation. I have prepared 4 posts. It is 5th. So I am going to publish it somewhere.  
I did some preparation and I have same assumptions:

* I want to publish my website on concrete domain:
  * [CodePruner.com](https://CodePruner.com) - the main version of the blog
  * [test.CodePruner.com](https://test.CodePruner.com) - the test version where I will test some features or layout
* I want to deploy the code automatically
  * To simplify the process of publishing
  * I am going to use: 
    * AzureDevOps - I like it and I know it
    * GitHub Actions - I want to use it, but I will check if I will be able to manage two staghes
    * Maybe I will write some script to be able to change DevOps tool in the future
* I don't want to generate big costs
* Hugo generate statis pages, so I don't need very complicated services to host it. 
* I'm thinking about using
  * Azure Blob Storage - As global, not expensive and elastic. My friend [Radek Maziarka](https://radekmaziarka.pl/) is using
  * [Zenbox.pl](https:/zenbox.pl) - My current webpage provider. To not generate additionals costs and just use FTP to publish.
  * GitHub Pages - as a nice free alternative 

# How will it look like?
After commit 

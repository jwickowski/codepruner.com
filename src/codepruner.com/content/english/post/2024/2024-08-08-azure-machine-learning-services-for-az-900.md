---
author: Jerzy Wickowski
categories:
  - dotnet
date: 2024-08-08T05:40:58.000Z
disqus_identifier: azure-machine-learning-services-for-az-900
disqus_title: Azure Machine Learning services for AZ-900
disqus_url: 'https://codepruner.com/azure-machine-learning-services-for-az-900'
draft: false
images:
  - images/posts/2024/2024-08-08-azure-machine-learning-services-for-az-900.jpg
tags:
  - AI
  - azure
  - az900
  - exam
  - machine learning
title: Azure Machine Learning services for AZ-900
type: regular
url: azure-machine-learning-services-for-az-900
---
Today we will not go very deep in that topic. It is rather to show you a basic services for AI/ML. Currently Artificial Intelligence is a buzzword and when we think about it LLMs and GPTs come to out minds, but threre are more technical services, added to Azure long time ago. So I am going to focus on these older services today, but if you would like to read about newer ones. Let me know in the comments sections.

## What is what? Life is life.

Before we go further, we have to know answer for questions like: "What is AI?" or "What is Machine Learning?". You can find the answers in article [`How to distinguish types of AI`]({{< relref "../2023/2023-10-04-how-to-distinguish-types-of-AI.md">}}).

But before we go to specific details there is a workflow how we work with ML models:
- Train - You have provide data and train the model
- Package - To be able to work with that
- Validate - You need to check if results are satisfying
- Deploy(eg. as web service) - When you have the model then you can make it publish for internal or external usage
- Monitor - When you you the model you can improve it

## Machine Learning
There is one bit Azure Machine Learning service. It is a PaaS platform that include everything you need to build and use your own model. There are for example:
- Machine Learning Workspace - It it a top level ML resource in Azure
  - It is the main resource that manage the whole process
- Notebooks - It allow us to run Python or R script, but it contains also some example scripts
- Automated ML - A tool that can build ML model automatically
- Designer - A tool for creating ML models with user interface
- Datastore - To connect you other storages to ML tools
- Compute - to run model building

## Summary 
I know it was shorter material than usual, but I believe it will be expanded in the future.
If you have any question, let me know in comments section below.
If you want to be informed about new posts, subscribe.
---
author: Jerzy Wickowski
categories:
  - dotnet
date: 2024-05-13T06:40:58.000Z
disqus_identifier: general-information-about-cloud-computing-az-900
disqus_title: General information about cloud computing - AZ-900
disqus_url: 'https://codepruner.com/general-information-about-cloud-computing-az-900'
draft: false
images:
  - images/posts/2024/2024-05-13-general-information-about-cloud-computing-az-900.jpg
tags:
  - az-900
  - dotnet
  - Azure
  - certificate
  - course
  - exam
title: General information about cloud computing - AZ-900
type: trending
url: general-information-about-cloud-computing-az-900
---
During preparation for passing the AZ-900 exam you have to get knowledge about Azure, but also you need to have a good understanding of a general cloud computing concepts. In the [previous post]({{< relref "./2024-05-10-preparing-to-Microsoft-Azure-Fundamentals-Certificate-AZ-900.md" >}}) I described my plan and listed the resources I used to prepare for the exam. Below you can find general information about cloud computing. 

## What Cloud Computing is?
I think it is the most important question when you start thinking about the cloud. There are some characteristics what Cloud is. Moreover, I think, most of us just feel it, but let's try to describe it. In a shortcut I understand it as:
> Internet service that allows you to use resources with a possibility to create and delete then when you need it. Moreover it should have a possibility to scale automatically.

There are 4 types of services that should be provided by a cloud provider:
- storage - a place to store your data
- compute power - a place to run your code
- networking - a way to connect your resource
- analytics - a way to analyze how your resources are used

Do you agree with that? What is you short description of Cloud Computing? Let me know in the comments.

## Characteristics of Cloud Computing
We know the main idea, let's go a bit deeper and list the main characteristics of Cloud Computing. Here are the most important ones:

### Agility
It is the general, the most important idea behind the cloud computing. It is the ability to quickly deploy the resources, but also to remove reduce them if they are not required. Only that, but the Agility opens the possibility for the rest of the characteristics.

### Scalability 
It is a possibility to increase or decrease the resources. We can do it in two directions:
- vertical - increase or decrease the *power* of the machine 
  - More CPU, more memory, etc.
  - It can be done in two directions
    - UP (stronger machine)
    - DOWN (weaker machine)
- horizontal - increase or decrease the *number* of machines
  - More machines
  - It can also be scaled in two directions
    - OUT(more machines) 
    - IN(less machines)

### Elasticity
when you connect two previous points you get elasticity. It is a possibility to automatically increase or decrease the resources based on the amount of users. So when system discover that more/less power is required then it is able to allocate/deallocate resources.

### Availability
This group is related to metrics that describe how much time the system is available.
- uptime - the time when the system is available
- downtime - the time when the system is not available
- availability - uptime/(uptime + downtime)
  - eg. 99% means 7.2 hours of downtime per month
  - eg. 99.99% means 4.38 minutes of downtime per month

  So cloud solutions should have a high availability. This value is strongly connected with:
  - fault tolerance - the ability to recover from failures when the server shut down immediately
  - disaster recovery - the ability to recover after a disaster
    - eg. when one data center is down, the system should be able to recover in another data center

## Different approaches
There are many ways to classify the cloud approach. Here are some of them:

### Public vs private vs hybrid
There are three main approaches to cloud computing. It depends who is the owner of the resources.
- Private - available for one organization
  - It is an on-premises cloud solution
  - It means that a company has its own servers
- Public - available for everyone. 
  - It is the most popular approach. When we talk about cloud it is the default approach.
  - The company who provides the cloud services is called cloud provider 
- Hybrid - mix of public and private
  - It is a mix of the previous two approaches.
  - Sometimes some data cannot leave a company or a country. In this case, we can use a hybrid approach.
  
Every approach has its own advantages and disadvantages:
- advantage of public cloud
  - No CapEx* (it is described below)
  - High availability & agility
  - Pay as you go
- advantage of private cloud
  - Security
  - Ownership
  - Compliance

### Cloud services model
There are multiple level of services that can be provided by a cloud provider. There is a choice between: control and abstraction. Here are the most popular models:

- On-Premises
  - You do everything by yourself.
  - You have buy servers, install software, etc.
- IaaS - Infrastructure as a Service
 - You don't think about servers, just use VMs
 - You have to think about the operation system, middleware, runtime
- PaaS - Platform as a Service
  - You don't think about anything except your application 
  - It is the most popular service model when you think about public cloud
- SaaS - Software like a Service
  - It is when you just pay for a software like:
    - Gmail, DropBox, Office365, etc.

## Glossary
There are also important words that you should know when you talk about cloud computing. Here are some of them:

- Capital Expenditure (CapEx) - is when you buy anything to improve the quality of your services. It is one-time payment.
  - Buy your own servers
  - Big initial investment
- Operational Expenditure (OpEx) - is when you pay for the service you use. It is a recurring payment.
  - Rent the servers
  - Little or no initial investment  

## Summary
Thank you for being with me. If you have any question, let me know bellow in comments section. Stay tuned!


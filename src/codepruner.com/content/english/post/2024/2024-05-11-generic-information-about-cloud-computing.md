---
author: Jerzy Wickowski
categories:
  - dotnet
date: 2024-05-11T06:40:58.000Z
disqus_identifier: generic-information-about-cloud-computing
disqus_title: generic-information-about-cloud-computing
disqus_url: 'https://codepruner.com/generic-information-about-cloud-computing'
draft: true
images:
  - images/posts/2024/2024-05-11-generic-information-about-cloud-computing.jpg
tags:
  - dotnet
  - Azure
  - certificate
  - course
  - exam
title: generic-information-about-cloud-computing
type: regular
url: generic-information-about-cloud-computing
---
## General information
Cloud computing can be split into 4 categories:
- storage
- compute power
- networking
- analytics

key characteristics of cloud computing:
- scalability - to increase or decrease the resources
  - vertical - increase or decrease the power of the machine
   - UP(stronger) or DOWN(weaker)
  - horizontal - increase or decrease the number of machines
    - OUT(more) or IN(less)
- elasticity - to automatically increase or decrease the resources, based on amout of users
  - automatic scaling
- agility - to quickly deploy the resources. Or create new resources easy and quickly
  - allocate or deallocate resources quickly
- fault tolerance - to have resources available all the time
  - the ability to recover from failures when wf. server shut down immediately
- disaster recovery - the ability to recover after disaster
  - eg. have a backup of the data
  - eg. you can deploy the resources in multiple regions
- high availability - uptime/(uptime + downtime)
  - availability is a measure of system uptime for users/services
  - eg. 99% means 7.2 hours of downtime per month
  - eg. 99.99% means 4.38 minutes of downtime per month


A - up
V - down
< - in
> - out

## CapEx vs OpEx 
- Capital Expenditure (CapEx) - is when you buy anything to improve the quality of your services. It is one-time payment.
  - Buy your own servers
  - Big initial investment
- Operational Expenditure (OpEx) - is when you pay for the service you use. It is a recurring payment.
  - rent the servers
  - little or no initial investment


## Consumption-based model
- Compute-  pay for the time you use the machine
- Storage - pay for the amount of data you store
- Networking - pay for the amount of data you transfer

just pay-as-you-go 


## Cloud services model
-On-Premises - you do everything by yourself
- IaaS - you don't think about servers, just use VMs
  - Storage/Networking/Servers/Virtualization
- PaaS - like azure
  - Operation Systel, MIddleware, Runtime
- SaaS - just services/applications
  - Gmail, DropBox, etc

## Cloud deployment model
- Public - available for everyone
  - all resources are in Clound Sevice Provider
  - advantage 
    - No CapEx
    - High availablity & agility
    - Pay as you go
  - disadvantage
    - Security
    - Ownership
    - Compliance
    
- Private - available for one organization
  - All resources are in the organization
- Hybrid - mix of public and private


HEre is everything from the general Cloud Computing. In the next part we will focus of Azure specific scenarios. 
```



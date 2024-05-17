---
author: Jerzy Wickowski
categories:
  - dotnet
date: 2024-05-17T09:40:58.000Z
disqus_identifier: general-azure-infrastructure-az-900
disqus_title: General Azure infrastructure for AZ-900
disqus_url: 'https://codepruner.com/general-azure-infrastructure-az-900'
draft: false
images:
  - images/posts/2024/2024-05-17-general-azure-infrastructure-az-900.jfif
tags:
  - az-900
  - dotnet
  - Azure
  - certificate
  - course
  - exam
  - infrastructure
title: General Azure infrastructure for AZ-900
type: regular
url: general-azure-infrastructure-az-900
---
Next part of my preparing to pass AZ-900 exam is understanding how Azure is build, because that knowledge is crucial to create high available and scalable solutions. We will go from the lowest structure to the highest ones. Let's come with me on this journey to the Azure infrastructure.

Everything runs on servers, but to be honest it's only an implementation details and can be ignored from our perspective. We can start thinking from a groups of server, called datacenters. Datacenters can, but don't have to work in Availability Zone. They are grouped into regions. Regions are grouped into geographies. It is everything, but let's go a bit deeper.

## Datacenter
It is the lowest, important for us, level of Azure Infrastructure. There are some important facts about datacenter we should know:

- It a physical building or buildings with a huge amount of servers
- Every datacenter have an independent power supply, cooling, and networking
  - So if there are two datacenters in one region, they should not fail at the same time
- Group of datacenters are called regions

## Regions 
It is the base term you can see during azure Usage.
- It is physical location with a set of datacenters. 
- When you create request a service, you have to select a region
- You can check what region is the closest to you
  - [Azure Speed Test 2.0](https://azurespeedtest.azurewebsites.net/)
- You may be aware that not every service is available in every region
  - [Tool to check if a service is available in a selected region](https://azure.microsoft.com/en-us/explore/global-infrastructure/products-by-region/) - a 
- Region examples: `East US`, `West Europe`, `Central Poland`, etc.
- Many regions have a pair

### Region pairs
Not every region has a pair, but 
- When regions are paired that cross-region replication is enabled
- The idea of region pairs is to have a backup in case of a disaster
  - The distance between regions should be at least 500 km
  - in case of natural disaster, like earthquake, flood, etc.
- You cannot select region pairs. The are predefined by Microsoft
- All planned updates are not done in the same time in pairs, in case of failure

## Availability Zone
There is another level to improve the availability of the service.
- When there is a bigger region then it can be divided into Availability Zones.
- One availability zone contains one or more datacenters
- Not every region has option to select availability zones
- When one datacenter is down, the other (in a different Availability Zone) can still work

## Geographies
It ia bigger area in Azure glossary.
- It works on a higher level
- It is more about data residency and compliance
  - For example some data should not leave the country or continent
- Inside Geographies is a group of regions
- eg. North America, Europe, Asia Pacific, etc.

## Global services
In most cases when you use azure services you must select a region, but there are some services that are global. By Global I mean that they are just avaialble in Azure and it is transparant for you where they are located. For example:
- Azure Entra ID
  - old Azure Active Directory
- Azure DevOps


## Do you want more?
Let me know in the comments!

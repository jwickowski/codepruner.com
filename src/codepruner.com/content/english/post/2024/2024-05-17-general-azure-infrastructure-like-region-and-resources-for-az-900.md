---
author: Jerzy Wickowski
categories:
  - dotnet
date: 2024-05-17T06:40:58.000Z
draft: true
images:
  - images/posts/2024/2024-05-17-general-azure-infrastructure-like-region-and-resources-for-az-900.jpg
tags:
  - az-900
  - dotnet
  - Azure
  - certificate
  - course
  - exam
  - infrastructure
type: regular
title: General Azure infrastructure for AZ-900
---
Next part of my preparing to pass AZ-900 exam is understanding how Azure is build, because that knowledge is crucial to create high available and scalable solutions. We will go from the lowest structure to the highest ones. Let's come with me on this journey to the Azure infrastructure.

Everything runs on servers, but to be honest it's only an implementation details and can be ignored from our perspective. We can start thinking from a groups of sever, called Datacenter. Datacenters can, but don't have to work as Availability Zone. They they are grouped into regions. Regions are grouped into geographies. It is everything, but let's goa bit deeper.

## Datacenter
It is the lowest,important for us, level of Azure Infrastructure. There are some important facts about datacenter we should know:

- It a physical building or buildings with a huge amount of servers
- Every datacenter have an independent power supply, cooling, and networking
  - So if there are two datacenters in one region, they should not fail at the same time
- Group of Datacenters are called regions

## Regions 
It is the base term you can see during azure Usage.
- It is physical location with a set of datacenters. 
- When you create request a service, you have to select a region
- You can check what region is the closest to you
  - [Azure Speed Test 2.0](https://azurespeedtest.azurewebsites.net/)
- You may be aware that not every service is available in every region
  - [Products available by region](https://azure.microsoft.com/en-us/explore/global-infrastructure/products-by-region/) - a tool to check if a service is available in a selected region
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


- UNDERSTAND IT BETTER and maybe create a diagram
  - Locally Redundant Storage (LRS) - data is replicated 3 times in the same datacenter
  - Zone Redundant Storage (ZRS) - data is replicated 3 times in 3 different datacenters
  - Geo Redundant Storage (GRS) - data is replicated 3 times in the same region and 3 times in a different region
  - Read-Access Geo Redundant Storage (RA-GRS) - the same as GRS but you can read data from the second region
  - Geo Zone Redundant Storage (GZRS) - the same as GRS but data is replicated in 3 different regions
  - Read-Access Geo Zone Redundant Storage (RA-GZRS) - the same as GZRS but you can read data from the second region
  - Read-access geo redundant storage (RA-GRS) - the same as GRS but you can read data from the second region

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





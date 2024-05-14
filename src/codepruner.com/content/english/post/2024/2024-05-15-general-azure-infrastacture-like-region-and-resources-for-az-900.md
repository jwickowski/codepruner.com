---
author: Jerzy Wickowski
categories:
  - dotnet
date: 2024-05-13T06:40:58.000Z
draft: true
images:
  - images/posts/2024/2024-05-15-general-azure-infrastacture-like-region-and-resources-for-az-900.jpg
tags:
  - az-900
  - dotnet
  - Azure
  - certificate
  - course
  - exam
type: regular
---
Next part of preparinf for AZ-900 exam. This time I will focus on the infrastructure part.


## Datacenter
- everything is run on servers
- servers are in datacenters

- group of datacerters are called regions
- regions are spread around the world

- datacenters are connected with high-speed/low-latency network

- every service should have selcted region(location)
- you can select region(location) when you create a service

- there are some services that are global
  - eg. Azure Active Directory


- [Azure Speed Test 2.0](https://azurespeedtest.azurewebsites.net/) - - a tool to check latency between regions
- [Products available by region](https://azure.microsoft.com/en-us/explore/global-infrastructure/products-by-region/) - a tool to check if a service is available in a selected region


## Availability Zone
- regional feature
- we can say that one availability zone is one datacenter
- you can select multiple availability zones
- in one region are multiple availability zones
- when one datacenter is down, the other is still working
- not every region has availability zones


- UNDERSTAND IT BETTER and maybe cretae a diagram
  - Locally Redundant Storage (LRS) - data is replicated 3 times in the same datacenter
  - Zone Redundant Storage (ZRS) - data is replicated 3 times in 3 different datacenters
  - Geo Redundant Storage (GRS) - data is replicated 3 times in the same region and 3 times in a different region
  - Read-Access Geo Redundant Storage (RA-GRS) - the same as GRS but you can read data from the second region
  - Geo Zone Redundant Storage (GZRS) - the same as GRS but data is replicated in 3 different regions
  - Read-Access Geo Zone Redundant Storage (RA-GZRS) - the same as GZRS but you can read data from the second region
  - Read-access geo redundant storage (RA-GRS) - the same as GRS but you can read data from the second region


## region pairs
- every region has a pair
- at least >300 miles away (500 km) (when possible)
  - in case of natural disaster, like earthquare, flood, etc.
- Each region is paried with another region within the same geography
- you cannot select region pairs (just check in documentation)
- planed updated across the region pairs
  - to not brake both on the same time

## geographies
- group of regions
- eg. North America, Europe, Asia Pacific, etc.
- 

# resources, resources groups,
- resources
  - it is just a service
  - every service is a resource
  - can be represent by a JSON 
    - type
    - api version
    - name
    - location
    - and more and more
- resource group
  - logical container for resources
    - it can be grouped by:
      - type (all sql together) 
      - lifecycle (all resources for one project)
      - environment (all resources for dev, test, prod)
      - by billing/location
- tool: Azure Resource Explorer 
- resource managger 
  - you can create requirses in many ways
    - portal
    - rest
    - powershell
    - cli
    - sdk
  - but all of these are connectes to `Azure Resource Manager (ARM)`
  - ARM is responsible also for Access Control



  - protal



# Access control (IAM)
- Identity and Access Management
- who can do what
- role-based access control (UNDERSTAND IT)
  - roles
    - owner - can do anything
    - contributor
    - reader
    - user access administrator
    - etc.
  - you can create custom roles
- to check:
  - if you add access on resource group, it is inherited by all resources in this group ???






















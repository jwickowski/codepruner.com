---
author: Jerzy Wickowski
categories:
  - dotnet
date: 2024-05-20T06:40:58.000Z
draft: true
images:
  - images/posts/2024/2024-05-20-resources-and-services-in-azure-for-az-900jpg
tags:
  - az-900
  - dotnet
  - Azure
  - certificate
  - course
  - exam
  - infrastructure
title: Resources and services in azure for AZ-900
type: regular
---

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
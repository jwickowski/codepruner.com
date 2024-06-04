---
author: Jerzy Wickowski
categories:
  - dotnet
date: 2024-06-04T06:40:58.000Z
disqus_identifier: core-services-in-azure-for-az-900
disqus_title: Core services in Azure for AZ-900
disqus_url: 'https://codepruner.com/core-services-in-azure-for-az-900'
draft: true
images:
  - images/posts/2024/2024-06-04-core-services-in-azure-for-az-900.jpg
tags:
  - az-900
  - dotnet
  - Azure
  - certificate
  - course
  - exam
  - infrastructure
  - compute
  - storage
  - network
title: Core services in Azure for AZ-900
type: regular
url: core-services-in-azure-for-az-900
---

Welcome in the next episode of the preparing to passing excam AZ-900. In that episode we will cover the most popular resources in Azure. They are be grouped by the role they have, like computing, networking or storage.

## Resources and Resources Groups
But before we bite a hard meat we should try the appetizer, because there are two generic topics we have to talk.
The 1st is:

### Resources
It is the base unit in Azure. Every services exist in Azure is a Resource. It doesn't matter whether it is a `WebApp`, `Azure Sql` or `Search AI`. So every service has its own properties, but because all of them are resources thay have some common properties:
- Every service is a resource in Azure
- It can be represent by JSON with `type`, `api version`, `name`, `location`
- They can be created in many ways
  - Azure Portal, Rest API, PowerShel, SDK, Bicep, Teraform 
  - Every of these tools have the same functionality, because all of these tools use `Azure Resource Manager(ARM)`. It is a main place in Azure where Resources are requested.
    - More over ARM is responsible for Access Control
- There is a tool: Azure Resource Explorer, that allows to check exisitng Resources

### Resource Group
Ok. It is the 1st services we will talk. Why? Because you have to have at least one `Resource Group` to create any service or in reverse direction, every resourse must have assigned `Resource Group`. 

You can think about `RG` that is is just a container for other services. It is a free service and you can have so many `RGs` as you want and it depends on you how to use it. You can Group Resources by:
- type 
  - grupping by service type, like Apps, Storages, etc 
- lifecycle 
  - all resources for one project
  - when project is done you can just remove the whole resource group
- environment
  - You can create resource for every environemnt
  - for dev, test, prod
  - that approach allows you to create new environemnt easly
- billing
  - when you need to distinush the price of resources
  - or when resources in the same RG has common costs 
 - location
  - to create a `RG` for services in specific region


## Compute services
Lets dive a bit into the main course. Let's start with the most interesting part for developers: `Compute services`. We talk about resources where we can execute our code. They are order from the heaviest to the lightest.

### Azure Virual Machines
It is just a way to run a VM in the cloud. So it is IaaS (Infrastacture as a Service) with all it pros and cons:
- To establish VM you need to provide an image
  - You can use an image from marketplace
  - or you can build your own image and use it 
- There is no option to Scale.
  - Of course you can remove the VM and create a new  
- When create you can select the size of the machine
- You can use RDP (Remore Desktop Protocol) to connect to he machine

### Azure Virtual Machine Scale Sets
We can call it `Scallable Virtual Machines`. 
- It is a set of identical virtual machines
- It can be scalled autoamtically

### Azure Kubernetes Service (AKS)
It is an option to have Kubernetes in Azure. It is not a clear K8S, because is is managed by Azure so it is much easier to work with.

### Azure Container Instances (ACI)
It is a simpler way of publishing a container. We can call it: 'Containers as a service'.  You don't have so many option like in AKS, but it is much easier to configure and use. 
- It is simplest and the fastest way to run a container in Azure
- You can run containers without managing the infrastructure
- There is a possibility to scale

### App Service/WebApp
I think it is the most common used service in Azure. It is just a way to host your application. You can publish a package or a container image. 
- It support many languages nad platforms like: `.NET`, `Java`, `Node`, `PHP` nad more
- It can scale automatically

### Azure Functions
It is even simpler than App Service, because you can run just a  piece of code.
- It is clearly Serverless approach. You don't care what is behind. 
- It hides evertyhing from you
- There are two hosting models
    - Consumption plan - You pay only for the time when the function is running
    - Dedicated plan - You pay for the resources
- Triggers are important. Because you can tell when a function should be invoked. It can be:
  - Http request - the most common thing
  - a change in a different Service like: 
    - Adding message to queue
    - Uploading a file to blob 


## Network services
The compute power is not everything. To utilize the power you must manage the connection between them. But with Network services you can also establish the connection with 'on-prem services', protect and monitor services. Check them.


### Azure Virtual Network
The core mechanism to control networks in Azure. 
- It is an emulation of physical network infrastacture
- It contains one or more`Subnet`
- One `Virtual Network` is for a single region
- If you want to connect multiple virtual network you can:
  - use VNet peering
  - use VPN Gateway
- There is a nice feature in Azure to draw your network topology 
- To control the trafic to a `Subnet` use `Network Security Group`
  - It can control the trafic from internet
  - but also between `Subnets`

### Virtual Network Gateway (VPN Gateway)
If you want to connect your `On-prem` services or `Virtual Network` from different regions. You can use `VPN Gateway`
regions

### Azure Load Balancer
You can use it when you have more than one istance of your application you need something to split the traffic.
- It distributes the incoming traffic to the resources
  - When you [scale OUT]({{< relref "./2024-05-13-general-information-about-cloud-computing-az-900.md" >}})
- Load balancer can automaticly check the health of the resources
  - So if a service stops working then Load Balander will redirect trafic to others
- We can distingush two types of load balancer.
  - public - it work in the front of the application to redirect the traffic from internet
  - private / internal - it works in the backed 

### Application Gateway
It is like `Load Balancer`, but on higher abstraction layer and with a bit more features:
- Firewall - to block not wanted traffic
- Redirection
- Session affinity - to handle sticky session
- Url-based routing
- SSL termination
  - eg. remove SSL from the request and send it to the backend as a HTTP

### Content Delivery Network (CDN)
When you have static files to download in your app, you can use CDN. There are a lot of POP (Point of Presence) spread around the world to minialize the distance between user and the content. You should use it to:
- Minimalize latency, because there are a lot of POP
- Cache the content in close distance to the end user
- Reduce the web server load, because some part of the data can be transfered from CDN, not the app

## Storage services
On the dessert I have Storage for you. It doesn't matter how much compute power you have and how fast your network is, if you don't have a place to store your data. 

Before we start talking about services should disinguish type of data. We can have
- Structured data
  - eg. SQL database
- Unstructured data
  - Files, images, videos, etc.
- Semi-structured data
  - like in Document database
  - like Json

so... when we know type of data we can start storing them in Azure Services:

### Azure Storage Account
When we talk about data in Azure we must mention Azure Storage Account. It is a group of sub-services to store data. 
- sub-services
  - blob storage
  - queue storage
  - table storage
  - file storage
- It is the chipest way to store data in Azure

Lets check these storage one by one.

#### Azure Blob Storage
- It is a place to store blobs
- BLOB = Binary Large Object 
- BLOB is unstructured data
- When you create a container you must selct tier:
  - Hot 
    - Frequency accessed data
    - The most expensive
  - Cool
    - Infrequently accessed data
    - lower availability
    - good for backup
  - Archive
    - rarely accessed data
    - the if you don't want to use that data for a long time
    - the cheapest
    - it can take hours to access the data

#### Queue storage
- It is a place to store messages
- These messages must be small
- Designed for asynchronous communication

#### Azure Table storage
- It is a NoSQL database
- It is for storing semi structured
- It is good for storing large amount of data
- There is no possibility to joins
- There is no schema
- You should use `row key` and `partition key` to access the data 
  - partition key - it is a way to store rows in the same location

#### Azure File Storage
It is simillar to blog storage, but you can use SMB protocol to access the data, so you can mount it as a network drive.
- There are some differece with names:
  - blob -> file
  - container -> share
  - blob storage -> file storage

### Azure Disk storage
It is an emulation of a physical disk. So you can attach it to the VM.

### Database services
Let's switch to the most advanced storage options. If you want to store data in a more structured way you should use a database.  

## Azure CosmosDB
It is very scalable database with interesting properties:
- It can be replicated automatically through regions on the whole world
- It is low latency database with response lower then 10 ms 
- It can handle multiple APIs like: `SQL`,`MongoDb`, `PostgreSQL`, `NoSQL`, `Apache Cassandra`, `Apache Gremlin`

## Azure SQL database. 
The 1st option for structured data. We can call it PaaS or DBaaS(Database as a Service)
- When you want to create a database you need to create a server. Then you can have multiple databases per server.
- It can be scaled easily
- It is the simplest option a limited version of Sql Server, but if you need more you can use: 
  - Sql Managed Instance - It is a more complete version of Sql Server
  - Sql Data Warehouse - It is a MPP (Massively Parallel Processing) database
  - Sql Server on VM - It is like standard SqlServer, but on VM in Azure
- If you have used SQL Server you will be familiar with it, but also with some other services like:
  - Reporting services (SSRS) => Power BI
  - Integration services (SSIS) => Data Factory
  - Analysis services (SSAS) => Analysis Services
 
## More databases
There are also options to use different database engines in Azure like: `MySQL` and `PostrgeSQL`. So You can 

## Summary
It is everything for now. I know there are more and more details to talk like Replication Options, RBAC and of couse we can go details with every of these services. 

If you want to read more about specific service. Let me know in the comments below.
Is you want to be informed about next posts, join to the subsciption list and become one of CodePruners.

See you in the next article.

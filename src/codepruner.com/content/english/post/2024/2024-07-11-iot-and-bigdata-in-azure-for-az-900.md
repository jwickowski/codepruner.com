---
author: Jerzy Wickowski
categories:
  - dotnet
date: 2024-07-11T05:40:58.000Z
draft: true
images:
  - images/posts/2024/2024-07-11-iot-and-bigdata-in-azure-for-az-900.jpg
tags:
  - az-900
  - Azure
  - certificate
  - course
  - exam
  - iot
  - bigdata
title: IoT and BigData in Azure for AZ-900
type: trending

---
In the [last post]({{< relref "./2024-06-04-core-services-in-azure-for-az-900.md" >}}) you were able to read about core services in Azure for storage, network and computing. Now it is time to dive into more specialized services for IoT and BigData.

## IoT
IoT (Internet of Things) is more and more popular. There was a peak about 2018. Now we don't hear too much about it, because we have new buzz words like AI, GPT, etc. But there is still a huge need of collecting a great deal of data from sensors. IoT services are here to handle these requirements.

Before we dive into IoT, we should have an answer for the question: What IoT is?
It is a network of connected devices. There devices send telemetry information, but also can receive configurations. Examples? Of course, cars; light bulbs; cameras; temperature sensors;

### IoT Hub
It is the base Azure IoT service to help us working with IoT. 
- It allows for bidirectional communication between IoT Hub and devices
- It supports multiple protocols: `HTTPS`, `AMQP`,`MQTT`
- We can cal it PaaS for IoT
- It integrates with a lot of Azure services
- There are SDKs available in many languages

To test it there is `Rasperry Pi Azure IoT Online Simulator` to simulate a usage of example IoT device.

### IoT Central
It is a service on higher abstraction layer then IoT Hub. We can call it a SaaS for for connecting, managing and monitoring IoT devices. It uses more than 30 other Azure services behind.

### Azure Sphere
It is something more than just a service it is an ecosystem for IoT from Microsoft. It is build both from physical chip with dedicated OS and the service in the cloud.
- The OS on that system will be updated by Microsoft
- You can write your app on that chip
- There is a way to update software on these devices

There are related services:
- Azure Sphere Security Service   
  - It is used to communicate the cloud with devices
  - Microsoft can apply upload updated to OS with that
  - You can upload an update to the app with that
  - The main reason is ti create secure e2e solution

## BigData
The second topic for today it BigData. As previous we should start with describing what BigData is, because it has more complicated definition than IoT. Although everyone heard about BigData, it is still a mysterious area. So make it clear. What BigData is?

It is a part of technology that helps us with processing too large or too complex set of data for traditional software solutions. There are some metrics when we can talk about BigData:
- Velocity:
  1. Realtime
  2. Near Realtime
  3. Periodic
  4. Batch
- Volume
  1. Petabytes
  2. Terabytes
  3. Gigabytes
  4. Megabytes
- Variety
  1. Video/Social
  2. Photo/Audio
  3. Database
  4. Table

If the position, in any of these 3 areas, is higher, then BigData solutions are more probable. 

Before we deep dive into services we need to cover some steps how we work with data:
1. Identify where your data is - I think it is the most important. We have to know where our data are to start working with them
2. Ingest the data to cloud - Then you should move them to the could to allow other services to have access to it
3. Transform it to good structure - I think it is clear.
4. Store them - We should keep these files and he transformed ones
5. Serve the results - After analyzing we want to use that data. For example to show a dashboard with them

### Azure Synapse Analytics
Synapse allows to build pipelines for Ingesting and Transforming data with visual tools.
Tools inside:
- Apache Spark
- Synapse SQL

There is `Synapse Studio`. To integrate these tools. Moreover it has integration with `Azure Data Lake Storage`.
- It a PaaS, Big data analytics platform

### Azure HDInsights
- It can handle every process in the processing BigData
- It is multi-purpose big data platform
- It provides `Big Data Clusters` like:  `Hadoop`, `Kafka`, `Spark`, `Hive`, `Storm`, `HBase`

### Azure Databricks
It is similar to HDInsight, but these clusters based on `Apache Spark` and `Apache Spark Alone`.
It is also a collaboration platform for data engineers
User can focus on analyzing, not on management the platform
It integrates with Auzre Services, so getting data from Blobs is very easy


## Summary
Thank you for reading. I hope it is more clear for you know. Fo me BigData is still a magic, but I am planning to update the post when I my knowledge is wider.

Leave a comment below if you want to add or ask anything. See you next time :)

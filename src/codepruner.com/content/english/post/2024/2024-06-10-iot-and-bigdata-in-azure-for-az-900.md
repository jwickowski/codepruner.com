---
author: Jerzy Wickowski
categories:
  - dotnet
date: 2024-06-06T09:40:58.000Z
draft: true
images:
  - images/posts/2024/2024-06-10-iot-and-bigdata-in-azure-for-az-900.jpg
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
IoT (Internet of Things) is more and more popular. There was a peak about 2018. Now we don't hear too much about it, because we have new buzz words like AI, GPT, etc. But there is still a huge need of collecting a great deal of data from sensors. IoT services are here to handle these requirenemnts.

Before we dive into IoT, we should have an aswer for the question: What IoT is?
It is a network of connected devices. There devices send telemetry informations, but also can recieve configurations. Examples? Of course, cars; light bulbs; cameras; temperature sensors;

### IoT Hub
It is the base Azure IoT service to help us working with IoT. 
- It allows for bidirectional communication between IoT Hub and devices
- It supports multiple protocols: `HTTPS`, `AMQP`,`MQTT`
- We can cal it PaaS for IoT
- It integrates with a lot of Azure services
- There are SDKs avaliable in many languages

To test it there is `Rasperry Pi Azure IoT Online Simulator` to simulate a usage of example IoT device.

### IoT Central
It is a servise on higher abstraction layer then IoT Hub. We can sall it a SaaS for for connecting, managing and monitoring IoT devies. It uses more than 30 other Azure services behind.

### Azure  Sphere
It is something more than just a service it is an ecosystem for IoT from Microsoft. It is build both from phisical chip with dedicated OS and the servie in the cloud.
- The OS on that system will be updated by Microsoft
- You can write your app on that chip
- There is a way to update software on these devicess 

There are related services:
- Azure Sphere Secirity Service   
  - It is used to communicate the cloud with devices
  - Microsoft can apply upload updated to OS with that
  - You can upload an update to the app with that
  - The main reason is ti create secure e2e solution

## BigData
The second topic for today it BigData. As previus we should start with describing what BigData is, because it has more complicated definition than IoT. Although everyone heard about BigData, it is still a misteriuos area. So make it clear. What BigData is?

It is a part of technology that helps us with processing too large or too compex set of data for traditional software solutions. There are some metricks when we can talk about BigData:
- Velocity:
  1. Realtime
  2. Near Realtime
  3. Periodic
  4. Batch
- Volume
  1. Peta bytes
  2. Terra bytes
  3. Giga bytes
  4. Mega bytes
- Variety
  1. Video/Social
  2. Photo/Audio
  3. Database
  4. Table

If the position, in any of these 3 areas, is higher, then BigData solutions are more probable. 

### Azure Synapse Analutics
Before we deep into services we need to cover how our data are analysed.
1. Identify where your data is
2. Ingect the data to cloud
3. Transform it to good structure
4. Store them
5. Serve the results

Synapse allows to build pipelies for Ingest and Transform data with visual tools
There is some tools inside:
- Apache Spark
- Synapse SQL

There is `Synapse Studio`. To integrate these tools. Moreover it has integration with `Azure Data Lake Storage`.

- It a PaaS, Big data analytics platform
### Azure HDInsights
- It can handle every process in the processing BigData
- It is multi-purpose big data platform
- It provides `Big Data Clusters` like:
  - Hadoop
  - Kafka
  - Spark
  - Hive
  - Storm
  - HBase

### Azure Databricks
It is smillar to HDInsight, but these clusters based on `Apache Spark` and `Àpache Spart Alone`.
It is also a collaboration platform for data engeeners
It is BigData colaboration platform
User can focuss on analyzing, not on management the platform
It integrates with Auzre Services, so getting data from Blobs is very easy


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
IoT (Internet of Things) is more and more popular. There was a peak about 2018. Not we don't hear too much about it, because we have new buzz words like AI, GPT, etc. But there is a huge need to collect a huge amout of data from sensors. IoT services are here to handle these requirenemnts.

### What is IoT?
It is a network of connected devices. There devices send telemetry informations, but also can recieve configurations. Examples? Of course, cars; light bulb; cameras; temperature sensors;

### IoT Hub
- It allows for bidirectional communication between IoT Hub and devices
- PaaS for IoT
- Integrates with a lot of Azure services
- There are SDKs avaliable in many languages
- It supports multiple protocols: `HTTPS`, `AMQP`,`MQTT`

There is `Rasperry Pi Azure IoT Online Simulator` to simulate a usage of example IoT device.
To connect a device to IoT Hub:
-  you need to pass connection string to that device

### IoT Central
It is simmilar to Iot Hub, but it works on a differene level.
- It is build on IoT Hub and 30+ Azure Services
- It is IoT App Platform - SaaS
- Industry specific app tempates
- Service for connecting, managing and monitoring IoT devies

### Azure  Sphere
- It is not just a service it is set of component to build IoT app
- - It looks that a part of hardware needs to be installed in devices 
- The OS on that system will be updated by Microsoft
- Then your app can run on that sevice
- certified chips

There are related services:
- Azure Sphere Secirity Service   
  - It is used to communicate the cloud with devices
  - Microsoft can apply upload updated to OS with that
  - You can upload an update to the app with that
  - The main reason is ti create secure e2e solution

## BigData
Every one heard about BigData, but what is it and how to work with that? Let's check.

## What is BigData?
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


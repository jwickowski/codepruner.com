---
aliases:
  - >-
    /posts/2029-09-22/how-to-run-postgresql-and-adminer-or-pgadmin-with-docker-compose
author: Jerzy Wickowski
categories:
  - docker
date: 2023-09-22T05:40:58.000Z
disqus_identifier: >-
  post-2023/2023-09-22-how-to-run-postgresql-and-adminer-or-pgadmin-with-docker-compose
disqus_title: How to run postgresql and adminer/pgadmin with docker compose
disqus_url: >-
  https://codepruner.com/post/2023/2023-09-22-how-to-run-postgresql-and-adminer-or-pgadmin-with-docker-compose
draft: false
images:
  - >-
    images/posts/2023/2023-09-22-how-to-run-postgresql-and-adminer-or-pgadmin-with-docker-compose.png
tags:
  - sql
  - postgres
  - docker
  - adminer
  - pgadmin
  - docker compose
title: How to run postgresql and adminer/pgadmin with docker compose
type: trending
url: >-
  post/2023/2023-09-22-how-to-run-postgresql-and-adminer-or-pgadmin-with-docker-compose
---

I usually develop in .net and I use SqlServer database, but sometimes it is time to change the environment a bit and try something new. I decided to use PostgreSQL. It is a new tool for me, so I had some trouble at the beginning with running that with docker compose, because I wasn't able to establish a connection by `Adminer` and `pgAdmin`. Take a look at how I solved the issue.

### Lets start with docker-compose.yml file
{{<code language="yaml" file="static/examples/CodePruner.Examples/PostgresAndDockerCompose/docker-compose.yml" >}}

Here I have defined three services:
* `postgres` - a Postgress database
* `adminer` - database management tool
* `pgadmin` - database management tool
 
All of the parameters are the standard ones, but if you would like I can extend the description. Just leave a comment :)

### Time to run it
You can run it with command: `docker compose up`. Wait a bit, and everything should start working.

You can check if `adminer` or `pgadmin` are working, by opening browser with correct address:
- adminer: `http://localhost:8080`
- pgadmin: `http://localhost:5050`

#### Lets start with `adminer`
When it is open you can see a simple form with data to establish the connection.  
My 1st try was to fill it with data:
- server: `localhost:5432`
- user: `root`
- password: `postgres`

and I got an error:

{{< notice "warning" >}}
  Unable to connect to PostgreSQL server: could not connect to server: Connection refused Is the server running on host "localhost" (127.0.0.1) and accepting TCP/IP connections on port 5432? could not connect to server: Address not available Is the server running on host "localhost" (::1) and accepting TCP/IP connections on port 5432?
{{< /notice >}}

then I started to search and investigate the issue. So you will not have to do it.
You should to use docker container name instead of localhost. So when you use below data:
- server: `postgres`
- user: `root`
- password: `postgres`

You will be able to have the working connection to the database with `adminer`

#### Lets start with `postgres`
When you have the knowledge from the previous paragraph, it becomes simple.
* Login with credentials from the file:
  * login: `pgadmin@codepruner.com`
  * password: `pgadminP@ssw0rd!` 
* Click `Add new server`
* Fill data in connection tab:
  * Host name/address: `postgres`
  * username: `root`
  * password: `postgres`
* Fill name in 1st tab and click `Save`

Then you are connected to the database with `pgadmin`.

### Did you have the same problem? 

I hope I helped you. 
Or maybe do you have a different problem?
Let me know in comments.

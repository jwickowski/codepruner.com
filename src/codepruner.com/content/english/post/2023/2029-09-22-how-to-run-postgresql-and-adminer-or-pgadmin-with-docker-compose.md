---
title: "How to run postgresql and adminer/pgadmin with docker compose"
author: "Jerzy Wickowski"
images:
  - "images/posts/2023/2023-09-22-how-to-run-postgresql-and-adminer-or-pgadmin-with-docker-compose.png" 
url: "posts/2029-09-22/how-to-run-postgresql-and-adminer-or-pgadmin-with-docker-compose"
date: 2023-09-22T06:40:58+01:00
draft: true
tags: ["sql", "postgres", "docker", "adminer", "pgadmin", "docker compose"]
categories: ["docker"]
type: "regular"

---

I usually develop in .net and use mssql database, but it is time to change a bit and try something new. I decided to use PostgreSQL. I will use it with Adminer and pgAdmin. I will run it with docker-compose.

### Lets start with docker-compose.yml file
```yaml
version: "3.9"
services:
  postgres10:
    container_name: pg_container
    image: "postgres"
    restart: always
    ports:
      - "5432:5432"
    environment:
      POSTGRES_PASSWORD: postgres
      POSTGRES_USER: root 
      POSTGRES_DB: marten_testing
  adminer:
    image: dockette/adminer
    restart: always
    depends_on:
      - postgres10
    ports:
      - 8080:80  
  pgadmin:
    container_name: pgadmin4_container
    image: dpage/pgadmin4
    restart: always
    ports:
      - 5050:80
    environment:
       PGADMIN_DEFAULT_EMAIL: "pg@admin.pl"
       PGADMIN_DEFAULT_PASSWORD: "pgadmin"
... 

Here I have defined three services:
* postgres10 - PostgreSQL database
* adminer - Adminer database management tool
* pgadmin - pgAdmin database management tool
 describe parameters

ok you can run it with command: `docker compose up`
and it should work, but 
### how to connect to database?
My first try was to open adminer or pgadmin on the browser
- adminer: http://localhost:8080
- pgadmin: http://localhost:5050

and it opened... I fill forms with data:
- server: localhost:5432
- user: root
- password: postgres

and it didn't work, but after a bit of investigation i have found a solution. I have to use docker container name instead of localhost. So I have to use:
- server: pg_container:5432
- user: root
- password: postgres

and then it works.



### Did you have the same problem? 

or maybe w different one?
Let me know in comments.



---
title: "How to configure CI/CD for hugo in practice with GitHub Actions?"
author: "Jerzy Wickowski"
images:
  - "images/blog/red-sharp-pruner.jpg"
url: "posts/hugo/how-to-configure-ci-cd-for-hugo-in-practice"
date: 2021-04-16T04:40:58+01:00
draft: false
tags: ["hugo", "github actions", "devops"]
categories: ["hugo"]
---

Some time ago we talked what is good approch to depoloy hugo blog. Now I know we want to use GitHub to deploy it using FTP. Arguments for that solution you can find [here](how-to-configure-ci-cd-for-hugo). 

On github there is GitHub Actions. It is a server CI build in GitHub, so integration with GitHub is fantastic. Moreover there is very easy way to extend it, by creating custom Actions. Today we are going to use some existing actions.

Soo, let's get started

## Step one: prepare workflow
To add a workflow in github, you have to create file yml in `.github\workflows`. You can do it manually or you can use a wizzard on the page. In that step you have to add some basic info and it can look like this:

```
name: CI

on:
  push:
    branches: [ master ]

  workflow_dispatch:

jobs:
  build:
    runs-on: ubuntu-latest

    steps:
      - uses: actions/checkout@v2
        with:
          submodules: true 
          fetch-depth: 0
```

We configured to:
- Run workflow when code to master will be pushed
- But we have options to run in manually `workflow_dispatch:`
- It will be run on ubuntu
- We want to download/checkout the source code with submodules
  - because template is added as submodule

## Step two: build the page
In next step we need to setup hugo and build the blog. It can look like that:
```
      - name: Setup Hugo
        uses: peaceiris/actions-hugo@v2
        with:
          hugo-version: '0.82.0'

      - name: Build
        run: hugo --source "src/codepruner.com/" --minify 
```

## Step three: deploy it using FTP
To deploye the site using FTP you have to create an FTP account on your hosting provider. When you have it you can add deployment into your yml file:
```      - name: Upload ftp
        uses: sebastianpopp/ftp-action@releases/v2
        with:
          host: ${{ secrets.FTP_SERVER }} 
          user: ${{ secrets.FTP_USERNAME }} 
          password: ${{ secrets.FTP_PASSWORD }} 
          localDir: "src/codepruner.com/public"
          remoteDir: "/public_html"
          forceSsl: true
```

As you notice I didn't add credentials into yml file. It is becouse of security. We don't want to add any sensitive data into repository. They should be stored outside.

In Github you have an opptions to add secrets and use it it pipelines.

## Be happy
Now the simple version of CI/CD pipeline is working for me. 
When I add a post in markdown into repository it should be build and deployed. I will be tested it in the future.

What is is way to delivery content?



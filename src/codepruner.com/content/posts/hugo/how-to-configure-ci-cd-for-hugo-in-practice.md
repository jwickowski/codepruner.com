---
title: "How to configure CI/CD for hugo in practice?"
date: 2021-04-16T20:40:58+01:00
draft: true
tags: ["hugo", "github actions", "devops"]
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
          hugo-version: '0.82'

      - name: Build
        run: hugo --source "src/codepruner.com/" --minify 
```

## Step three: deploy it using FTP




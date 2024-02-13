---
aliases:
  - /posts/hugo/how-to-configure-ci-cd-for-hugo-with-azure-devops-pipelines
author: Jerzy Wickowski
categories:
  - blog
date: 2023-09-13T04:40:58.000Z
disqus_identifier: >-
  post-2023/2023-09-13-how-to-configure-ci-cd-for-hugo-with-azure-devops-pipelines
disqus_title: How to configure CI/CD for hugo with Azure DevOps Pipelines?
disqus_url: >-
  https://codepruner.com/post/2023/2023-09-13-how-to-configure-ci-cd-for-hugo-with-azure-devops-pipelines
draft: false
images:
  - >-
    images/posts/2023/2023-09-13-how-to-configure-ci-cd-for-hugo-with-azure-devops-pipelines.jpg
tags:
  - hugo
  - azure devops
  - pipelines
  - yaml
  - devops
  - gethugothemes
  - multistage
  - chocolatey
title: How to configure CI/CD for hugo with Azure DevOps Pipelines?
type: regular
url: >-
  post/2023/2023-09-13-how-to-configure-ci-cd-for-hugo-with-azure-devops-pipelines
---

When I have started the blog more than two years ago I decided to [automate the deployment with GitHub Actions]({{< relref "./2023-09-13-how-to-configure-ci-cd-for-hugo-with-azure-devops-pipelines.md" >}}). It still works, but I had a need to create another hugo page, but this time, the source code is in AzureDevops. So I would like to invite you to read how to configure deployment Hugo with Azure DevOps Pipelines in multistage YAML.

# The requirements disallow me the simplest solutions
I didn't start with with a pure and clean hugo page, but I use one of themes from (GetHugoThemes.com)[https://gethugothemes.com/]. So I wasn't able to use any version, but a specific one, recommend from theme creator and it was `Hugo Extended 0.113.0`.

Here are my tries that didn't work:
- use a ready-to-use action from marketplace
  - but Azure Devops is not so open as GitHub Actions, I wasn't able to find a good and up-to-date action to use. Everything I found was really old and wasn't updated to the newest version.
- use a docker image
  - I have found an information on [gohugo.io](https://gohugo.io/installation/linux/#docker) about recommended docker image, but the newest image version was `0.101.2` and it was too old for me.
- install hugo with apt-get
  - but the newest version wasn't enough too

But there is a different option. After I knew what I mustn't use, I have found a working solutions.
- use image `windows-latest`
- and use `chocolatey` to install the correct hugo version
  - thanks to [Pauby](https://blog.pauby.com/) for maintaining the package


# Building that works
Here is working pipeline configuration in yaml format.
{{< highlight yaml >}}
trigger:
 batch: true
 branches: 
  include:
    - main

pool:
  vmImage: windows-latest

stages:
  - stage: Build
    displayName: Build hugo
    jobs:
      - job: Build
        steps:
          
          - task: PowerShell@2
            displayName: Install hugo-extend
            inputs:
              targetType: 'inline'
              script: |
                choco install hugo-extended --version 0.118.2
                hugo version 

          - task: Npm@1
            displayName: Install npm packages
            inputs:
              command: 'install'
              workingDir: 'landing/src'

          - task: PowerShell@2
            displayName: Build hugo
            inputs:
              targetType: 'inline'
              script: |
                hugo  --logLevel info --minify --destination '$(Build.ArtifactStagingDirectory)/landing'
              workingDirectory: 'landing/src'
         
          - task: PublishPipelineArtifact@1
            displayName: Publish Artifact
            inputs:
              targetPath: '$(Build.ArtifactStagingDirectory)/landing'
              artifact: "landing"
              publishLocation: "pipeline"

  - stage: DeployToProd
    displayName: Deploy to prod
    jobs:
      - job: DeplyToProd
        displayName: Deploy to Prod
        steps:
          - task: DownloadPipelineArtifact@2
            displayName: Download landing artifact
            inputs:
              buildType: "current"
              artifactName: "landing"
              targetPath: "$(Pipeline.Workspace)"
            
          - task: FtpUpload@1
            displayName: Deploy landing
            inputs:
              credentialsOption: 'serviceEndpoint'
              serverEndpoint: 'MY_CONNECTION_NAME'
              rootDirectory: '$(Pipeline.Workspace)'
              filePatterns: '**'
              remoteDirectory: 'public_html'
              clean: false
              cleanContents: false
              preservePaths: true
              trustSSL: true
{{< / highlight >}}

The configuration above is working, you can use it, but if you want to read a bit more, stay with me a bit longer.

# Steps description

- Install hugo-extend
  - It is rather obvious. We need a specific hugo version. 
  - Chocolatey is a package manager for windows and it works :)
- Install npm packages
  - At the beginning I thought it won't be needed, but when I just build hugo without node modules it failed with error:
    -  `Error: error building site: POSTCSS: failed to transform "css/style.css" (text/css). Check your PostCSS installation; install with "npm install postcss-cli". See https://gohugo.io/hugo-pipes/postcss/: binary with name "npx" not found`
  - So I had to also install two node packages with command:
    - `npm install postcss-cli @fullhuman/postcss-purgecss`
- Build hugo
  - The main step of building hugo
  - I have added additional options:
    - `--logLevel info` to see more information in logs
    - `--minify` to have minimized output
    - `--destination '$(Build.ArtifactStagingDirectory)/landing` to push the output to a specific place
- Publish Artifact
  - To use it later
  - I know I could deploy it in that place, but I would like to have an artifact:
    - to have a possibility to download it manually to check what is inside
    - to monitor the size of the page
    - to deploy it in different stage or stages
- Download landing artifact
  - Getting the artifact
- Deploy landing
  - The last step to publish it
  - I had to use older version of the task `FtpUpload@1` instead of `FtpUpload@2`, because newer is not working for me
  - Important settings:
    - I used configured connection in devops to not keep credentials in the source code
      - I used `generic connection` for it in: `Project Settings -> Service Connections -> New Service Connection -> Generic`
    - `preservePaths: true` - It is important to set it to true, because we want to have a specific file structure 
    - `clean: false` and `cleanContents: false` 
      - I think it is better to not clean, because during the deployment the application still be available

# Would you use it? Do you have any questions?

If you have any question or suggestions? Let me know in comments section below :)

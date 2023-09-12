---
title: "How to configure CI/CD for hugo with Azure DevOps Pipelines?"
date: 2023-09-11T15:40:58+01:00
draft: true
tags: ["hugo", "azure devops", "pipelines", "yaml", "devops", "gethugothemes"]
---

When I have started the blog more than two years ago I decided to automate the deployment with GitHub Actions]({{<relref "./how-to-configure-ci-cd-for-hugo-in-practice.md" >}}). It still works, but I had a need to create another hugo page, but this time I will keep the source code in AzureDevops. So I configured deployment with Azure DevOps Pipelines and here you can find a instruction how to do it.

# My requirenments didn't disallow me the simplest solutions
I didn't start with with a pure and clean hugo page, but I use one of themes from (GetHugoThemes.com)[https://gethugothemes.com/]. So I wasn't able to use any version, but a specific one, recomened from theme creator and it was `Hugo Extended 0.113.0`.

So my guess was to use
- a ready-to-use action
  - but AzureDevos is not so open as GitHub Actions, I wasn't able to find a good ready-to-use action, because everything I have found was really old and not updated to newe hugo version.
- use a docker image
  - I have found an information on (gohugo.io)[https://gohugo.io/installation/linux/#docker] about recomended docker image, but the newest image version was `0.101.2` and it still too old for me
- apt-get
  - but the nevest version wasn't enought

## but allows me for a different option
After it I knew what I wouldn't use, so it is time to switch to working solutions.
- image `windows-latest`
- `chocolatey`to install the correct version of hugo
  - thanks to (Pauby)[https://blog.pauby.com/], hugo is maintained with the newest version


# Builing that works

Here is working pipelie configuration in yaml format.
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
            displayName: Downaload landing artifact
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

The configution above is working, you can use it, but if you want to read a bit more, stay with me a bit longer.

# Steps description

- Install hugo-extend
  - It is rather obvious. We need a specific hugo version. 
  - Chocolatey is a package manager for windows and it works :)
- Install npm packages
  - At the beginning I thought it won't be needed, but when I just build hugo without node modules it failed without
    -  `Error: error building site: POSTCSS: failed to transform "css/style.css" (text/css). Check your PostCSS installation; install with "npm install postcss-cli". See https://gohugo.io/hugo-pipes/postcss/: binary with name "npx" not found`
  - So I had to also install two node packages with command:
    - `npm install postcss-cli @fullhuman/postcss-purgecss`
- Build hugo
  - The main step of builing bugo
  - I have added additional options:
    - `--logLevel info` - to see more information in logs
    - `--minify` - to have minimalized output
    - `--destination '$(Build.ArtifactStagingDirectory)/landing` - to push the output to a specific place
- Publish Artifact
  - To use it later
  - I know I could deploy it in that place, but I would like to have an artifact:
    - to have a posibility to download it manually to check what is inside
    - to monitor the size of the application
    - to deploy it in different stage or stages
- Downaload landing artifact
  - Getting the artifact
- Deploy landing
  - The last step to publish it
  - I had to use older version of the task `FtpUpload@1` instead of `FtpUpload@2`, because newer is not working for me
  - Important settings:
    - I used configured connection in devops to not keep credentials in the source code
      - I used `generic connection`
    - `preservePaths: true` - It is importand to set it to true, because we want to have a specific file structure 
    - `clean: false` and `cleanContents: false` 
      - I think it is better to not clean, because during the deployment the appliation still be avaialble

# Would you use it?

If you have any question or suggestions? Let me know in comments section below :)







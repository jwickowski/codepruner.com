---
author: Jerzy Wickowski
categories:
  - AI
companies:
  - esatto
date: 2023-10-30T03:40:58.000Z
draft: false
images:
  - images/posts/2023/2023-10-30-what-semantic-kernel-is-and-how-to-use-it.png
tags:
  - ai
  - openai
  - Semantic Kernel
  - planner
  - pipeline
title: What is SemanticKernel how to use it?
type: trending
url: post/2023/2023-10-30-what-semantic-kernel-is-and-how-to-use-it
---

When I have started my adventure with AI I [explored the OpenAI API]({{< relref "./2023-10-11-six-things-you-must-know-before-you-start-using-OpenAI-ChatGPT.md" >}}). It worked, but during my trip, deeper and deeper I discovered there were more and more concepts. All of them are important and we need to understand and implement like history, context, prompt sequential, external API requests. Everything can be done with API and you code, but there is something like SemanticKernel that can help you with it.


### What is SemanticKernel?
It is an open-source library, created by Microsoft. You can understand it as an abstraction layer above AI API with some features to do all required work to integrate your app with AI. Creators describe it: `Semantic Kernel is an SDK that integrates Large Language Models (LLMs) like OpenAI, Azure OpenAI, and Hugging Face ... and automatically orchestrate plugins with AI`

{{< notice "info" >}}
Remember! All of the examples you can find the [CodePruner.com repository](https://github.com/jwickowski/codepruner.com/tree/master/src/codepruner.com/static/examples/CodePruner.Examples)
{{</ notice >}}

### How to create it. 1st step.

- Add Nuget `Microsoft.SemanticKernel`
  - Currently only beta is available. The version is ~~`1.0.0-beta3`~~ `1.0.0-beta5`
- Create a `KernelBuilder` to create a kernel
{{<code language="csharp" file="static/examples/CodePruner.Examples/CodePruner.Examples.AI.ExploreSemanticKernel/ExploringSemanticKernel.cs" region="create_simple_semantic_kernel" >}}
- You can also pass a console logger to the builder to see a bit more:
{{<code language="csharp" file="static/examples/CodePruner.Examples/CodePruner.Examples.AI.ExploreSemanticKernel/ExploringSemanticKernel.cs" region="create_semantic_kernel_with_console_logging" >}}

### Plugins in Sematic Kernel
The main concept present is in the SK is a plugin system. It is build with two types of plugin functions `NativeFunction` and `SemanticFunction`. Both of them are designed to extend or encapsulate a logic for specifics tasks. Let's check some details and diffrences between them.

#### SemanticFunction
It is a way to create a predefined query to AI. It is nothing else like already prepared and configured, ready to use prompt to AI. It is build with:
- Prompt
  - It is a simple text
  - It can include some placeholders to inject some values to the prompt
- Configuration - It is a JSON file with the configuration for the prompt. like:
    - Description - a description of the function. 
      - It it used for users to know the intention of the function, 
      - but also for a `planner` (we will talk about it later) to know what the function does.
    - Temperature - to set how random or predictable the response should be
    - MaxTokens - to set the length of the response
    - and more
- It can be defined inline:
{{<code language="csharp" file="static/examples/CodePruner.Examples/CodePruner.Examples.AI.ExploreSemanticKernel/ExploringSemanticKernel.cs" region="semantic_function_bike_joke_inline" >}}
As you can see in that example. When you create one `SemanticFunction` and use it in different situations. 

- or, better it can be defined in two seperate files:
  - `skprompt.txt` - a  text file with a function prompt
{{<code language="text" file="static/examples/CodePruner.Examples/CodePruner.Examples.AI.ExploreSemanticKernel/Plugins/BikePlugin/BikeJoke/skprompt.txt" >}}
  - `config.json` - configuration
{{<code language="json" file="static/examples/CodePruner.Examples/CodePruner.Examples.AI.ExploreSemanticKernel/Plugins/BikePlugin/BikeJoke/config.json" >}}
 - and here you can see the example of usage:
{{<code language="csharp" file="static/examples/CodePruner.Examples/CodePruner.Examples.AI.ExploreSemanticKernel/ExploringSemanticKernel.cs" region="semantic_function_bike_joke_files" >}}

#### NativeFunction
It is a way to create your own part of code to run it thought the kernel. It can be responsible for calculations, doing API calls, saving or loading some data. The definition can look like that:
{{<code language="csharp" file="static/examples/CodePruner.Examples/CodePruner.Examples.AI.ExploreSemanticKernel/Plugins/BikeApiPlugin.cs" >}}
and execution:
{{<code language="csharp" file="static/examples/CodePruner.Examples/CodePruner.Examples.AI.ExploreSemanticKernel/ExploringSemanticKernel.cs" region="native_function_bike_size" >}}

I know the current example doesn't have too much sense. Why did I use the kernel to execute the function if I just could call it directly from `BikeSizePlugin`?
- It is just a simple example to show you how to create `NativeFuction`.
- It will give you a bit more sense when we combine them with a pipeline. 

### Pipeline
It is a way to running multiple functions, `Semantic` or `Native` in order with passing the output from one function to the following one. To achieve it you can configure it manually or use `planner`. Let's start with the 1st option:

#### Manual pipeline configuration
You can configure how the pipeline should look like. Check the example:
{{<code language="csharp" file="static/examples/CodePruner.Examples/CodePruner.Examples.AI.ExploreSemanticKernel/ExploringSemanticKernel.cs" region="manual_pipeline" >}}

#### Planner
You can do it in a different way. It still needs to be tested for more complex examples, but let see how it works in that example: 
{{<code language="csharp" file="static/examples/CodePruner.Examples/CodePruner.Examples.AI.ExploreSemanticKernel/ExploringSemanticKernel.cs" region="sequential_planner" >}}

As you can se, we just registered plugins and used `SequentialPlanner` to prepare a pipeline for asked query. The planner can use Native or Semantic functions. Because it knows what the function does, what is the input and output. 

### Summary
Thank you that you are still here. I think you know main concepts of Semantic Kernel now. There are still some topics like: `Context` and `Memory`, but it is a topic for a different article.

Let me know if you have any questions or comments. I will be happy to answer them.

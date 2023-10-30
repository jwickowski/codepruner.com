---
title: "What SemanticKernel is and how to use it?"
author: "Jerzy Wickowski"
images:
  - "images/posts/2023/2023-10-126-XXXXXXXXXXXXXXXXXXXXXXXXXXX.jpg" 
date: 2023-10-30T04:40:58+01:00
draft: false
tags: ["ai", "openai", "chatGPT", "Semantic Kernel"]
categories: ["AI"]
type: "regular"
companies: ["esatto"]
---

When I have started my adventure with AI I started with [exploring the OpenAI API]({{< relref "./2023-10-11-six-things-you-must-know-before-you-start-using-OpenAI-ChatGPT.md" >}}). It worked, but when I went deeper I discovered that there were more and more concepts that we need to understand and implement like history, context, prompt sequential, external API requests. Everything can be done with API and you code, but there is something like SemanticKernel that can help you with it.


### What is SemanticKernel?
It is an open-source library, created by Microsoft. You can understand it as an abstraction layer above AI API with some features to do all required work to integrate you app with AI. Creators describe it:
> Semantic Kernel is an SDK that integrates Large Language Models (LLMs) like OpenAI, Azure OpenAI, and Hugging Face ... and automatically orchestrate plugins with AI


### How to create it. 1st step.

- Add Nuget `Microsoft.SemanticKernel`
  - Currently there is only beta version: `1.0.0-beta3`
- Create a `KernelBuilder` to create a kernel
{{<code language="csharp" file="static/examples/CodePruner.Examples/CodePruner.Examples.AI.ExploreSemanticKernel/ExploringSemanticKernel.cs" region="create_simple_semantic_kernel" >}}
- You can also pass a console logger for example bit more configuration like:
{{<code language="csharp" file="static/examples/CodePruner.Examples/CodePruner.Examples.AI.ExploreSemanticKernel/ExploringSemanticKernel.cs" region="create_semantic_kernel_with_console_logging" >}}


### Plugins in Sematic Kernel
The main concept present is in the SK is a plugin system. It is build with two types of plugin functions `NativeFunction` and `SemanticFunction`. Both of them are designed to extend or encapsulate a logic for specifics tasks. Let's check some details and diffrences between them.

#### SemanticFunction
It is a way to create a predefined query to AI. It is nothing else like already prepared and configured, ready to use prompt to UI. It is build with:
- Prompt
  - It is a simple text
  - It can include some placeholders to inject some values to the prompt
- Configuration - It is a JSON file with the configuration for the prompt. like:
    - Description - a description of the function. 
      - It it used for users to know the intention of the function, 
      - but also for the `planner`(we will talk about it later) to know what the function does.
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
{{<code language="csharp" file="static/examples/CodePruner.Examples/CodePruner.Examples.AI.ExploreSemanticKernel/Plugins/BikeSizePlugin.cs">}}
and execution:
{{<code language="csharp" file="static/examples/CodePruner.Examples/CodePruner.Examples.AI.ExploreSemanticKernel/ExploringSemanticKernel.cs" region="native_function_bike_size" >}}

I know the current example doesn't have too much sense, but it is just a simple example. It will give you a bit more sense when we combine them with a pipeline. 

What MS write about Sematic Kernel
- OpenSource SDK
- Semantic Kernel has been engineered to allow developers to flexibly integrate AI services into their existing apps
- A core for copilots
- A sample copilot: https://github.com/Azure-Samples/miyagi
- It is for an orchestration a copilot
- there is an alternative like: LangChain 
- inside
  - prompt and response filtering
  - metaprompt - 
- SC - uses context variables and semantic functions


Conetxt
- You can create on kernel like `var context = kernel.CreateNewContext();`
- Then you can add values to it:  like `var history = ""; context.Variables["history"] = history;`
- Then between commends you can add it like: 
  ```
 history += $"\nUser: {input}\nMelody: {answer.GetValue<string>()}\n"; 
    context.Variables["history"] = history;
    ```
- It will be added to prompt

Planner:
- How it decides which plugin should be used?
- Is the plan genereted by UI ?
- I think It can be prepared manually or it can be created by UI
-  Within Semantic Kernel, you can invoke these functions either manually (see chaining functions) or automatically with a planner.
- If you want to order them manually you can do sth like that:
```
 var output = await this._kernel.RunAsync(
        request,
        getNumbers,
        extractNumbersFromJson,
        MathFunction,
        createResponse
    );
```

- SemanticFunction
 - is is a way to probmpt
   - it has a defined bechavior
   - with parameters in format {{VARIABLE_NAME}}
   - it has defined parameters like maxTokens, Temperatre, TopP
- Within a plugin, you can create two types of functions: semantic functions and native functions. 
- At its core, a semantic function is just a prompt that is sent to an AI service along with any additional settings the AI service requires.

- SemanticFunction is like predefined prompt with:
  - with: 
    - skprompt.txt => prompt
      - It can be with parameters
    - config.json  -> a configuration how to use it
      - but here are parameters too


- Native - it is a part of code 
  - They can be used to 
    - save data
    - retrieve data
    - and perform any other operation that you can do in code
    - do math, because LLM is not good in math :)
    - add argument [SKFunction ]


Example:
  - Before everything:
    - Populate `memory` with your context data
  - Executing (a pipeline)
    - Get user data context with NativeFunction (do a request for example)
    - Add domaincontext with `recall` from with `TextMemoryPlugin`. 
    - Then make it a bit shorter for example with a specific semantic function
  - Get user query
  - prapare a bit of prompt
  - create a pipeline



Memories
- a way to broader context for your ask
- we can fed it. There are three the most populaor ways:
 - Conventional key-value pairs
 - Conventional local-storage
 - Semantic memory search
- Q: Should I use it only for the user history or for the whole context?
  - Q: Is there a different place to store data?
- It is jus an abstraction over the persisttation layer

SemanticFunction is important part of SematicKernel

Plugin:
 - containes functions, semantic or native
 - probably is is in the same format as OpenAI plugin => https://learn.microsoft.com/en-us/semantic-kernel/ai-orchestration/plugins/chatgpt-plugins
 - to transform NAtivePluagin to chatGPT plugin you should change methotds to Http endpoints
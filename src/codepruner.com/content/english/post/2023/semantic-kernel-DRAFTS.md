---
author: Jerzy Wickowski
categories:
  - AI
companies:
  - esatto
date: 2023-11-01T03:40:58.000Z
draft: true
images:
  - images/posts/2023/2023-10-126-XXXXXXXXXXXXXXXXXXXXXXXXXXX.jpg
tags:
  - ai
  - openai
  - chatGPT
  - Semantic Kernel
title: Context and Memory in SemanticKernel?
type: regular
url: post/2023/semantic-kernel-DRAFTS
---


IT is a topic to write.
Currently it isnt a draft. It is only some notes to work with.


### Context
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
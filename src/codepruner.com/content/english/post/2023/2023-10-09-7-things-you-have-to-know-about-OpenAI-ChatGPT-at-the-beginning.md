---
title: "2023-10-09-7-things-you-have-to-know-about-OpenAI-ChatGPT-at-the-beginning"
author: "Jerzy Wickowski"
images:
  - "images/posts/2023/2023-10-09-7-things-you-have-to-know-about-OpenAI-ChatGPT-at-the-beginning.jpg" 
date: 2023-10-09T04:40:58+01:00
draft: true
tags: ["ai", "openai", "chatGPT", "api", "temperature"]
categories: ["AI"]
type: "regular"
companies: ["esatto"]
---

ChatGPT it great tool. You can test it's capability on [webpage chat.openai.com](https://chat.openai.com/). It will answer your question, suggest a solution, write a bit code for you or write a nice post to Twitter(x) :). But if you limit your usage of that tool only to the chat, it will be only a toy. Nice, technological advanced, but only a toy. To discover to full potential you have to use it as an API. In this article I will show 7 things you should know about ChatGPT API before you start using it..

### 1. API models
There are multiple models available. They are specialized in different scenarios. They have different capabilities and different prices. Moreover every model has multiple versions. You can find the full list of them [here](https://platform.openai.com/docs/models). For example:
  - `gpt-3.5-turbo` - The most popular model. It is something you should use at the beginning. It has the balance between price and skills.
  - `gpt-4` - It is more advanced model. OpenAI describes that it "can solve difficult problems with greater accuracy". It is also 10x more expensive then `gpt-3.5-turbo`.

There are also more specialized models to use them im more specific scenarios like:
  - `text-embedding-ada-002` - It is useful if you would like to prepare data for your own model
  - `DALL·E` - a model to generate images from text or manipulate existing images
  - `Whisper` - a model to recognize speech

#### Example with region: 
{{<code language="csharp" file="/static/examples/CodePruner.Examples/CodePruner.Examples.AI.ChatGPT/ModelsTests.cs" region="ask_about_capitols_in_europe" >}}


### 2. Fill the temperature
When you do a first 

2. Roles, how it will behave
3. Plans
  - Different result
  - different price
4. No history. You have to keep it and send it every time, context
5. Lean how to prompt (with roles)
6. function calling
  - it will not call a function 
    - you need to define a function
    - then ain a response can be an information to call a function
    - then you can map it on your side 
    - and call a function
7. ?????

3. Tokens
4. Function calling
5. Prompts
6. Parameters
7. 
8. API
9. History
10. Azure API
11. Train my own model

- to start working with API you need to generate a access key
- plans:
  - they recommend to use:
     - gpt-3.5-turbo-instruct
     - gpt-3.5-turbo
     - basuese they are cheaper
  - but there are different models and they are specialized in different scenatios
- you can define different roler
  - user: it is you what you ask (do a prompt)
  - system: you describe how the api should behave. 
      - it sets the bahavior of assistant
      - examples:
         - You will find keywords in the article and return them in json format with information about the frequent
         - You will be helpful asistant
         - You will find the most atractive 2 sentences in the article

  - asistant: it is answering to you 
      - (or and examle answer to e able to give you a simillar answer in the future)
  - api has no history, so you have to store it by yourself and send every time  in a message content
- api. They suggest to use [azure api for .net](https://github.com/Azure/azure-sdk-for-net/tree/main/sdk/openai/Azure.AI.OpenAI). It should be compatible with openAI api.

- there is an option to train my own model with chatGPT => https://platform.openai.com/docs/guides/fine-tuning
- As I understand it is important how to do `prompts` to get what we want. It is a type 
- parameters:
   - temperature - what is the probability of choosing the next token. The higher the value, the more random the result will be. The lower the value, the more predictable the result will be.
       - It is beeter to get smaller value if you want ot have a stable answer
       - IT is better to have higher value if you want to have a creative or variety answer
- tokens, as i understand 
  - all of input/output is tokenized. eg. splitted into smaller parts. 
  - there is a probability what the next tokem will be

- 
    
<!--more-->
TO do it you need following steps:
- create an account on OpenAI
- create an API key
- install OpenAI .NET library
- use it in your application

to use it in your application you need to:
- create a new project
- add OpenAI .NET library
- add a reference to the library
- add a using statement
- create a new instance of the OpenAI class
- set the API key
- set the engine
- set the prompt
- set the temperature
- set the max tokens
- set the top p
- set the frequency penalty
- set the presence penalty
- set the stop sequence
- call the Complete method
- display the result
- run the application
- check the result


with code samples it will looke like:
```csharp
using OpenAI;

// do a request to opensi
var openAi = new OpenAIClient("your api key");
var result = openAi.Complete(engine: Engine.Davinci, prompt: "Once upon a time", maxTokens: 5);
Console.WriteLine(result);
```

to add there your own data you need to write:
```csharp
using OpenAI;

// do a request to opensi
var openAi = new OpenAIClient("your api key");
var result = openAi.Complete(engine: Engine.Davinci, prompt: "Once upon a time", maxTokens: 5, stop: new[] { "\n" }, temperature: 0.7, topP: 1, frequencyPenalty: 0, presencePenalty: 0);
Console.WriteLine(result);
```




Open AI Embeddings - process to see text as a nubmer


### How to fill AI with my owkn data
- I can train my own model
- I can use in-context (so add the text in the prompt)
- RAG ?????
  - embeding... 
- I can use a fine-tuning ?????
 



 ### locally
 - https://ollama.ai 
 - use uncenzored data
 
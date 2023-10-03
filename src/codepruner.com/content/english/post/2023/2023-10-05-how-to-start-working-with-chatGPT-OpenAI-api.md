---
title: "How to distinguish types of AI?"
author: "Jerzy Wickowski"
images:
  - "images/posts/2023/2023-10-05-how-to-start-working-with-chatGPT-OpenAI-api.jpg" 
date: 2023-10-05T04:40:58+01:00
draft: true
tags: ["ai", "openai", "chatbot", "chatGPT", "api"]
categories: ["AI"]
type: "regular"
companies: ["esatto"]
---

Is ChatGPT only a web page where you can ask some question? Of course not. It's also an API. You can use it in your own application to enhance your app with AI features. If you are interested in how to do it, grab a mug of tea and read this article.

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



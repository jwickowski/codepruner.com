---
author: Jerzy Wickowski
categories:
  - AI
companies:
  - esatto
date: 2023-10-09T03:40:58.000Z
draft: false
images:
  - >-
    images/posts/2023/2023-10-11-six-things-you-must-know-before-you-start-using-OpenAI-ChatGPT.jpg
tags:
  - ai
  - openai
  - chatGPT
  - api
  - temperature
title: 6 things you must know before you start using OpenAI ChatGPT
type: trending
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

Just keep it in mind that there are more options to more complex or different scenarios.

### 2. Feel the temperature
When you use ChatGPT UI you have only a chat. You can ask a question and see the an answer. You have not option to pass more parameters and there is one really important parameter you should know. It is called `temperature`.  By adjusting `temperature` you can modify how random the answer will be. For example if you want to have analyze something you should set tempreture as low value, but you need more creativity then the value should be higher.

#### Application name examples
> Can you provide me 3 short fancy names for an application to calculate boring reports?

{{< tabs "application_name" >}}
{{< tab "Temp: 0" >}}
```
1. Elysian Analytics
2. Opulent Insights
3. Regal Reports
```
{{</ tab >}}
{{< tab "Temp: 1" >}}
```
1. Elysian Analytics
2. LuxeMetrics
3. Opulent Insights
```
{{< /tab>}}
{{< tab "Temp: 2" >}}
```
1. Eoplectify
2. Exancitus Veˈdonderovalesh
3. Spectrofuture Analytics
```
{{< /tab>}}
{{</ tabs >}}

I know it is not very useful example, but you can see the difference between. It is great trick to unlock your brain with some ideas or generate a name for something new in an application. 

#### Twitter post examples
>  Write a attractive Twitter post about AI in bookkeeping?
It should be 5 sentences long and there should be references music bands.
Use rhymes.

{{< tabs "twitter_post_example" >}}
{{< tab "Temp: 0" >}}
```
🎵 Move over, old-school bookkeeping, AI is here to rock the charts! 📚🎸 Embrace the future of financial management with #AI-powered bookkeeping, where accuracy and efficiency harmonize like never before. 🤘📊 Say goodbye to tedious number crunching and hello to a symphony of automated processes that'll make your books sing! 🎶📚 Let AI be your backstage crew, freeing up time for you to jam with your favorite music bands. 🎵🎉 #BookkeepingRevolution #RockingWithAI
```
{{</ tab >}}
{{< tab "Temp: 1" >}}
```
🎵 Groove to the future and let AI do the math - a bookkeeping revolution in your path! 📚💻✨ With AI technology, accuracy is tight, freeing your time to rock the mic!🔥🎶 Say goodbye to paper piles, 'cause AI's got the style! 📊💯 From AC/DC to Led Zeppelin vibes, AI ensures bookkeeping thrives! 🎸📝 Let the rhythm of automation and efficiency play, while AI keeps your numbers in perfect sway! 🤘💫 #AIBookkeeping #RockingTheBooks
```
{{< /tab>}}
{{< tab "Temp: 1.8" >}}
```
🎵🤖who ushered AI, now on bookkeeping stage? 📚 Listen up, your off-key numerals are pages locked stage;
Celebrate revolutions align, pixels to lend--breferences evolved Souls layer undeflooralready
```
{{< /tab>}}
{{</ tabs >}}

Difference is huge, isn't it? Mostly when you see answer with really high temperature `1.8`, in that case it is not even a text, but a set of words, but maybe in different scenarios it can be helpful. When I discover that scenarios I will let you know. Maybe you know good examples of it? Leave a comment with them.

### 3. Roles, the chat behavior
It is very important part of the API. Using roles you will have power to define how the chat will behave. There are three main roles:
- user
  - It the the input you will provide to the chat. 
  - It it the main prompt you send to the chat.
  - You use that role when you use the UI.
- assistant
  - It is the output of the chat.
  - It is the answer you will get from the chat.
- system
  - It is the behavior of the chat or rather the behavior of the assistant.
  - It gives you a huge power to make it much more useful.
  - examples:
    - You will find keywords in the article and return them in JSON format with information about the frequent
    - You will be helpful assistant
    - You will find the most attractive 2 sentences in the article

#### Some useful examples of system role
{{< notice "info" >}}
All of the examples you can find the [CodePruner.com repository](https://github.com/jwickowski/codepruner.com/tree/master/src/codepruner.com/static/examples/CodePruner.Examples)
{{</ notice >}}

When we ask a detailed question and when Chat doesn't know the answer then it will hallucinate. It means the chat will lie to you.
{{<code language="csharp" file="static/examples/CodePruner.Examples/CodePruner.Examples.AI.ChatGPT/SystemRoleTests.cs" region="default_role_jerzy_wickowski_in_desperation" >}}

but you can define the system role to tell only the truth, then it will be more useful in real life scenarios. 
{{<code language="csharp" file="static/examples/CodePruner.Examples/CodePruner.Examples.AI.ChatGPT/SystemRoleTests.cs" region="honest_role_jerzy_wickowski_in_desperation" >}}

### 4. There it no history
When you use ChatGPT UI you can see the history of the chat. You can see what you asked and what was the answer. Moreover the UI keeps the history as a context. So following asnwers make sense with the previous messages. When you use API every request is independent, so you have to store the history yourself. How to do it? You have to keep the history and pass it in next requests. I think it will looks better with examples:

{{<code language="csharp" file="static/examples/CodePruner.Examples/CodePruner.Examples.AI.ChatGPT/HistoryTests.cs" region="lose_the_context" >}}
Here is the proof it doesn't keep the history, but we can implement it in different way. Take a look:

{{<code language="csharp" file="static/examples/CodePruner.Examples/CodePruner.Examples.AI.ChatGPT/HistoryTests.cs" region="keep_the_context" >}}
As you can see the continuation is kept. So if you want to keep the context you can do it like that. Moreover you can pass prepare the examples yourself to pass to the AI the context you wish. No one will stop you from doing things like that:

{{<code language="csharp" file="static/examples/CodePruner.Examples/CodePruner.Examples.AI.ChatGPT/HistoryTests.cs" region="prepare_the_context" >}}
Of course in real life you can add to the context better information about your app, your company, your product or user context. We will do more logic experiments in further posts.


### 5. Lean how to prompt
If you want to use the tool in real life you must learn how to create prompts. Then you will gets wanted results. Let assume you want to create a regex to find leading zeros from a number. 
- There are some requirements:
  - If zero is at the beginning then is should be find
  - If there is comma after zero then is should be ignored
- Prompt examples:
  - Simple question
    - prompt: "What is regex to remove leading zeros"
    - answer: `^0+`
    - test samples: 
      - 000231 => 231 - OK
      - 000,231 => 000,231 - Not OK
    - It is a simple answer for simple question. We didn't pass all out requirements. Let's try to so it.
  - Simple Question with requirements:
    - prompt: "What is regex to remove leading zeros. If zero is at the beginning then is should be find. If there is comma after zero then is should be ignored."
    - answer: `^(0+)(?![,])`
    - test samples:
      - 00123 => 123 - OK
      - 00,123 => 0,123 - OK
  - You can do it by passing examples:
    - prompt: What is regex to remove leading zeros. Examples: 0001 => 1; 00123 => 123; 0,12 => 0,12; 00000,423 => 0,423;
    - answer: `^0+(?=\d)`
    - test samples: 
      - 00123 => 123 - OK
      - 00,123 => 0,123 - OK

#### You can also define the answer format like here:
{{<code language="csharp" file="static/examples/CodePruner.Examples/CodePruner.Examples.AI.ChatGPT/ModelsTests.cs" region="ask_about_capitols_in_europe_1" >}}

but... to be honest it is a nice feature as a chat or Google alternative. If you would like to make it more useful from developer perspective you can modify the prompt to return th data in JSON format for examle. It can look like that:

{{<code language="csharp" file="static/examples/CodePruner.Examples/CodePruner.Examples.AI.ChatGPT/ModelsTests.cs" region="ask_about_capitols_in_europe_json" >}}

and... now it is more useful, because answer like that is easy to parse and process in a further steps. You still have to be careful, because in example like that there are no information about the unit. You cannot be sure if the are is in square kilometers or miles. The same with population. It can be millions or thousands. So it is always better to be more strict when you ask about data like that and you can see it in the next example.

{{<code language="csharp" file="static/examples/CodePruner.Examples/CodePruner.Examples.AI.ChatGPT/ModelsTests.cs" region="ask_about_capitols_in_europe_with_strict_units_json" >}}

### 6. How to pass your data to ChatGPT
There is no dedicated way to do it. You are not able to train chat GPT with your own data. But there is a way to allow it working in your context. You just need to add the information to the prompt. Of course is it limited, but let's see how it works:

When there is no context it won't be able to tell you anything:
{{<code language="csharp" file="static/examples/CodePruner.Examples/CodePruner.Examples.AI.ChatGPT/ContextCinemaTests.cs" region="prompt_without_context" >}}

You you can add there some context. To allow answering it with more sense:

{{<code language="csharp" file="static/examples/CodePruner.Examples/CodePruner.Examples.AI.ChatGPT/ContextCinemaTests.cs" region="prompt_with_context" >}}

Of course it is a simple data you can make it more complex. It depends on your needs. There are more ways to prepare the context, but I will show you them in the next posts.


### Summary
Thank you for reading the article. I hope you know what you came here. If no, let me know what you are looking for and I will try to help you with that.




 


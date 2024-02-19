---
author: Jerzy Wickowski
categories:
  - AI
date: 2024-02-19T18:40:58.000Z
disqus_identifier: how-to-configure-indexing-files-from-blob-in-azure-ai-search
disqus_title: How to give AI knowledge about your business?
disqus_url: 'https://codepruner.com/how-to-give-ai-knowledge-about-your-business'
draft: false
images:
  - images/posts/2024/2024-02-19-how-to-give-AI-knowledge-about-your-business.jpg
tags:
  - ai
  - openai
  - chatGPT
  - azure
  - search
title: How to give AI knowledge about your business?
type: trending
url: how-to-give-ai-knowledge-about-your-business
---

How to give AI knowleadge about your business?

Because of OpenAI and them ChatGPT AI is more and more popular. You can use it as a nice toy and ask it for some simple and fun things. It can also help you with some of your creative work like generating ideas, writing drafts of posts to social media.

Even if it can generate you a content, it doesn't know you and your business. It won't know your context, but you can help it to have it. There are couple of ways to extend LLM with required knowledge. Let's check them.

## Extending Context
When you send a prompt to AI you can add more information to it. You can ask a question without passing no data like:
```
Who is Jerzy Wickowski?
```
but probably you get an answer similar to this:
```
As of my last knowledge update in January 2022, there isn't significant information available about an individual named Jerzy Wickowski in widely recognized contexts or public domains. It's possible that Jerzy Wickowski might be a private individual, a person of local significance, or associated with specific communities or industries that might not have gained widespread attention or documentation up to that point.

If Jerzy Wickowski has become notable or gained prominence after January 2022, I wouldn't have information about those developments.
```
and it makes sense, because I am not very popular person, maybe it will change in the future, but for the current momoent in time the answer is fine for me.

You can also pass a bit of information to the prompt to allow an AI to have some knowledge. The prompt can look like that:
```
Who is Jerzy Wickowski?

### Additional Information
Jerzy Wickowski is a software developer.
Jerzy Wickowski is from Poland.
Jerzy is a software developer woth 15 years of experience.
Wickowski is an author on CodePruner.com.
```
the answer can look like that:

```
Based on the information you provided, Jerzy Wickowski is a software developer from Poland with 15 years of experience. Additionally, Wickowski is noted as an author on CodePruner.com, which suggests that he might have contributed articles, tutorials, or content related to software development on that platform.
```

AS you can see, if we provide the information to the context, then AI will be able to answer a specific question, but you will not be able to add all information from hundreds of company documents. To achieve it we can go to he next part 

## RAG - Retrieval Augmented Generation
This approach uses the previous one, but give you a way more option and allows to to work with a much bigger of data. 

The main idea is to index you data somewhere and before creating a prompt, you can ask for a specific data from the index. Then you can add the result to the prompt. I will describe the whole process in one of the next posts, but in a nutshell it looks like that:

1. Index all of the files in an index 
2. When user asks for anything
3. Ask for a specific data/document from the index
4. Add the result to the AI prompt
5. Send the prompt with user Query and found data to the AI
6. Get the answer

## Plugins
The idea of plugins is fantastic. You create your own API and you prepare a Plugin Manifest for the AI in specific format. Then when you work with AI, it discover your intention and call your plugin/your API to do a specific action. It can be getting data from your source, but also it can be an invocation of a specific action.

How the AI know what method in plugin it should call? I have no idea, but if you want to know, let me know in the comments and I will investigate it.

## Fine tuning
Another option is an adjusting a model to your needs. It is one of the most media method. When you think that you can adjust the model to your needs, you can think that it will be the best option. 

Before you decide to go that way make sure:
- You have tried the previous, simpler, methods and you are sure they are not enough
- Read how to do FineTunning well
- Be aware that it can be a long and expensive process


## Summary
Would you like to know anything more about it? Let me know in the comments. I will be happy to talk with you.
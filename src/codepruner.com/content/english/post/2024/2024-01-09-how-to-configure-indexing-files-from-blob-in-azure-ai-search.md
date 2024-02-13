---
author: Jerzy Wickowski
categories:
  - AI
companies:
  - esatto
date: 2023-01-04T03:40:58.000Z
disqus_identifier: >-
  post/2024/2024-01-09-how-to-configure-indexing-files-from-blob-in-azure-ai-search
disqus_title: How to configure indexing files from blob in azure AI search
disqus_url: >-
  post/2024/2024-01-09-how-to-configure-indexing-files-from-blob-in-azure-ai-search
draft: false
images:
  - >-
    images/posts/2024/2024-01-09-how-to-configure-indexing-files-from-blob-in-azure-ai-search.jpg
tags:
  - ai
  - openai
  - chatGPT
  - azure
  - search
title: How to configure indexing files from blob in azure AI search
type: trending
url: >-
  post/2024/2024-01-09-how-to-configure-indexing-files-from-blob-in-azure-ai-search
---

How to give AI knowleadge about your business?

Because of OpenAI and them ChatGPT AI is more and more popular. You can use it as a nice toy and ask it for some simple and fun things. It can also help you with some of your creative work like generating ideas, writing drafts of posts to social media like Facebook, LinkedIn or X. 

But it will generate you only generic content. It doesn't know you and your business. It won't know your context, but you can help it to have it. There are couple of ways to extend LLM with required knowledge. Let's check them.


# Extending Context
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

AS you can see, if we provide the information to the context, then AI will be able to answer a specific question, but you will not be able to add all information from hundreds of company documents. To acheive it we can go to he next part 

# RAG - Retrieval Augmented Generation
This approach is a bit similar to the previous one, but you have to add a bit of logic before adding data to the context.

You 


# Plugin

# Fine tuning

# Build your specific model


AI gives as a huge amount of possibilities to improve things we do. One of them could be generating responses based on documents we have, but how to achieve it?


# How should it work
We will go a bit deeper with that, but the main plan is:
- Index all of the files in a search
- When user asks for anything
- Ask for a specific data/document in search
- Add the result to the AI promt

That approach is called RAG: Retrieval Augemented Generation, but we will go step by step  
the main idea is to index all of the files. Then when a user asks a specific question, then 





POSTY:
- Jak to ogólnie ogarnac (mozeżę być sharepoint, a moze byc blob, po prostu datasource)
- Jak Podejść do tematu, gdy musimy np. przetłumaczyć pliki i coś z nich wyciagnąć (skillset)
- Osobno, jak skonfigurować skillset, aby zrobić split -> translate -> merge 
- Jak zrobić to przy pomocy asystentów w OpenAI (i jakie to ma wady/zalaty)
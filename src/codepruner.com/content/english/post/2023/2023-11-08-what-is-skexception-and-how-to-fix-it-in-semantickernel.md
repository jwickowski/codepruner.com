---
author: Jerzy Wickowski
categories:
  - AI
companies:
  - esatto
date: 2023-11-08T03:40:58.000Z
disqus_identifier: post/2023/2023-11-08-what-is-skexception-and-how-to-fix-it-in-semantickernel
disqus_title: >-
  How to fix SKException: Function not available _GLOBAL_FUNCTIONS_.input from
  SemanticKernel?
disqus_url: >-
  https://codepruner.com/post/2023/2023-11-08-what-is-skexception-and-how-to-fix-it-in-semantickernel
draft: false
images:
  - >-
    images/posts/2023/2023-11-08-what_is_skexception_and_how_to_fix_it_in_semantickernel.jpg
tags:
  - ai
  - openai
  - Semantic Kernel
  - Exception
title: >-
  How to fix SKException: Function not available _GLOBAL_FUNCTIONS_.input from
  SemanticKernel?
type: regular
url: post/2023/2023-11-08-what-is-skexception-and-how-to-fix-it-in-semantickernel
---

When I have created my 1st or 2nd semantic plugin for SemanticKernel I have got an exception: `SKException` with message`Function not available _GLOBAL_FUNCTIONS_.input`. After a bit of investigation I have discovered the issue. Here you can read how to reproduce that bug and how to fix it.

### How to reproduce the issue?
The most important thing to fix any issue is to find a way to reproduce it. So let's start with it. 
{{<code language="csharp" file="static/examples/CodePruner.Examples/CodePruner.Examples.AI.ExploreSemanticKernel/ExploringSemanticKernel.cs" region="throw_SKException_Function_not_available_GLOBAL_FUNCTIONS_input" >}}

As you can see it throws an exception. You can try it by yourself, because the example is in [CodePruner.com GitHub repository](https://github.com/jwickowski/codepruner.com). 


### How to fix it?
The exception is thrown because the variable `input` is used in wrong way. You can see it is `{{input}}` in above example. To make it right you should add a dollar sign `$` to it. The you should have `{{$input}}`.  Here is working example:
{{<code language="csharp" file="static/examples/CodePruner.Examples/CodePruner.Examples.AI.ExploreSemanticKernel/ExploringSemanticKernel.cs" region="fix_throw_SKException_Function_not_available_GLOBAL_FUNCTIONS_input" >}}

### Summary
As you can notice there is a way to invoke a function from prompt when you omit dollar sign `$`, but it is a story for a different article.

Thank you for being here. I hope I fixed your problem. If you have any questions or comments, please leave them below. 


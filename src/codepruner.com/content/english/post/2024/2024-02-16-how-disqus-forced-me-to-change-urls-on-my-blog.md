---
author: Jerzy Wickowski
categories:
  - blog
date: 2024-02-16T11:40:58.000Z
disqus_identifier: how-disqus-forced-me-to-change-urls-on-my-blog
disqus_title: How Disqus forced me to change URLs on CodePruner.com
disqus_url: 'https://codepruner.com/how-disqus-forced-me-to-change-urls-on-my-blog'
draft: false
images:
  - images/posts/2024/2024-02-16-how-disqus-forced-me-to-change-urls-on-my-blog.jpg
tags:
  - blog
  - comments
  - disqus
title: How Disqus forced me to change URLs on CodePruner.com
type: regular
url: how-disqus-forced-me-to-change-urls-on-my-blog
---

When I started that blog I had created an account on Disqus to have a nice communication with you. It was easy to add, but it didn't work as it should. I was so irritated, that I wanted to throw it away. But here is a short story about. 

## Adding disqus to GoHugo
Added Disqus to a blog is was easy. I just added a shortcode to GoHugo configuration and it started working. It looks like:
```toml
[services.disqus]
shortname = 'codepruner-com'
```

or 
```toml
disqusShortname = "codepruner-com"
```
 in older version.

 But I had problem with...

## Comments duplication
I noticed that the same comments are displayed on multiple pages. I know it can be useful in same cases, eg. when you have the same post with multiple addresses. Unfortunately it wasn't my case. I was able to see the same comments on different posts. So articles about `Blazor` were the same comments as article about `Semantic Kernel` and vice versa. It was irritating for me and for you. I started digging.

I had three thoughts about it:
1. I did something wrong during the configuration, so I should check it.
2. I want to write new articles to the blog, not fight with comments! 
3. I don't want to fight with tools

## The investigation and the issue
I started the investigation from my blog configuration. 
- I checked Disqus shortcode. 
- I compared the configuration with GoHugo documentation. 
- I check the generated pages and compared it to different blogs with Disqus.

There were nothing wrong! Comments were still duplicated....

I didn't stop my analyzing and I have found and an information that every post should have defined `this.identifier` and `this.url` in Disqus initialization script. I was very happy when I discovered that GoHugo supports it. So I have created a bit of code to add `disqus_identifier`, `disqus_url`, `disqus_title` to FrontMatter for every post. Then I noticed that correct values were added to pages like:

```js
  this.page.identifier="how-to-run-blazor-wasm-app-in-container";
  this.page.title="How to run blazor app in docker container";
  this.page.url="https://codepruner.com/post/2023/2023-09-02-how-to-run-blazor-wasm-app-in-container/"
```

... but it didn't help, and comments were still duplicated....

I went deeper, and analyzed it. Disqus creates new thread when you comment field is rendered for the 1st time. Then `thread` is created. It has assigned `Link` value, with an address to page with comments. So far, everything makes sense, but I had no idea why Disqus cut my URL `https://codeprucer.com/post/2022`. Then every post with the same the beginning of URL was assigned with the same thread. 

YEY! I found the issue. Now, I just inform Disqus support about that issue and wait for the solution, right? 

## Disqus support answer
No, they didn't help me. They said me:
- I have to set `this.identifier` and `this.url`. - I did it, and it didn't help.
- I have to  update the Link to a correct one with Url Migration Tool. - I understand, I can do it once, but I don't want to do it every time after publishing a new post. I want to focus on writing new articles, not fighting with comments.
- I have to clean `Identifiers` for every Thread every time after publish new post. - The same as above, I can, but I don't.

They didn't help me, but I have found a workaround. 

## The real solution (workaround)
I had urls like: 
`https://codepruner.com/post/2023/2023-09-02-how-to-run-blazor-wasm-app-in-container/`
`https://codepruner.com/post/2023/2023-10-11-six-things-you-must-know-before-you-start-using-openai-chatgpt/`

but all of them were recognized as `https://codeprucer.com/post/2023` by Disqus. It looks that Disqus cuts the url after 3rd `/`. I can't see any sense with that, but maybe it is good idea to remove dated from url to make them easer to read.

So I decided to simplify URLs and remove date from them. I did it and it looks that now, Disqus can handle it.

Moreover I wrote a bit of code to update FrontMatter in all my posts to keep the old URLs working. Now you can find the solution in [CodePruner's GitHub repository](https://github.com/jwickowski/codepruner.com/blob/master/src/tools/updateFrontMatter.ts).


## Alternatives
If you had similar issue like me, but you don't want to change your URLs, you can try to use different comment service. If I decided to change I would select one of listed below:
- [FastComments](https://fastcomments.com/) - It looks nice, I would need to compare it with Hyvor
- [Hyvor](https://talk.hyvor.com/)- It also looks nice, I would need to compare it with FastComments

Moreover, https://commento.io/ looks really nice, but their support doesn't work, because they didn't answer on my email with a simple question and they did last change in their GitHub 2 years ago.

## Summary
Did you have the same adventure with Disqus? Can you suggest any comment tool for static blogs?

Just leave a comment. :)


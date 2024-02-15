---
author: Jerzy Wickowski
categories:
  - blog
date: 2023-02-14T03:40:58.000Z
disqus_identifier: why-i-stopped-using-disqus-and-switched-to-anything-else
disqus_title: why-i-stopped-using-disqus-and-switched-to-anything-else
disqus_url: >-
  https://codepruner.com/why-i-stopped-using-disqus-and-switched-to-anything-else
draft: true
images:
  - >-
    images/posts/2024/2024-02-14-why-i-stopped-using-disqus-and-switched-to-anything-else.jpg
tags:
  - blog
  - comments
  - disqus
  - commento
  - search
title: why-i-stopped-using-disqus-and-switched-to-anything-else
type: regular
url: why-i-stopped-using-disqus-and-switched-to-anything-else
---

When I started that blog I have created an account on Disqus and added it to the blog. It was easy and it worked, mostly because it come out of the box with Hugo. 
I am going to stop using Disqus and switch to anything else. Why? Because it sucks, at least for me. Allow me to explain.

# Comment duplication
I noticed that the same comments are displayed on multiple pages. I know it can be useful in same cases, eg. when you have the same post with multiple addresses. Unfortunately it's not my case. I was able to see the same comments on different posts. So above post about Blazor were comments about Semantic Kernel and vice versa. It was irritating for me and I for you. I started digging.
I had two thought about it:
1. Probably I did something wrong during the configuration, so I should check it.
2. I want to write new articles to the blog, not fight with comments! 

# The investigation and the issue
I started the investigation from my blog configuration. I checked Disqus shortcode. I compared the configuration with GoHugo documentation. Moreover I check the generated pages and compared it to different pages with disqus. There were nothing wrong. 

Comments were still duplicated....

I didn't stop my analyzing. I googled a bit I asked Disqus support to help me solve that issue. 

I have found and information that I should add `this.identifier` and `this.url` to Disqus initialisation script. I was verry happy when I discovered that GoHugo supports it. So I have created a bit of code to add `disqus_identifier`, `disqus_url`, `disqus_title` to FrontMatter of every post.  Then I noticed that correct values were added to pages. It is good. It looks I did everything what I should.  , , to the Disqus shortcode. I did it, and I also added a comment to the shortcode:

<!-- ```html
{{ if .Params.disqus_identifier }}
    <div id="disqus_thread"></div>
    <script>
        var disqus_config = function () {
            this.page.identifier = '{{ .Params.disqus_identifier }}';
            this.page.url = '{{ .Permalink }}';
        };
        (function() {
            var d = document, s = d.createElement('script');
            s.src = 'https://codepruner.disqus.com/embed.js';
            s.setAttribute('data-timestamp', +new Date());
            (d.head || d.body).appendChild(s);
        })();
    </script>
    <noscript>Please enable JavaScript to view the <a href="https://disqus.com/?ref_noscript">comments powered by Disqus.</a></noscript>
{{ end }}
``` -->
// Story about this.identfier and this.urls and that is has sense

... but it didn't help, and comments were still duplicated....

I go deeper, and analyzed it with disquss, that is created new thread when there is a 1ft post or visit on a page and there is a column `Link`, with an address to page with comments. I have no idea why Disquss cut that url to `https://codeprucer.com/post/2022`. Then every post that started with the same address was add to the same thread. 


YEY! I found the issue. Now, I just inform Disqus support about that issue and wait for the solution, right? 

# Disqus support answer

No, they didn't help me. They said me:
- I have to set `this.identifier` and `this.url`. - I did it, and it didn't help.
- I have to  update the Link to a correct one with Url Migration Tool. - I understand, I can do it once, but I don't want to do it every time after publihsig a new post. I want to focus on writing new articles, not fighting with comments.
- I have to clean `Identifiers` for every Thread every time after publish new post. - the same as above, I can, but I don't.


# The real solution
I decided to stop using Disqus and switch to something else. I am in the middle of searching new tool, but currently there are three  nice options:
- https://fastcomments.com/
- https://commento.io/
- https://talk.hyvor.com/


I am still not sure what use, but when I do an investigation and when I use new comment system, I will let you know. 

Did you have the same adventure with disqus? Can you suggest any comment tool for static blogs?





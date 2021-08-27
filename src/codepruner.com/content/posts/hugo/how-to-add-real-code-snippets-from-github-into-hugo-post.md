---
title: "How to add real code snippets from github into hugo post"
date: 2021-08-27T6:40:58+01:00
draft: true
tags: ["hugo", "blog", "wordpress", "code", "github"]

---

# It is a technical blog
So there is a natural thing that code should be included into posts. I didn't want to embed the code just in pst content. I wanted to to it better:
- I want to inject a real code from working project. Then I am sure that the code is compiling and that it will be updated in case of some errors.
- I want to have only one source of true. I don't wont to copy the code manually from a repo to post. Because I have manual, stupid, boring job like that.
- I wanted to inject to code in the post. Hugo is static page generator so I wanted to have injected static code, on build.


# I tried multiple things
but they didn't work (or I did them wrong)
like: 
- trying to inject a file from higher level than hugo root folder. I was not able to do this
- I thought about copying the code before build and then including them

but all of that solutions were bad and not good. So I had an idea to get the code from external source like github and I have found a [github shortcode](https://github.com/haideralipunjabi/hugo-shortcodes) and I decided to do something like that.

# My implementation of github shortcode for Hugo




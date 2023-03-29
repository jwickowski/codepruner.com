---
title: "How to add real code snippets from github into hugo post"
date: 2021-08-30T6:40:58+01:00
draft: false
tags: ["hugo", "blog", "wordpress", "code", "github"]
---

# It is a technical blog

So it is a natural thing that code should be included into posts. I didn't want to embed the code just in post content. I wanted to to it better:

- I want to inject a real code from working project. Then I am sure that the code is compiling and that it will be updated in case of some errors.
- I want to have only one source of true. I don't wont to copy the code manually from a repo to post. Because I have manual, stupid, boring job like that.
- I am a big fan of automated tests. So the code I add here I want to be tested.
- I wanted to inject to code in the post. Hugo is static page generator so I wanted to have injected static code, on build.

# I tried multiple things

but they didn't work (or I did it wrong)
like:

- Trying to inject a file from higher level than hugo root folder. I was not able to do this. Probably because of security and it makes sense for me.
- I thought about copying the code before build and then including them. It look like nice idea and maybe I will try it in the future.

All of that solutions has own pros and cons, but there is different way.
I can download source code from github and embed it in post on build.

Moreover I have found an inspiration [here](https://github.com/haideralipunjabi/hugo-shortcodes) and I'm almost satisfied with that.

# My implementation of github shortcode for Hugo

The source code looks like this:

{{< highlight  go "linenos=false,linenostart=1" >}}

{{ $dataJ := getJSON "https://api.github.com/repos/"  $.Site.Params.github_repo_name  "/contents/"  (.Get "file")  }}{{ $con := base64Decode $dataJ.content }}{{ $con | safeHTML}}

{{< / highlight >}}

I know it it in one line, because if i don't do that then hugo will add additional breaks. I think I can be fixed, but maybe in the future.

There are some changes I have added:

- I got repo name from `Params.github_repo_name` then I can assume that the code is in the same repo. So updating it should be easy
- I added `| safeHTML` at the end to not encode special chars.
  - I can be not safety, but I add there only code from my repo currently so it should not be a problem :)

# Does it interesting?

Does it useful? Leave a comment and let me know.

Do you want to help me? Create a PR to the [repo](https://github.com/jwickowski/codepruner.com) with an improvement.

Thank you for reading.

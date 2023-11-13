---
author: Jerzy Wickowski
categories:
  - blog
date: 2021-05-14T05:08:58.000Z
disqus_identifier: 'https://codepruner.com/posts/hugo/how-to-add-gtm-to-static-hugo-website'
disqus_title: How to add Google Tag Manager to static hugo website?
disqus_url: 'https://codepruner.com/posts/hugo/how-to-add-gtm-to-static-hugo-website'
draft: false
images:
  - images/blog/red-sharp-pruner.jpg
tags:
  - hugo
  - blog
  - GTM
title: How to add Google Tag Manager to static hugo website?
type: regular
url: posts/hugo/how-to-add-gtm-to-static-hugo-website
---

# Why I want to add Google Tag Manager
I want to do this because:
- New Google Analytics requires GTM
- It allows me to connect Facebook Pixel in the future without any code changes
- It gives me more options related to some events in the future

## Step 1 - Add a GTM parameter into config.toml
It is required because it allows to to change the tag in the future without code changing.  You have to member to add the new parameter in `params` section. It will look like this:
```
[params] 
  googleTagManager = "GTM-XXXXXX"
```

## Step 2 - Create partials for GTM
You can find some tutorial in the internet that you can just add GTM scripts into layout page. It is a solution, but I don't like it because:
- It makes code more complicated
- Then theme update will be painful
- When you change theme you will have to add it again
- When google change GTM scripts then you will have to go there again

So I suggest to create two files:
- `layouts/partials/googleTagManagerHead.html`
    ```
        {{ if eq (getenv "HUGO_ENV") "production" | or (eq .Site.Params.env "production")  }}
            {{ if $.Site.Params.googleTagManager }}
                <!-- Google Tag Manager -->
                <script>(function(w,d,s,l,i){w[l]=w[l]||[];w[l].push({'gtm.start':
                    new Date().getTime(),event:'gtm.js'});var f=d.getElementsByTagName(s)[0],
                        j=d.createElement(s),dl=l!='dataLayer'?'&l='+l:'';j.async=true;j.src=
                        'https://www.googletagmanager.com/gtm.js?id='+i+dl;f.parentNode.insertBefore(j,f);
                        })(window,document,'script','dataLayer','{{ $.Site.Params.googleTagManager }}');</script>
                <!-- End Google Tag Manager -->
            {{ end }}
        {{ end }}
    ```
- `layouts/partials/googleTagManagerBody.html`
    ```
        {{ if eq (getenv "HUGO_ENV") "production" | or (eq .Site.Params.env "production")  }}
            {{ if $.Site.googleTagManager }}
                <!-- Google Tag Manager (noscript) -->
                <noscript><iframe src="https://www.googletagmanager.com/ns.html?id={{ $.Site.Params.googleTagManager }}"
                    height="0" width="0" style="display:none;visibility:hidden"></iframe></noscript>
                <!-- End Google Tag Manager (noscript) -->
            {{ end }}
        {{ end }}

    ```

Now you have component that you can embed on you site

## Step 3 - Add additional layer
You can just include that partials in the page, but I would like to suggest you adding additional layer to have an opportunity to extend some scripts or tags in the future.

Add two additional templates:
- `layouts/partials/head-additions.html`
    ```
    {{ partial "googleTagManagerHead.html" . }}
    ```

- `layouts/partials/top-body-additions.html`
    ```
    {{ partial "googleTagManagerBody.html" . }}
    ```
Don't forget about the dot(`.`) at the end. If you don't do thais then context will be not passed, so your param won't be visible.


## Step 4 - Embed GTM partials
Now you have to analyze your layout and include:
- `head-additions` at the **end of the head** section
    -using: `{{ partial "head-additions.html" . }}`
- `top-body-additions` at the **beginning of the body** section
    -using: `{{ partial "top-body-additions.html" . }}`

In my current theme Anake I didn't have to embed head-additions, because it was already added.

# Now configure your GTM 
and have fun with collecting and analyzing data

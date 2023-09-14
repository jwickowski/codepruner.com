---
title: "Why not WordPress?"
images:
  - "images/blog/red-sharp-pruner.jpg"
url: "posts/hugo/why-not-wordpress"
date: 2021-03-23T6:40:58+01:00
draft: false
tags: ["hugo", "blog", "wordpress"]

---

# It is not my first blog
I have been present in the internet since 2016 year. I have blog [JerzyWickowski.pl](https://jerzywickowski.pl) and [Blog.DeployAacademy.pl](https://blog.deployacademy.pl/). 

Both of them are using Wordpress and I still have to tell that wordpress is great, but there are some cons:
 * It is difficoult customize, because there are mass of code around
 * It isn't simple to recreate
   * To restore backup of old version
   * To create new instance, for test purposes for examle
 * It requires updating plugins all the time
 * It requires internet connection to work on new posts
 * Working on Wordpress editor make me nervous. 
   * Classic one is too lov level
   * Gutenberg is too slow and not conforrtable is i want to copy or move semething

So I decides to find... 

# An alternative
I have multiple options:
* I can write my on blog engine
  * But it will take me a lot of time
  * I don't have soo much free time to write something like this
* I can create a tool to write posts in Markdown and import them in to wordpress
  * But Wordpress is not only a collection of posts. There are pluggins, settings, pages
  * So recreating it will be painful
* I can use an different CMS, like WIX, Joomla etc
  * But there will be no difference
* I can use a static page generator like Jekyll or Hogo
  * And it is my destiny, but...

# What I need from blog tool
* I want to wrire posts in Markdown
  * Because it easy
  * Because it doesn't interrupt me
  * Because it is very easy to migrate
* I want to have a posibility to simple recreate a whole page
  * To restore backup
  * To test new layout or feature on a different domain
  * To write some plugins if required in safe environement
* I have to have a standard way to cooperate with community 
  * When someone would like to write a post to my blog
  * When someone would like to improve one on my post
* I want to automate as much as I can

# The decision is...
To use [Hugo](https://gohugo.io/). I will try to use it. I thought the start will be easier and faster. I will descrbe it later.

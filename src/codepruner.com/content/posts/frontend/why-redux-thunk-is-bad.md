---
title: "Why redux-thunk is bad and what it an alternative"
date: 2021-07-21T10:40:58+01:00
draft: true
tags: ["frontend", "redux", "react", "libraries"]
---

I hate redux-thunk. It make code much more complicated than it can be, and make it very difficoult to undestand the flow with my human/dev brain.


# Why redux-thunx is bad
 - It allows to pass function as redux action's payload

 - It destroy redux simplicity, becouses actions are not pure
 - It hides complex logic
 - It mix a flow from simple one-directional

# How it should looks like
  - simplify a flow 
  - do async calls, just like functions
     - but you don't see a full flow
  - do async calls after state is updated by reducers
     - and throw additionl actions with postfix `PENDING`, `SUCCESS`, `FAILED`  

# Tools for it?
- loop?
- Observable ?
- Simple effect middleware

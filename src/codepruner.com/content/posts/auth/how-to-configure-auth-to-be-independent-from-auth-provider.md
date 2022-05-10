---
title: "How to configure Auth to be independent from auth provider"
date: 2022-02-28T11:40:58+01:00
draft: true
tags: ["auth", ".net", "jwt", "C#"]
---

Have you ever implement authentication or authorization? As developer we do it very often, because it a task that have to be done only once in a project. I have that pleasure to do it in current project. 


# Multiple auth options
- self implementing with login and password
- using a standard external provider like google or office365
- use a client specific approach, like Windows Authentication for example

All of these methods can work, but all of them are very specific and using them in the application increase our PRZYWIĄZANIE to specific provider.


# One to rule them all
Even if we choose one of above providers we still have a chance to be independent from specific impelmentation. You can use Json Web Token, known also as JWT.

What is it? On [JWT official site](https://jwt.io/) you can read 'JSON Web Tokens are an open, industry standard method for representing claims securely between two parties.' 

In our context it is a standard and safe way to share information about logged user and we will use it like that.

# How it can work
Let's assume you have three parts in your application:
- Frontend - It can be browser application in React, Angular, Vue or in pure JS. 
- Api - The backend. Frontend sends requests to API.
- AuthApi - An API to generate JWT

It will work like that:
1. `Frontend` do a request to `AuthApi` to generate JWT Token
2. `AuthApi` authenticate the user with a specific provider and returns JWT Token
3. `Frontend` saves token to use it later
4. When `Frontend` do a request to `Api` then it adds a token to the request
5. `Api` verify token signature, expiry date and more is wants. 
  - If token passes the validation then request is handled
  - If token is invalid then `Api` returns an error

It doesn't look very complicated, does it?

# Then you have the imdependency
When you use authentication based on JWT you are indepemndet from auth provider. 
If you wish you can change the provider and you just need to meed the JWT interface and its content. 

In that approach JWT is your own abstract any-corrrption layer. 

# Next parts
I am sure there will be next parts about JWT and topics around auth.

I am happy you are here, at the end of the article. 

Can you tell me what is your approach to auth in your projects?

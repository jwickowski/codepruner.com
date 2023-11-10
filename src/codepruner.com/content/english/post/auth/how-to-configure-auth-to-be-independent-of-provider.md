---
author: Jerzy Wickowski
categories:
  - auth
date: 2022-06-22T10:40:58.000Z
draft: false
images:
  - images/blog/red-sharp-pruner.jpg
tags:
  - auth
  - jwt
  - backend
title: How to configure Auth to be independent of provider
type: regular
url: posts/auth/how-to-configure-auth-to-be-independent-of-provider
---

Have you ever implement authentication or authorization for a project you worked on? I can say,  we don't do it very often, because it is needed to be done only once in a project. There is a high probability you have never implemented it. It was true for me as well, but I am so lucky, I had that pleasure to do it. 

# Get the drivers
At the beginning I had to decide what approach will be the best for our project. We need to take multiple drivers into account. Some of the most important are:
- Client's policy/requirements
  - Usually it is the most important criterion. If client wants to use domain Account to login, we have to use it. 
- Application type 
  - For example we can't use cookies when we don't use browser.
- Will the application be split to multiple services?
- How to protect the project if client changes their mind in the future?

## Find the best option
When you know what you need, it is time to decide what approach will be the best for the project. Maybe you are thinking what solution is the best and how many options you have. To be honest, not too many. We can divide them into two groups:

- Self implementing
  - but, I don't recommend it, if you really don't have to.
- External provider
  - the standard one like google or office365
  - use a client specific approach

It doesn't matter what you choose, because the project will depend on an external provider or your implementation of it. You should consider adding an anti-corruption layer on it, to be sure  your project will be protected against the change of auth contract. 

# One to rule them all
To have a standard way of auth, you can use Json Web Token, known as JWT. 

What is it? On [JWT official site](https://jwt.io/) you can read 'JSON Web Tokens are an open, industry standard method for representing claims securely between two parties.' 

In our context it is a standard and safe way to share information about logged user and we will use it like that.

# How it works
Let's assume you have three parts in your application:
- `Frontend` - It can be browser application.
- `Api` - The backend. `Frontend` sends requests to API.
- `AuthApi` - An API to generate JWT.

How the flow of authentication can looks like?

## Auth flow - happy path 

1. `Frontend` does a request to `AuthApi` to generate `JWT Token`
2. `AuthApi` authenticates the user with a specific provider and returns `Token`
3. `Frontend` saves token to use it later
4. When `Frontend` does a request to `Api` then it adds a `Token` to every request
5. `Api` verifies token signature, expiry date and more properties if needed. 
  - If token passes the validation then request is handled
  - If token is invalid then `Api` returns an error

It doesn't look very complicated, does it?

## Auth flow - not logged in
But there is a possibility that user won't be verified:

1. `Frontend` do a request to `AuthApi` to generate `JWT Token`
2. `AuthApi` returns an error that client cannot be logged
3. `Frontend` 
  - Can try again
  - Can redirect user to 401 page

## Auth flow - token expired
Let's assume user stored a `Token` and it expired. What will the flow look like:

1. `Frontend` has saved `Token` 
2. `Frontend` does a request to `Api`
3. `Api` returns error that token is expired
4. `Frontend` can do a request to `AuthApi` to get new `Token`
5. When `AuthApi` returns new `Token`
6. Then `Frontend` can retry request to `Api` again

# You have independence
That solution is great, because you have only one point in the application that depends on auth provider. The rest of the application is safe. If you wish, you can change the provider and you just need to meet the JWT interface and its content. 

# A little request to you
Can you tell me what is your approach to auth in your projects?

Would you like to see it in examples? Let me know below :)

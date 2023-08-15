---
title: "Never hardcode api endpoints. Generate THEM!"
date: 2023-08-10T10:40:58+01:00
draft: false
tags: ["javascript", "typescript", "apiClient", "api", "generator", "ongoing refactor", "code generator"]
---

Have you ever forgot to update frontend endpoint address when backend specification were changed? I am sure you have! I did it multiple times. I know a great way for us to avoid that situations in the future.

# Hardcode story
It is a natural part of a development that we hardcode. Mostly at the beginnig of every project. When we start creating new solution is it usually simpler and faster to write an address as a string and be happy that requests are done successfully. Application is working, everyone is happy.

There are not too many disadvantages of that solution, a t the beginning, but they will appear. Let imagine you will be adding more and more endpoints, more and more models. Then... one of backed developers discovers that he did a typo in a model and decides to fix it. Just changing a field in request from `transcation` to `transaction`. Smalll change. What can go wrong...... :)

Maybe you will no see errors. Requests will be sent, but the new field `transaction` will be empty, because the change wasn't applyed on client side. But you will lose more and more data and more and more money. In better scenario you will notice some exceptions or it will be discoverd during tests, but it can be done better.

# A way to fight with that risk
There are mulpiple ways to handle the potential risk, but have you ever though what will be the solution? Take a look at some solutions:

- Having more and better testers - The most obvious solution, but to be honest they are only humans. They can make mistakes. Nothing more to add.
- E2E tests - Yes. It is great idea to have them. They will be able to protect us againts issues like that, but there are more problems with e2e tests. They are expensive and slow. Moreover you will be informed about the issue after merge, when test run on CI. It not bad, but can be better.
- Generate stronly typed client - It is the best option. You will never do a typo in a model or in an address. You will not have to think about maching request with response with method. It will be done automatically. Moreover you will be informed about any api changes on build level, so the feedback loop will be much faster. And because you will see these changes in your GIT, then applying these changes will be fast and easy.

# How to generate the client and why use swagger-typescript-api 
When you decide that you want generate api client you must make a decision how to do it. There are at least two options, but the 2nd one is better :).

1. Generate FrontEnd client based on backend source code. I have described the solution in post [Why and how generate models from C# to Typescript]({{< relref "./why-and-how-generate-models-from-csharp-to-typescript.md">}}). Today, after 2 years, I know there are better alternatives to generate the code. 
 
2. Generate the client using existing tool. It is better to not discover wheel again. If someone did a good job we should use it, but how to find a good solution?
I did an investigation and I can recomend you [swagger-typescript-api](https://github.com/acacode/swagger-typescript-api).

What were my requirements:
- It should consume `swagger.json` file. 
  - Then I will be able to use for any project I wish in the future.
- It should be run and be configured via CommandLine. 
  - I didn't want to create or use an application to generate file. 
  - I wanted to be able to generate it with one commnd
  - and have a possibility to integrate it somewhere to run automatically
- Generated Client should be flexible:
  - eg. add/modify some headers is a must
- Methods must be conncted with types

And I have found `swagger-typescript-api`. It meets all my expetations.

Here is a code snipped we use to generate ApiClient.

```
npx swagger-typescript-api `
--axios `
--single-http-client `
--path swagger.json `
--api-class-name GeneratedApiClient `
--output GeneratedApiClient.generated.ts `
--name GeneratedApiClient 
```
Of course, I need to downlod `swagger.json` file first, but it is not a big problem, because you can write a simple script to download it first.

Then I suggest you to have a one place when you create an instance of the Client like:
```
const createApiClient = (): GeneratedApiClient<unknown> => {
  const apiUrl = getApiUrl();

  const httpClient = new HttpClient({
    baseURL: apiUrl,
  });
  const result = new GeneratedApiClient(httpClient);
  return result;
};

export const getBaseUrl = (): string => {
    return "https://localhost:4444";
};
```

Use it and be safe without typos and with strong type request and response.

Tell in the comment what is your approach!


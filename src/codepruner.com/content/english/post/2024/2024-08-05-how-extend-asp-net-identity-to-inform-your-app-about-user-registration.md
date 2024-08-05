---
author: Jerzy Wickowski
categories:
  - dotnet
date: 2024-08-05T05:40:58.000Z
disqus_identifier: how-extend-asp-net-identity-to-inform-your-app-about-registration
disqus_title: How to extend AspNet Identity to inform your app about user registration
disqus_url: 'https://codepruner.com/how-extend-asp-net-identity-to-inform-your-app-about-user-registration'
draft: false
images:
  - images/posts/2024/2024-08-05-how-extend-asp-net-identity-to-inform-your-app-about-user-registration.jpg
tags:
  - ddd
  - aspnet
  - identity
  - dotnet
  - user
  - account
title: How to extend AspNet Identity to inform your app about user registration
type: regular
url: how-extend-asp-net-identity-to-inform-your-app-about-user-registration
---
AspNet Identity is a .NET library from Microsoft. It provides authentication and authorization for ASP.NET application. It is regularly developed and maintained. Moreover, together with .NET 8 `Identity API endpoints` was published. I decided to use it in my project, but I had a need to extend it. Are you interested it? Welcome.

## My requirements
1. I have decided to create separate API application for Auth, like I have describe it in [one of my previous post]({{< relref "../auth/how-to-configure-auth-to-be-independent-of-provider.md" >}}). I use there `ASP .NET Identity API endpoints`. 

2. I need to execute something when user is registered. The case is described in post [ How to implement an invitation for a user that does not exist]({{< relref "./2024-07-30-how-to-implement-an-invitation-for-a-user-that-does-not-exist.md" >}}). 

3. I don't want to change already created code if I don't have to. Because the old code is working and it is well tested.

## Implementation
I am not going to describe how to configure `ASP NET API Identity endpoints`, because you can find the nice instruction on [Microsoft's page](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/identity-api-authorization?view=aspnetcore-8.0). But if it would be worth for you, let me know! I can describe it a bit simpler.

Let assume you have already configured the `endpoints`. Because there are not build in events in ASP.NET Identity we have to handle ourself. The core of ASP .NET Identity is `UserManager`. So you have to create a new class that inherits from `UserManager`. It can look like:
``` csharp
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace CodePruner.Example.Auth.API;
public class ApplicationUserManager : UserManager<IdentityUser>
{
    private readonly IEventPublisher _eventPublisher;
    public ApplicationUserManager(
        IUserStore<IdentityUser> store,
        IOptions<IdentityOptions> optionsAccessor,
        IPasswordHasher<IdentityUser> passwordHasher,
        IEnumerable<IUserValidator<IdentityUser>> userValidators,
        IEnumerable<IPasswordValidator<IdentityUser>> passwordValidators,
        ILookupNormalizer keyNormalizer, 
        IdentityErrorDescriber errors,
        IServiceProvider services,
        ILogger<UserManager<IdentityUser>> logger,
        IEventPublisher eventPublisher)
        : base(
                store,
                optionsAccessor,
                passwordHasher,
                userValidators,
                passwordValidators,
                keyNormalizer,
                errors,
                services,
                logger)
    {
        _eventPublisher = eventPublisher;
    }

    public override async Task<IdentityResult> CreateAsync(IdentityUser user, string password)
    {
        var result = await base.CreateAsync(user, password);
        if (result.Succeeded)
        {
            await _eventPublisher.PublishAsync("user.registered", new { UserId = user.Id, Email = user.Email });
        }
        return result;
    }
}
```
There are two things worth to indicate:
- I have just overridden one method `CreateAsync` in a very simple way. Just execute the base code and publish an event.
  - Of course it can be any logic you want, but event is nice, because it can be handled in any place which need it
- There is additional parameter `IEventPublisher eventPublisher` and rest of them are just passed to parent constructor.

Here is also an interface for `IEventPublisher`:
``` csharp
public interface IEventPublisher
{
    Task PublishAsync(string eventName, object value);
}
```

There is the last thing you have to remember. To register it in you IoC container:
``` csharp
services.AddScoped<UserManager<IdentityUser>, ApplicationUserManager>();
services.AddScoped<IEventPublisher>();
```

## What next and summary? 
With that solution you are open for new changes, like in Open-Close Principle from SOLID. 
I recommend it. 

Let me know what do you think about. Below is a comment section :)

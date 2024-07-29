---
author: Jerzy Wickowski
categories:
  - ddd
date: 2024-07-26T05:40:58.000Z
draft: false
images:
  - images/posts/2024/2024-07-26-how-to-implement-invitation-for-a-user-that-not-exists.jpg
tags:
  - ddd
  - implementation
  - dotNet
  - ideas
title: how-to-implement-invitation-for-a-user-that-not-exists
type: trending

---


```
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Vataha.Auth.API.Auth
{
    public class ApplicationUserManager : UserManager<IdentityUser>
    {
        private readonly IEventPublisher _eventPublisher;

        public ApplicationUserManager(
            IUserStore<IdentityUser> store,
            IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<IdentityUser> passwordHasher,
            IEnumerable<IUserValidator<IdentityUser>> userValidators,
            IEnumerable<IPasswordValidator<IdentityUser>> passwordValidators,
            ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors,
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

    public class  IEventPublisher
    {
        public Task PublishAsync(string eventName, object value)
        {
            return Task.CompletedTask;
        }
    }

    
}

public record UserRegisteredEventData(Guid UserId, string Email);
public record UserRegisteredEvent(string Name, UserRegisteredEventData Data) : Event<UserRegisteredEventData>(Name, Data);
public record Event<T>(string Name, T Data);

```

  services.AddScoped<UserManager<IdentityUser>, ApplicationUserManager>();
  services.AddScoped<IEventPublisher>();
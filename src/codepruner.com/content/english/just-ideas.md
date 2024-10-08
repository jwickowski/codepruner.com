---
title: "Just ideas"
# description
description: "It should be public. It is just a palace where I keep some ideas for posts etc"
layout: "contact"
draft: true
---

## We would Love To Hear From You....


POSTY:
- Czym jest CAP? Jak go użyć? itp itd
- Jak to ogólnie ogarnac (mozeżę być sharepoint, a moze byc blob, po prostu datasource)
- Jak Podejść do tematu, gdy musimy np. przetłumaczyć pliki i coś z nich wyciagnąć (skillset)
- Osobno, jak skonfigurować skillset, aby zrobić split -> translate -> merge 
- Jak zrobić to przy pomocy asystentów w OpenAI (i jakie to ma wady/zalaty)
- describe BICEP  https://learn.microsoft.com/en-us/azure/azure-resource-manager/bicep/overview?tabs=bicep 

- signalR - technical post how it works in practice with examples (tutorial)
  - One simple with JS
  - One with Blazor
  - One with Redux
  - One with shared state
  - One with generating client code
  - how to be sure that the message was delivered in signalR
  - how to be sure that all data is up to date with signalR

- Jak odpalić hugo server z poziomu dockera (trzeda dodać --bind 0.0.0.0)
- Testcontainers + xunit + Class Fixtures (+ Assembly Fixtures in v3)
  - https://testcontainers.com/
  - https://xunit.net/docs/shared-context#assembly-fixture

## Some basic terms
- TCP - Transmission Control Protocol
- UDP - User Datagram Protocol

### Azure Disk storage
- can be unmanaged or manage - READ ABOUT IT
  - managed disk is more expensive but you don't have to manage it
  - unmanaged disk is cheaper but you have to manage it

# Access control (IAM)
- Identity and Access Management
- who can do what
- role-based access control (UNDERSTAND IT)
  - roles
    - owner - can do anything
    - contributor
    - reader
    - user access administrator
    - etc.
  - you can create custom roles
- to check:
  - if you add access on resource group, it is inherited by all resources in this group ???

## Replication options 
-- WHEN WE TALK ABOUT STORAGE THEN IT WILL A GOOD PLACE FOR IT, and here should we have a link to 
When you think about the high availability, disaster recovery, etc. you should think about replication. There are some options in Azure:
- Locally Redundant Storage (LRS) - data is replicated 3 times in the same datacenter
- Zone Redundant Storage (ZRS) - data is replicated 3 times in 3 different datacenters
- Geo Redundant Storage (GRS) - data is replicated 3 times in the same region and 3 times in a different region
- Read-Access Geo Redundant Storage (RA-GRS) - the same as GRS but you can read data from the second region
- Geo Zone Redundant Storage (GZRS) - the same as GRS but data is replicated in 3 different regions
- Read-Access Geo Zone Redundant Storage (RA-GZRS) - the same as GZRS but you can read data from the second region
- Read-access geo redundant storage (RA-GRS) - the same as GRS but you can read data from the second region


## Azure Marketplace
When you select a resource to install you select them in the Azure Marketplace. There are a lot of services provided by Microsoft. We described some of them in [the previous post] But it is more that that.
- It is very similar to normal eShop
- There are products/services from Microsoft and from other vendors
- One template can provide one or more services
- You can think about it like "Azure Shop"
- There are IaaS, Paas and SaaS


There is something bigger than `Azure Merketplace`. It is `Comertial Marketplace`
It contains:
- `Azure Marketpalce`
  - It is only for azure
  - Dedicated for Developers
- `Microsoft Appsource`
  - Is is for wider use like: `Azure`, `Power BI`,`Dynamics 365`,`Microsoft 365`

## How to configure it on backend


## How to configure it in frontend


## Base for convencions test
```csharp
public class ConvenctionTests
{
    [Fact]
    public void check_total_amount_of_assemblies()
    {
        var assemblies = GetAssemblies();
        assemblies.Count().ShouldBe(13);
    }

    [Fact]
    public void assemblies_and_csprojs_amount_should_be_the_same()
    {
        var csprojs = GetCsProjs();
        var assemblies = GetAssemblies();

        csprojs.Count().ShouldBe(assemblies.Count());
    }

    private IEnumerable<Assembly> GetAssemblies()
    {
        var processed = new HashSet<string>();
        var toCheck = new Queue<RuntimeLibrary>(DependencyContext.Default.RuntimeLibraries);
        while (toCheck.Any())
        {
            var runtimeLibrary = toCheck.Dequeue();
            if (processed.Contains(runtimeLibrary.Name))
            {
                continue;
            }

            processed.Add(runtimeLibrary.Name);
            if (runtimeLibrary.Name.StartsWith("PROJECT_PREFIX"))
            {
                var assembly = Assembly.Load(new AssemblyName(runtimeLibrary.Name));
                yield return assembly;
            }
        }
    }

    private IEnumerable<string> GetCsProjs()
    {
        var basePath = AppDomain.CurrentDomain.BaseDirectory;
        basePath = Path.Combine(basePath, "../../../../");
        basePath = Path.GetFullPath(basePath);
        var files = Directory.GetFiles(basePath, "*.csproj", SearchOption.AllDirectories);
        return files;
    }
}

```
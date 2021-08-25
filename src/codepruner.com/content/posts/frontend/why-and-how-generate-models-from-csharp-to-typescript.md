---
title: "why-and-how-generate-models-from-csharp-to-typescript"
date: 2021-08-10T10:40:58+01:00
draft: true
tags: ["javascript", "typescript", "C#", "clean code", "code generator"]
---

I don't understand why developers doesn't automate their job. They can save a huge amount of time with very low effort. How?  For example, by generating models from backend to frontend. We will talk about it today.
```
{{% code file="BackendField.cs" %}}
```
# How the generation can look like?
The main idea is to keep backend and frontend models synchronized automatically or almost automatically. In most cases frontend ask backend about data, so the main source of truth should be at backend site. I see it in that way. When model is changed on backend then frontend models should be updated automatically or by executing simple command, like running a console app.

# Why generating is good
Before we go to details we should talk why it is soo good:
- Focus on important things
   - not on coping names from one place to another
   - We are developers. We should solve problems, not copy labels. Especially when we can avoid stupid work easily
- Less work in the future
   - To not do the same work twice, so you will save your time
- No risk of typo
   - We are humans. We make errors, like typos. We can reduce them to minimal by generating code.
- Errors on build
   - When you map  manually then you can be not synchronized and you can miss something. 
   - But when you do it automatically, then TypeScript compiler fail on build and it always better to know about an error earlier
- Better performance
   - Sometimes you need to check some data, some types, some structures, because you are not sure about it. When you generate it, they are sync. So some checking can be ignored, because compiler does it.


# Why people don't do this often?
If they are some many benefits, why this approach is so rare? 

- I have no idea :)
- People don't know how to achieve it
- People believe that front/and and backend are independent (then I suggest to use BFF (Backend For Frontend) pattern)

# How to do this
- Write it by your own, but it will be discovering a new circle
- Use complete tool
- Hybrid, write your code, but with usage of some libraries

# A real example
Here you can see a sample implementation. My approach is to create a simple version at the beginning and improve it when it is required. So that solution is not perfect, but it is simple and it will give you benefits form the 1st day of usage.

I will split into three fragments:
1. How to get metadata
2. How to generate code
3. How to run it

So let's get started
## Getting metadata

At the beginning we need a model to keep Fields in simple flat format.
```
public class BackendField {
   public string Name { get; set; }
   public string Type { get; set; }
}

```

When we have model we can get it from metadata. We will use reflection for this:
```
public class BackendFieldGetter {
   public IEnumerable<BackendField> GetBackendField(type sourceType){
        var properties = sourceType.GetProperties();
        foreach(var property in properties)
        {
           var propertyType = property.PropertyType.Name;
           var propertyName = propertyInfo.Name;
           var backendField = new BackendField{
              Name = propertyName,
              Type = propertyType
           }
            yield return backendField;
        }
   }
}
```

Ok. It is done. Not we need to consume that data and create file content

```
public class TypeScriptModelGenerator {
   public string GenerateModel(string className, IEnumerable<BackendField> backendFields) {
      var sb = new StringBuilder();
      sb.Append($"export type {className} {{");
      foreach(var backendField in backendFields){
      sb.Append($"  {backendField.Name}: {backendField.Type}")           
      }
      sb.Append($"  ")
      sb.Append("}");
      return sb.ToString();

   } 

   private string GetFrontendType(BackendField backendField){
      switch(backendField.Type){
         case "Int32":
            return "number";
             case "String":
            return "string"
      }

      return backendField.Type
   }
}
```


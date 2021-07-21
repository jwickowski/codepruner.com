---
title: "To throw or to throw ex. How to rethrow exceptions in C#"
date: 2021-06-14T11:40:58+01:00
draft: false
tags: ["legacy", "clean code", "exception", "C#"]
---

Some day I had to investigate a production issue based on logs. My luck that all exception's properties were logged, but there was a small vulnerability. Stack trace was missing. So my investigating was much longer than I expected.

To how it to you, I have created a code sample. Take a look.

# Missing Stack Trace with `throw ex`
Here is a code where is `throw ex`.
```
public class CodePrunerExceptionThrower
{
	public void ReThrowWithEx(){
		try
		{
			Inner();
		}
		catch(Exception ex)
		{
			throw ex;	
		}
	}
	
	private void Inner() {
		Deep()
	}

    private void Deep() {
        Hell();
    }

    private void Hell(){
        throw new Exception();
    }
}

```
What is wrong in that code? We are not able to see it at first look, but if you look at Stack Trace you will have an answer.

```
   at CodePrunerExceptionThrower.ReThrowWithEx()
   at Program.Main()
```

I think you can see it. The exception is throw from `Hell` but in out stack trace we can see only the method where it was reThrown. It is very important information we don't want to lose. Because the missing of that info make our investigation much longer. So... how to solve it?

# Replace `throw ex` with `throw` to not miss Stack Trace
The new version of the code look that:
```
public class CodePrunerExceptionThrower
{
	public void ReThrowWithoutEx(){
		try
		{
			Inner();
		}
		catch(Exception ex)
		{
			throw;	
		}
	}
	
	public void Inner() {
		Deep();
	}

    public void Deep() {
        Hell();
    }

    public void Hell(){
        throw new Exception();
    }
}
```

Now when the exception will be caught somewhere Stack Trace will be like:
```
   at CodePrunerExceptionThrower.Hell()
   at CodePrunerExceptionThrower.Deep()
   at CodePrunerExceptionThrower.Inner()
   at CodePrunerExceptionThrower.ReThrowWithoutEx()
   at Program.Main()
```
and your life will be easier, because it is clear when exception was originally thrown.

I know it is not intuitive and it looks like hidden C# feature, but it works.

# Should I do this always?
Probably yes. But there same cases that you will want to hide your Stack Trace, but you should not do it accidentally.

What is your approach to re-throwing exception? Have you ever hide your Stack Trace?

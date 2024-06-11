# Writting Files Shutdown AbpModuleManager

Writting static files into `bin` directory has caused AppDomain Recycled. In some cases, IIS is really weak, it only knows content changed.

## Reasons

Many reasons cause AppDomain recycled.
- Machine.Config, Web.Config or Global.asax are modified
- The bin directory or its contents is modified
- The number of re-compilations (aspx, ascx or asax) exceeds the limit specified by the <compilation numRecompilesBeforeAppRestart=/> setting in machine.config or web.config (by default this is set to 15)
- The physical path of the virtual directory is modified
- The CAS policy is modified
- The web service is restarted (2.0 only) Application Sub-Directories are deleted (see Todd’s blog for more info)


## AppDomain Recycling Metrics

### Windows Event Log
In machine's `Web.config`，append below to section `<healthMonitoring><rules>`。
```xml
<add name="Application Lifetime Events Default" eventName="Application Lifetime Events" provider="EventLogProvider" profile="Default" minInstances="1" maxLimit="Infinite" minInterval="00:01:00" custom=""/>
```

### Programtical Log

Hook `Application_End`, we can get `HttpRuntime` instance for logging.

```C#
HttpRuntime runtime = (HttpRuntime) typeof(System.Web.HttpRuntime).InvokeMember("_theRuntime", BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.GetField, null, null, null);
string shutDownMessage = (string) runtime.GetType().InvokeMember("_shutDownMessage", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetField, null, runtime, null);
string shutDownStack = (string) runtime.GetType().InvokeMember("_shutDownStack", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetField, null, runtime, null);
```


## Finding out jokers?

- [FileMon](https://learn.microsoft.com/zh-cn/sysinternals/)


# Tor Checker

Check if an IP is a Tor exit addresses.

It uses the updated list from **blutmagie.de** and **TorProject.org**.  

08/11/2018  
http://torstatus.blutmagie.de is no more available.  On the website there is this message:
  
  Hello world! I decided to discontinue my Tor Network Status service at 11/06/18.  
  Olaf Selke


## Tor documentation

Official Tor documentation: https://www.torproject.org/docs/faq-abuse.html.en#Bans  
Tor exit relay list: https://check.torproject.org/cgi-bin/TorBulkExitList.py  
DNS-based list you can query: https://www.torproject.org/projects/tordnsel.html.en  
  
The first link actually does not provide any list, but in that page there is a link to this: https://check.torproject.org/exit-addresses.  


## Usage

See the Example.ExampleOfUsage file.

### On-demand check
```C#
// The checker does not make any update in background and execute the "check" on demand
var checker = new TorChecker.Checker();

var clientIsUsingTor = checker.IsUsingTor("1.1.1.1");
```


### Backgroud update
```C#
// Set it to autoupdate the list every "settings.DefaultBacgroundUpdateIntervalMilliseconds" minutes
var settings = new TorChecker.Settings { BackgroundUpdateEnabled = true };
var checker = new TorChecker.Checker(settings);

var clientIsUsingTor = checker.IsUsingTor("1.1.1.1");
```



## NuGet package

The NuGet package is named [Tor.Checker](https://www.nuget.org/packages/Tor.Checker/).

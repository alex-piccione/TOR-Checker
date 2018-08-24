# Tor Checker

Check if an IP is a TOR exit node.

It use the updated list from **blutmagie.de**.

Another list is here: https://check.torproject.org/exit-addresses.  

## Tor documentation

URL: https://www.torproject.org/docs/faq-abuse.html.en#Bans  
Tor exit relay list: https://check.torproject.org/cgi-bin/TorBulkExitList.py  
DNS-based list you can query: https://www.torproject.org/projects/tordnsel.html.en  


## Usage

See the Example.ExampleOfUsage file.

### Occasional search
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

The NuGet package is named [TOR Checker](https://www.nuget.org/packages/TORChecker/).

The file "Publish package.bat" is part of the solution but it is only usable with a private key that is obviously not included in the repository.
@echo OFF

set suffix=alpha
set author="Alessandro Piccione"
set /p key=<NuGet_API_key.txt

nuget push "TorChecker\bin\release\Tor.Checker.0.3.2-alpha.nupkg" %key% -Source https://www.nuget.org/api/v2/package
echo.
echo ** NuGet package published **
echo.

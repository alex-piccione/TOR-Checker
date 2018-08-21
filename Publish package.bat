@echo OFF

set suffix=alpha
set author="Alessandro Piccione"
set /p key=<NuGet_API_key.txt

nuget push "IPChecker\bin\release\IPChecker.0.1.0-alpha.nupkg" %key% -Source https://www.nuget.org/api/v2/package
echo.
echo ** NuGet package published **
echo.

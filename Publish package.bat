@echo OFF

set suffix=alpha
set author="Alessandro Piccione"
set /p key=<NuGet_API_key.txt

nuget push "TORChecker\bin\release\TORChecker.0.2.0-alpha.nupkg" %key% -Source https://www.nuget.org/api/v2/package
echo.
echo ** NuGet package published **
echo.

rmdir /s /q bin
rmdir /s /q obj
dotnet pack -c Release
del bin\Release\*.symbols.nupkg
nuget push .\bin\Release\*.nupkg -s %BMNugetServerLocation%
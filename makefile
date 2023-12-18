pack:
	dotnet restore
	dotnet pack -c Release -o ./artifacts --include-symbols

publish:
	dotnet nuget push ./artifacts/* --api-key $(NUGET_API_KEY) -s "https://nuget.pkg.github.com/MuehlbergTech/index.json"
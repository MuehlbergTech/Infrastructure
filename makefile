pack:
	dotnet clean
	dotnet restore
	dotnet pack -c Release -o ./artifacts --include-symbols
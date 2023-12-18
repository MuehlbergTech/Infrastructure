pack:
	dotnet restore
	dotnet pack -c Release -o ./artifacts
Remove-Item -Path .\publish -Recurse
dotnet publish .\Sokoban\Sokoban\Sokoban.csproj --configuration Release --runtime win-x64 --output publish\bin
Compress-Archive -Path publish\bin\* -DestinationPath publish\Sokoban.zip
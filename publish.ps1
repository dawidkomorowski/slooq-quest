Remove-Item -Path .\publish -Recurse
dotnet publish .\Sokoban\Sokoban\Sokoban.csproj --configuration Release --runtime win-x64 --output publish\bin
dotnet publish .\Sokoban\Sokoban.Editor\Sokoban.Editor.csproj --configuration Release --runtime win-x64 --output publish\bin
$productVersion = (Get-Item .\publish\bin\Sokoban.exe).VersionInfo.ProductVersion
Compress-Archive -Path publish\bin\* -DestinationPath publish\Sokoban.$productVersion.zip
$ErrorActionPreference = "Stop"

Remove-Item -Path .\publish -Recurse
dotnet publish .\SlooqQuest\SlooqQuest\SlooqQuest.csproj --configuration Release --runtime win-x64 --output publish\bin
dotnet publish .\SlooqQuest\SlooqQuest.Editor\SlooqQuest.Editor.csproj --configuration Release --runtime win-x64 --output publish\bin
Remove-Item -Path .\publish\bin\*.pdb
Copy-Item -Path .\LICENSE -Destination .\publish\bin\LICENSE.txt
Copy-Item -Path .\ThirdPartyNotices.txt -Destination .\publish\bin\ThirdPartyNotices.txt
$productVersion = (Get-Item .\publish\bin\SlooqQuest.exe).VersionInfo.ProductVersion
Compress-Archive -Path publish\bin\* -DestinationPath publish\SlooqQuest.$productVersion.zip
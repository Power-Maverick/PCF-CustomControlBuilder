$sourceNugetExe = "https://dist.nuget.org/win-x86-commandline/latest/nuget.exe"
$targetNugetExe = ".\nuget.exe"
Remove-Item .\Tools -Force -Recurse -ErrorAction Ignore
Invoke-WebRequest $sourceNugetExe -OutFile $targetNugetExe
Set-Alias nuget $targetNugetExe -Scope Global -Verbose

##
##Download CoreTools
##
./nuget install  Microsoft.CrmSdk.CoreTools -O .\CoreTools
md .\CoreTools
$coreToolsFolder = Get-ChildItem ./CoreTools | Where-Object {$_.Name -match 'Microsoft.CrmSdk.CoreTools.'}
move .\CoreTools\$coreToolsFolder\content\bin\coretools\*.* .\CoreTools
Remove-Item .\CoreTools\$coreToolsFolder -Force -Recurse

##
##Remove NuGet.exe
##
Remove-Item nuget.exe
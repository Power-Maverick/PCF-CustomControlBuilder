Function Find-MsBuild([int] $MaxVersion = 2019)
{
    $agentPath2019 = "$Env:programfiles (x86)\Microsoft Visual Studio\2019\BuildTools\MSBuild\Current\Bin\"
    $devPath2019 = "$Env:programfiles (x86)\Microsoft Visual Studio\2019\Enterprise\MSBuild\Current\Bin\"
    $proPath2019 = "$Env:programfiles (x86)\Microsoft Visual Studio\2019\Professional\MSBuild\Current\Bin\"
    $communityPath2019 = "$Env:programfiles (x86)\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin\"
    $agentPath2017 = "$Env:programfiles (x86)\Microsoft Visual Studio\2017\BuildTools\MSBuild\15.0\Bin\"
    $devPath2017 = "$Env:programfiles (x86)\Microsoft Visual Studio\2017\Enterprise\MSBuild\15.0\Bin\"
    $proPath2017 = "$Env:programfiles (x86)\Microsoft Visual Studio\2017\Professional\MSBuild\15.0\Bin\"
    $communityPath2017 = "$Env:programfiles (x86)\Microsoft Visual Studio\2017\Community\MSBuild\15.0\Bin\"
    $fallback2015Path = "${Env:ProgramFiles(x86)}\MSBuild\14.0\Bin\"
    $fallback2013Path = "${Env:ProgramFiles(x86)}\MSBuild\12.0\Bin\"
    $fallbackPath = "C:\Windows\Microsoft.NET\Framework\v4.0.30319"
		
    If ((2019 -le $MaxVersion) -And (Test-Path $agentPath2019)) { return $agentPath2019 } 
    If ((2019 -le $MaxVersion) -And (Test-Path $devPath2019)) { return $devPath2019 } 
    If ((2019 -le $MaxVersion) -And (Test-Path $proPath2019)) { return $proPath2019 } 
    If ((2019 -le $MaxVersion) -And (Test-Path $communityPath2019)) { return $communityPath2019 } 
    If ((2017 -le $MaxVersion) -And (Test-Path $agentPath2017)) { return $agentPath2017 } 
    If ((2017 -le $MaxVersion) -And (Test-Path $devPath2017)) { return $devPath2017 } 
    If ((2017 -le $MaxVersion) -And (Test-Path $proPath2017)) { return $proPat2017h } 
    If ((2017 -le $MaxVersion) -And (Test-Path $communityPath2017)) { return $communityPath2017 } 
    If ((2015 -le $MaxVersion) -And (Test-Path $fallback2015Path)) { return $fallback2015Path } 
    If ((2013 -le $MaxVersion) -And (Test-Path $fallback2013Path)) { return $fallback2013Path } 
    If (Test-Path $fallbackPath) { return $fallbackPath } 
        
    throw "Unable to find msbuild"
}

Find-MsBuild
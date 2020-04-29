function Update-AssemblyInfoVersionFile
{
    Param
    (
	[Parameter(Mandatory=$true)]
        [string]$productVersion
    )
     
    $buildNumber = $env:BUILD_BUILDNUMBER
    if ($buildNumber -eq $null)
    {
        $buildIncrementalNumber = 0
    }
    else
    {
        $splitted = $buildNumber.Split('.')
        $buildIncrementalNumber = $splitted[$splitted.Length - 1]
		$buildYear = $splitted[0].Substring(0,4)
		$buildMonth = $splitted[0].Substring(4,2)
		$buildDate = $splitted[0].Substring(6,2)
    }
      
    $SrcPath = $env:BUILD_SOURCESDIRECTORY
    Write-Verbose "Executing Update-AssemblyInfoVersionFiles in path $SrcPath for product version Version $productVersion"  -Verbose
 
    $AllVersionFiles = Get-ChildItem $SrcPath AssemblyInfo.cs -recurse
	
    $versions = $productVersion.Split('.')
    $major = $productVersion.Split('.')[0]
    $minor = $buildYear
    $patch = $buildMonth
	$buildIncrementalNumber = "$buildDate$buildIncrementalNumber"

    $assemblyVersion = "$major.$minor.$patch.$buildIncrementalNumber"
    $assemblyFileVersion = "$major.$minor.$patch.$buildIncrementalNumber"
    $assemblyInformationalVersion = "$major.$minor.$patch.$buildIncrementalNumber"
     
    Write-Verbose "Transformed Assembly Version is $assemblyVersion" -Verbose
    Write-Verbose "Transformed Assembly File Version is $assemblyFileVersion" -Verbose
    Write-Verbose "Transformed Assembly Informational Version is $assemblyInformationalVersion" -Verbose
 
    foreach ($file in $AllVersionFiles) 
    { 
        (Get-Content $file.FullName) |
        %{$_ -replace 'AssemblyVersion\("[0-9]+(\.([0-9]+|\*)){1,3}"\)', "AssemblyVersion(""$assemblyVersion"")" } |
        %{$_ -replace 'AssemblyFileVersion\("[0-9]+(\.([0-9]+|\*)){1,3}"\)', "AssemblyFileVersion(""$assemblyFileVersion"")" } |
		%{$_ -replace 'AssemblyInformationalVersion\("[0-9]+(\.([0-9]+|\*)){1,3}"\)', "AssemblyInformationalVersion(""$assemblyInformationalVersion"")" } | 
		Set-Content $file.FullName -Force
    }
  
    return $assemblyFileVersion
}

Update-AssemblyInfoVersionFile $args[0]

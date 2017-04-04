# build automation script

param(
   [switch]
   $Publish,

   [string]
   $NuGetApiKey
)

$VersionPrefix = "2"
$VersionSuffix = "3.0.0"

$SlnPath = "LivyApi.sln"
$AssemblyVersion = "$VersionPrefix.0.0.0"
$PackageVersion = "$VersionPrefix.$VersionSuffix"
Write-Host "version: $PackageVersion, assembly version: $AssemblyVersion"

function Update-ProjectVersion([string]$Path, [string]$Version)
{
   $xml = [xml](Get-Content $Path)

   if($xml.Project.PropertyGroup.Count -eq $null)
   {
      $pg = $xml.Project.PropertyGroup
   }
   else
   {
      $pg = $xml.Project.PropertyGroup[0]
   }

   $pg.Version = $PackageVersion
   $pg.FileVersion = $PackageVersion
   $pg.AssemblyVersion = $AssemblyVersion

   $xml.Save($Path)
}

function Exec($Command)
{
   Invoke-Expression $Command
   if($LASTEXITCODE -ne 0)
   {
      Write-Error "command failed (error code: $LASTEXITCODE)"
      exit 1
   }
}

# General validation
if($Publish -and (-not $NuGetApiKey))
{
   Write-Error "Please specify nuget key to publish"
   exit 1
}

# Update versioning information
Get-ChildItem *.csproj -Recurse | Where-Object {$_.Name -eq "Elastacloud.LivyApi.csproj"} | % {
   $path = $_.FullName
   Write-Host "setting version of $path"
   Update-ProjectVersion $path
}

# Restore packages
Exec "dotnet restore $SlnPath"

# Build solution
Get-ChildItem *.nupkg -Recurse | Remove-Item -ErrorAction Ignore
Exec "dotnet build $SlnPath -c release"

# publish the nugets
if($Publish.IsPresent)
{
   Write-Host "publishing nugets..."

   Get-ChildItem *.nupkg -Recurse | % {
      $path = $_.FullName
      Write-Host "pushing from $path"

      Exec "nuget push $path -Source https://www.nuget.org/api/v2/package -ApiKey $NuGetApiKey"
   }
}

Write-Host "build succeeded."
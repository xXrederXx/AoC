param(
    [Parameter(Mandatory=$true)]
    [int]$year,

    [Parameter(Mandatory=$true)]
    [ValidateRange(1,25)]
    [int]$day
)

$dayPadded = "{0:D2}" -f $day

$dayFolder = "Day$dayPadded"
$yearFolder = "Years/$year"
$folder = "$yearFolder/$dayFolder"

$projectName = "Y$year.$dayFolder"
$projectFile = "$folder/$projectName.csproj"

Write-Host "Running $projectName..."

if (!(Test-Path $projectFile)) {
    Write-Error "Project file not found: $projectFile"
    exit 1
}

# Save current directory
$originalLocation = Get-Location

try {
    # Change working directory to the project folder
    Set-Location $folder

    # Run the project
    dotnet run
}
finally {
    # Restore original directory
    Set-Location $originalLocation
}
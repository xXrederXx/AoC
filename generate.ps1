param(
    [Parameter(Mandatory=$true)]
    [int]$year,

    [Parameter(Mandatory=$true)]
    [ValidateRange(1,25)]
    [int]$day
)

$dayFolder = "Day{0:D2}" -f $day
$yearFolder = "Years/$year"
$folder = "$yearFolder/$dayFolder"

$projectName = "Y$year.$dayFolder"
$projectFile = "$folder/$projectName.csproj"

$dataFolder = "$folder/data"
$exampleFile = "$dataFolder/example.txt"
$inputFile = "$dataFolder/input.txt"

$templateProgram = "Template/Program.cs"
$targetProgram = "$folder/Program.cs"

Write-Host "Creating $projectName..."

# Ensure folders exist
New-Item -ItemType Directory -Force -Path $folder | Out-Null
New-Item -ItemType Directory -Force -Path $dataFolder | Out-Null

# Create console project
dotnet new console -n $projectName -o $folder

# Replace Program.cs with template
if (Test-Path $templateProgram) {
    Copy-Item $templateProgram $targetProgram -Force
    Write-Host "Program.cs copied from template."
}
else {
    Write-Warning "Template Program.cs not found."
}

# Add project to solution
dotnet sln add $projectFile

# Add reference to Common
dotnet add $projectFile reference "./Common/Common.csproj"

# Create data files
New-Item -ItemType File -Force -Path $exampleFile | Out-Null
New-Item -ItemType File -Force -Path $inputFile | Out-Null

Write-Host "Created:"
Write-Host "  $exampleFile"
Write-Host "  $inputFile"
Write-Host "Year $year Day $day created and added to solution."
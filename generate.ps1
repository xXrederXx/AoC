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

# Load template and replace variables
if (Test-Path $templateProgram) {

    $content = Get-Content $templateProgram -Raw

    $content = $content.Replace('${YEAR}', $year)
    $content = $content.Replace('${DAY}', $day)
    $content = $content.Replace('${DAY_PADDED}', $dayPadded)
    $content = $content.Replace('${PROJECT}', $projectName)

    Set-Content $targetProgram $content

    Write-Host "Program.cs generated from template."
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
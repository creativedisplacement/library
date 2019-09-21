param([Parameter(Mandatory=$true)][string]$versionNumber)
docker build --tag library:$versionNumber .
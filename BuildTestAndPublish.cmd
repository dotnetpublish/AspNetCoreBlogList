:: Step1: Run dotnet restore
dotnet restore

:: Build the solution
msbuild  AspNetCoreBlogList.sln /p:deployOnBuild=true;configuration=Release;

:: Run the tests
cd AspNetCoreBlogList.Test
vstest.console.exe project.json /UseVsixExtensions:true
cd ..

:: Publish the solution
PowerShell.exe -Command "& '.\src\AspNetCoreBlogList\bin\Release\PublishProfiles\AspNetCoreBlogList - Web Deploy-publish.ps1' -packOutput '.\src\AspNetCoreBlogList\bin\Release\PublishOutput\'"
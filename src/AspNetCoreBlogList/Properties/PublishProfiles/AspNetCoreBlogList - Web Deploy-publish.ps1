[cmdletbinding(SupportsShouldProcess=$true)]
param($publishProperties=@{}, $packOutput, $pubProfilePath)

# to learn more about this file visit https://go.microsoft.com/fwlink/?LinkId=524327

try{
        $env:dotnetinstallpath='C:\Program Files\dotnet\dotnet.exe'
        $publishPropertiesUserFilePath = (Join-Path (Split-Path $MyInvocation.MyCommand.Path) 'PublishProperties.user')

        if((Test-Path $publishPropertiesUserFilePath) -and $publishProperties.Count -eq 0){
        $publishPropertiesSecureString = Get-Content $publishPropertiesUserFilePath | ConvertTo-SecureString
        $BSTR = [System.Runtime.InteropServices.Marshal]::SecureStringToBSTR($publishPropertiesSecureString)
        [System.Runtime.InteropServices.Marshal]::PtrToStringAuto($BSTR) | Out-File $publishPropertiesUserFilePath
        $publishProperties  =  Import-Clixml $publishPropertiesUserFilePath
        }

        $publishProperties | Export-Clixml $publishPropertiesUserFilePath
        $publishPropertiesSecureString =  ConvertTo-SecureString (Get-Content $publishPropertiesUserFilePath | Out-String) -AsPlainText -Force
        ConvertFrom-SecureString $publishPropertiesSecureString | Out-File $publishPropertiesUserFilePath

    if ($publishProperties['ProjectGuid'] -eq $null){
        $publishProperties['ProjectGuid'] = '213684d2-8c1e-4a2d-8f7f-8ce3644317cb'
    }

    $publishModulePath = Join-Path (Split-Path $MyInvocation.MyCommand.Path) 'publish-module.psm1'
    Import-Module $publishModulePath -DisableNameChecking -Force

    # call Publish-AspNet to perform the publish operation
    Publish-AspNet -publishProperties $publishProperties -packOutput $packOutput -pubProfilePath $pubProfilePath
}
catch{
    "An error occurred during publish.`n{0}" -f $_.Exception.Message | Write-Error
}
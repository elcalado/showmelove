﻿<#
.SYNOPSIS 
Deploys a Service Fabric application type to a cluster.

.DESCRIPTION
This script deploys a Service Fabric application type to a cluster.  It is invoked by Visual Studio when deploying a Service Fabric Application project.

.NOTES
WARNING: This script file is invoked by Visual Studio.  Its parameters must not be altered but its logic can be customized as necessary.

.PARAMETER PublishProfileFile
Path to the file containing the publish profile.

.PARAMETER ApplicationPackagePath
Path to the folder of the packaged Service Fabric application.

.PARAMETER DeloyOnly
Indicates that the Service Fabric application should not be created or upgraded after registering the application type.

.PARAMETER ApplicationParameter
Hashtable of the Service Fabric application parameters to be used for the application.

.PARAMETER UnregisterUnusedApplicationVersionsAfterUpgrade
Indicates whether to unregister any unused application versions that exist after an upgrade is finished.

.PARAMETER ForceUpgrade
Indicates whether to force an upgrade to occur with hard-coded settings, ignoring any of the upgrade settings in the publish profile.

.PARAMETER UseExistingClusterConnection
Indicates that the script should make use of an existing cluster connection that has already been established in the PowerShell session.  The cluster connection parameters configured in the publish profile are ignored.

.PARAMETER OverwriteBehavior
Overwrite Behavior if an application exists in the cluster with the same name. Available Options are Never, Always, SameAppTypeAndVersion. This setting is not applicable when upgrading an application.
Never will not remove the existing application. This is the default behavior.
Always will remove the existing application even if its Application type and Version is different from the application being created. 
SameAppTypeAndVersion will remove the existing application only if its Application type and Version is same as the application being created.

.EXAMPLE
. Scripts\Deploy-FabricApplication.ps1 -ApplicationPackagePath 'pkg\Debug'

Deploy the application using the default package location for a Debug build.

.EXAMPLE
. Scripts\Deploy-FabricApplication.ps1 -ApplicationPackagePath 'pkg\Debug' -DoNotCreateApplication

Deploy the application but do not create the application instance.

.EXAMPLE
. Scripts\Deploy-FabricApplication.ps1 -ApplicationPackagePath 'pkg\Debug' -ApplicationParameter @{CustomParameter1='MyValue'; CustomParameter2='MyValue'}

Deploy the application by providing values for parameters that are defined in the application manifest.
#>

Param
(
    [String]
    $PublishProfileFile,

    [String]
    $ApplicationPackagePath,

    [Switch]
    $DeployOnly,

    [Hashtable]
    $ApplicationParameter,

    [Boolean]
    $UnregisterUnusedApplicationVersionsAfterUpgrade,

    [Boolean]
    $ForceUpgrade,

    [Switch]
    $UseExistingClusterConnection,

    [String]
    [ValidateSet('Never','Always','SameAppTypeAndVersion')]
    $OverwriteBehavior
)

function Read-XmlElementAsHashtable
{
    Param (
        [System.Xml.XmlElement]
        $Element
    )

    $hashtable = @{}
    if ($Element.Attributes)
    {
        $Element.Attributes | 
            ForEach-Object {
                $boolVal = $null
                if ([bool]::TryParse($_.Value, [ref]$boolVal)) {
                    $hashtable[$_.Name] = $boolVal
                }
                else {
                    $hashtable[$_.Name] = $_.Value
                }
            }
    }

    return $hashtable
}

function Read-PublishProfile
{
    Param (
        [ValidateScript({Test-Path $_ -PathType Leaf})]
        [String]
        $PublishProfileFile
    )

    $publishProfileXml = [Xml] (Get-Content $PublishProfileFile)
    $publishProfile = @{}

    $publishProfile.ClusterConnectionParameters = Read-XmlElementAsHashtable $publishProfileXml.PublishProfile.Item("ClusterConnectionParameters")
    $publishProfile.UpgradeDeployment = Read-XmlElementAsHashtable $publishProfileXml.PublishProfile.Item("UpgradeDeployment")

    if ($publishProfileXml.PublishProfile.Item("UpgradeDeployment"))
    {
        $publishProfile.UpgradeDeployment.Parameters = Read-XmlElementAsHashtable $publishProfileXml.PublishProfile.Item("UpgradeDeployment").Item("Parameters")
        if ($publishProfile.UpgradeDeployment["Mode"])
        {
            $publishProfile.UpgradeDeployment.Parameters[$publishProfile.UpgradeDeployment["Mode"]] = $true
        }
    }

    $publishProfileFolder = (Split-Path $PublishProfileFile)
    $publishProfile.ApplicationParameterFile = [System.IO.Path]::Combine($PublishProfileFolder, $publishProfileXml.PublishProfile.ApplicationParameterFile.Path)

    return $publishProfile
}

$LocalFolder = (Split-Path $MyInvocation.MyCommand.Path)

if (!$PublishProfileFile)
{
    $PublishProfileFile = "$LocalFolder\..\PublishProfiles\Local.xml"
}

if (!$ApplicationPackagePath)
{
    $ApplicationPackagePath = "$LocalFolder\..\pkg\Release"
}

$ApplicationPackagePath = Resolve-Path $ApplicationPackagePath

$publishProfile = Read-PublishProfile $PublishProfileFile

if (-not $UseExistingClusterConnection)
{
    $ClusterConnectionParameters = $publishProfile.ClusterConnectionParameters

    try
    {
        [void](Connect-ServiceFabricCluster @ClusterConnectionParameters)
    }
    catch [System.Fabric.FabricObjectClosedException]
    {
        Write-Warning "Service Fabric cluster may not be connected."
        throw
    }
}

$RegKey = "HKLM:\SOFTWARE\Microsoft\Service Fabric SDK"
$ModuleFolderPath = (Get-ItemProperty -Path $RegKey -Name FabricSDKPSModulePath).FabricSDKPSModulePath
Import-Module "$ModuleFolderPath\ServiceFabricSDK.psm1"

$IsUpgrade = ($publishProfile.UpgradeDeployment -and $publishProfile.UpgradeDeployment.Enabled) -or $ForceUpgrade

if ($IsUpgrade)
{
    $Action = "RegisterAndUpgrade"
    if ($DeployOnly)
    {
        $Action = "Register"
    }
    
    $UpgradeParameters = $publishProfile.UpgradeDeployment.Parameters

    if ($ForceUpgrade)
    {
        # Warning: Do not alter these upgrade parameters. It will create an inconsistency with Visual Studio's behavior.
        $UpgradeParameters = @{ UnmonitoredAuto = $true; Force = $true }
    }

    Publish-UpgradedServiceFabricApplication -ApplicationPackagePath $ApplicationPackagePath -ApplicationParameterFilePath $publishProfile.ApplicationParameterFile -Action $Action -UpgradeParameters $UpgradeParameters -ApplicationParameter $ApplicationParameter -UnregisterUnusedVersions:$UnregisterUnusedApplicationVersionsAfterUpgrade -ErrorAction Stop
}
else
{
    $Action = "RegisterAndCreate"
    if ($DeployOnly)
    {
        $Action = "Register"
    }
    
    Publish-NewServiceFabricApplication -ApplicationPackagePath $ApplicationPackagePath -ApplicationParameterFilePath $publishProfile.ApplicationParameterFile -Action $Action -ApplicationParameter $ApplicationParameter -OverwriteBehavior $OverwriteBehavior -ErrorAction Stop
}
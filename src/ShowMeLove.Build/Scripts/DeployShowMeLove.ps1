cd "C:\Users\marrobi\onedrive\documents\visual studio 2015\Projects\AzureResourceGroup3\AzureResourceGroup3"

$version ="v02"
$KeyVaultName = "vaultShowMeLove$version"
$certPassword = "Passw0rd!"
$rgName = "ShowMeLove$version"

Login-AzureRMAccount
. .\Scripts\DeployDB.ps1

New-AzureRmResourceGroup -Name $rgName -Location WestUS
New-AzureRmKeyVault -VaultName $KeyVaultName -ResourceGroupName $rgName -Location WestUS -EnabledForDeployment

$clusterCert = .\New-ServiceFabricClusterCertificate.ps1 -Password $certPassword -CertDNSName "ShowMeLove" -KeyVaultName $KeyVaultName -KeyVaultSecretName ServiceFabric

# NEED TO GET OUTPUT


# deploy service fabric using json
$OptionalParameters = New-Object -TypeName Hashtable

# create service fabric cluster
$TemplateFile = "c:\users\marrobi\onedrive\documents\visual studio 2015\Projects\AzureResourceGroup3\AzureResourceGroup3\Templates\ServiceFabricCluster.json"
 
 
 $OptionalParameters.Add("certificateThumbprint",  "E11C12D0C710B956CA7D2C0F15054A9728AD662F")
  $OptionalParameters.Add("sourceVaultValue", $KeyVaultName)
 $OptionalParameters.Add("certificateUrlValue", "https://vaultshowmelovev01.vault.azure.net:443/secrets/ServiceFabric/ab96dfb190594a7e80b5ce48294a61e2")
     $OptionalParameters.Add("clusterLocation", "West US")
        $OptionalParameters.Add( "adminUserName","ShowMeLove")
      $OptionalParameters.Add( "adminPassword",(ConvertTo-SecureString -String Passw0rd! -AsPlainText -Force))
    

New-AzureRmResourceGroupDeployment -Name ((Get-ChildItem $TemplateFile).BaseName + '-' + ((Get-Date).ToUniversalTime()).ToString('MMdd-HHmm')) `
                                   -ResourceGroupName $rgName `
                                   -TemplateFile $TemplateFile `
                                  @OptionalParameters `
                                   -Force -Verbose
# create sql db

$TemplateFile = "c:\users\marrobi\onedrive\documents\visual studio 2015\Projects\AzureResourceGroup3\AzureResourceGroup3\Templates\SQLDB.json"
 
     $OptionalParameters.Add( "adminUserName","ShowMeLove")
      $OptionalParameters.Add( "adminPassword",(ConvertTo-SecureString -String Passw0rd! -AsPlainText -Force))
    
New-AzureRmResourceGroupDeployment -Name ((Get-ChildItem $TemplateFile).BaseName + '-' + ((Get-Date).ToUniversalTime()).ToString('MMdd-HHmm')) `
                                   -ResourceGroupName $rgName `
                                   -TemplateFile $TemplateFile `
                                  @OptionalParameters `
                                   -Force -Verbose



# import data

DeployDB -sqlserver srvshowmelove -dacpac $dacpac -dbname dbshowmelove


# create event hub
. .\Scripts\CreateEventHub.ps1

# HELP!!
CreateEventHub 

Remove-AzureRmResourceGroup -Name $rgName -Force
ipconfig /flushdns
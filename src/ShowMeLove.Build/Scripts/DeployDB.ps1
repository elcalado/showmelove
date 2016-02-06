Function DeployDB {

param( 
    [string]$sqlserver = $( throw "Missing required parameter sqlserver"), 
    [string]$dacpac = $( throw "Missing required parameter dacpac"), 
    [string]$dbname = $( throw "Missing required parameter dbname") )

Write-Host "Deploying the DB with the following settings" 
Write-Host "sqlserver:   $sqlserver" 
Write-Host "dacpac: $dacpac" 
Write-Host "dbname: $dbname"

# load in DAC DLL, This requires config file to support .NET 4.0.
# change file location for a 32-bit OS 
#make sure you
add-type -path "C:\Program Files (x86)\Microsoft SQL Server\110\DAC\bin\Microsoft.SqlServer.Dac.dll"

# Create a DacServices object, which needs a connection string 
$dacsvcs = new-object Microsoft.SqlServer.Dac.DacServices "server=$sqlserver"

# register event. For info on this cmdlet, see http://technet.microsoft.com/en-us/library/hh849929.aspx 
register-objectevent -in $dacsvcs -eventname Message -source "msg" -action { out-host -in $Event.SourceArgs[1].Message.Message } | Out-Null

# Load dacpac from file & deploy database
$dp = [Microsoft.SqlServer.Dac.DacPackage]::Load($dacpac) 
$dacsvcs.Deploy($dp, $dbname, $true) 

# clean up event 
unregister-event -source "msg" 

}
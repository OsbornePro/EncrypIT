# In order for EFS to work on Network Shares the devices need to be trusted for delegation. More info at the link below
# RESOURCE: https://docs.microsoft.com/en-us/previous-versions/windows/it-pro/windows-xp/bb457116(v=technet.10)?redirectedfrom=MSDN
#
# This is a simple script for setting up devices to be trusted for delegation with Kerberos delegation for devices in a domain environment

$CutOffDate = (Get-Date).AddDays(-60)

Write-Output "[*] Getting a list of enabled AD devices that have been signed into in the last 60 days and trusting them for Kerberos delegation"
Get-ADComputer -Properties LastLogonDate -Filter {LastLogonDate -gt $CutOffDate -and Enabled -eq $True} | Set-ADAccountControl -TrustedForDelegation $True
# YOU CAN ADD MORE FILTERS TO ABOVE COMMAND TO INCLUDE FOR EXAMPLE A NAMING CONVENTION
# Get-ADComputer -Properties LastLogonDate -Filter {Name -like "DESKTOP-*" -and LastLogonDate -gt $CutOffDate -and Enabled -eq $True} | Set-ADAccountControl -TrustedForDelegation $True

# Below roughly created command can be used if you want to define the individual SPNs on a device. More restrictive but requires more management
# Get-ADComputer -Properties LastLogonDate,servicePrincipalName | Select-Object -Property Name,servicePrincipalName | Set-ADComputer -Add @{'msDS-AllowedToDelegateTo'=@("$_.ServicePrincipalName")}

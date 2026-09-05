# Getting Started

## Requirements

EncrypIT is a Windows application built around Microsoft's Encrypting File System (EFS). EFS is an NTFS feature, so the files or folders you work with must be on a supported Windows/NTFS environment and EFS must be available under your system or domain policy.

## Check whether EFS is enabled

Run this from PowerShell:

```powershell
If ((Get-ItemProperty -Path 'HKLM:\SOFTWARE\Microsoft\Windows NT\CurrentVersion\EFS' -Name "EfsConfiguration" -ErrorAction SilentlyContinue) -eq 1) {
    Write-Output "EFS is Disabled"
} Else {
    Write-Output "EFS is Enabled"
}
```

## Download EncrypIT

Use the project's [GitHub Releases](https://github.com/OsbornePro/EncrypIT/releases) page for published builds. The project is also available through [SourceForge](https://sourceforge.net/projects/encrypit/files/latest/download).

EncrypIT is intended to run as a standalone application; no traditional installer is required for the published executable.

## Before encrypting important data

1. Confirm EFS is permitted and working in your environment.
2. Understand which Windows account/certificate will own the encrypted data.
3. Back up your EFS certificate and private key.
4. Store the PFX backup and its password securely and separately.
5. In managed environments, confirm your organization's data recovery and certificate policies.

Continue with [Using EncrypIT](using-encryptit.md).

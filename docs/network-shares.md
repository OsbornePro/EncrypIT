# EFS and Network Shares

Remote EFS is more complicated than encrypting a local NTFS file. Domain authentication, delegation, server configuration, profiles, certificates, and the storage protocol can all affect behavior.

## Kerberos delegation

The repository includes `TrustComputersForDelegation.ps1` as an administrative helper for environments that require delegation for computers hosting network shares.

```{warning}
Delegation changes have security implications. Review the script and your Active Directory design before running it, and apply delegation only where your environment actually requires it.
```

## Operational guidance

Test remote EFS with non-production data first. Confirm where encryption/decryption occurs, which account performs the operation, how certificates are made available, and what recovery path exists if a user key is lost.

The behavior of older Windows documentation may not exactly match modern Windows Server deployments, so validate against current Microsoft documentation and your organization's policies.

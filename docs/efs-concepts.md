# EFS Concepts & Limitations

EncrypIT provides a friendlier interface for Windows Encrypting File System operations; it does not replace the Windows EFS security model.

## Certificates matter

EFS uses certificate/private-key material to protect data. Access to an NTFS path alone does not guarantee the ability to decrypt an EFS-protected file. Protect and back up the relevant private keys.

## Users versus groups

EFS sharing is based on user certificates rather than ordinary NTFS group permissions. EncrypIT can help work with multiple users, but a group is not itself an EFS decryption certificate.

## Folders behave differently from individual files

Marking a folder for encryption influences files subsequently created or placed in that folder. Be deliberate when using recursive operations and verify the resulting state rather than assuming every child item has the same EFS status.

## Recovery planning

For business or domain environments, establish a recovery process before deploying EFS broadly. A Data Recovery Agent or another organization-approved recovery strategy can prevent a lost user key from becoming permanent data loss.

## Profiles and certificates

EFS operations depend on the user's Windows profile and certificate/private-key availability. Roaming, remote, or multi-device scenarios require additional planning so the correct credentials and keys are available where needed.

For authoritative platform behavior, consult Microsoft's current Windows security and EFS documentation for the Windows versions deployed in your environment.

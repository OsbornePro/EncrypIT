# Security & Data Safety

Encryption tooling deserves conservative operational practices.

## Protect private keys

An EFS private key can grant access to protected data. Store exported PFX files securely, use strong passwords, and limit access to backups.

## Keep a recovery path

Do not make important data dependent on a single unbacked-up user certificate. Organizations should define an approved recovery strategy before using EFS at scale.

## Test first

Use disposable files when validating EncrypIT, certificate templates, recursive behavior, remote shares, or delegation changes. Confirm both encryption **and recovery/decryption** before applying the workflow to important data.

## Permissions still matter

EFS encryption and NTFS permissions solve different problems. Decrypting a file does not automatically grant filesystem access, and filesystem permission alone does not provide an EFS private key.

## Report vulnerabilities

See the repository's [SECURITY.md](https://github.com/OsbornePro/EncrypIT/blob/main/SECURITY.md) for the project's security-reporting guidance.

# Back Up an EFS Certificate

Backing up the EFS private key is one of the most important steps when using file encryption.

## Create a backup with EncrypIT

1. Open EncrypIT.
2. Select **Backup Key**.
3. Supply a strong password when prompted.
4. Choose where to save the `.pfx` backup.
5. Secure both the PFX file and its password.

```{image} img/BackupKey.png
:alt: Back up an EFS certificate with EncrypIT
:class: screenshot
```

```{important}
Do not store the only PFX backup beside the encrypted files it is intended to recover. Treat the PFX and its password as sensitive credentials.
```

## Certificate discovery

The current application source first searches the user's certificate store for an EFS certificate associated with the **Basic EFS** template. If that search does not produce a certificate, the application includes a fallback based on key usage.

Administrators using a custom certificate template should review the current `Form1.cs` implementation before deployment rather than relying on a historical source-code line number.

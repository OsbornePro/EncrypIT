# Using EncrypIT

## Encrypt a file or directory

1. Open EncrypIT.
2. Drag one or more files or folders from File Explorer into the file area.
3. Select **Recursive** when you intend to process supported contents beneath a selected folder.
4. Select **Encrypt**.
5. Review **Status Information** for the result.

```{image} img/Encrypt.png
:alt: EncrypIT encryption results
:class: screenshot
```

## Decrypt a file or directory

1. Add the files or folders to EncrypIT.
2. Select **Recursive** when appropriate.
3. Select **Decrypt**.
4. Review the status output.

After successful decryption, access is governed by normal filesystem permissions rather than EFS encryption for that item.

```{image} img/Decrypt.png
:alt: EncrypIT decryption results
:class: screenshot
```

## Grant a domain user access

1. Add the encrypted file to EncrypIT.
2. Enter the supported user identity in the user field. Put multiple users on separate lines.
3. Select **Grant Access**.
4. Confirm the result in the status area.

```{image} img/GrantAccess.png
:alt: Grant EFS access
:class: screenshot
```

EFS file sharing is certificate-based. See [EFS Concepts & Limitations](efs-concepts.md) before designing a sharing workflow.

## Revoke a user's access

1. Add the encrypted file to EncrypIT.
2. Enter the user identity requested by the application.
3. Select **Revoke Access**.
4. Review the status output.

```{image} img/RevokeAccess.png
:alt: Revoke EFS access
:class: screenshot
```

## View encryption information

Add a file or folder and select **Get Encrypted File Info**. This is useful for checking EFS state and reviewing the identities associated with an encrypted file.

```{image} img/GetInfo.png
:alt: EFS file information
:class: screenshot
```

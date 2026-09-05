# EncrypIT

<div class="hero">

# Windows EFS, made easier

**EncrypIT** is a Windows desktop utility designed to simplify common Encrypting File System (EFS) tasks: encrypting and decrypting files, reviewing encryption information, managing user access, and backing up an EFS certificate.

[GitHub repository](https://github.com/OsbornePro/EncrypIT) · [Releases](https://github.com/OsbornePro/EncrypIT/releases) · [Report an issue](https://github.com/OsbornePro/EncrypIT/issues)

</div>

```{image} img/EncrypIT.png
:alt: EncrypIT application
:class: screenshot
:align: center
```

## What EncrypIT does

<div class="doc-card">

**Encrypt & decrypt** — Drag files or directories into the application and perform EFS operations without memorizing command-line syntax.

</div>

<div class="doc-card">

**Manage access** — Grant supported domain users access to encrypted files or revoke previously granted access.

</div>

<div class="doc-card">

**Inspect EFS information** — Review encryption details, including users able to decrypt a selected file and recovery information.

</div>

<div class="doc-card">

**Back up your key** — Export the EFS certificate and private key to a password-protected PFX file.

</div>

```{warning}
EFS depends on encryption certificates and private keys. Back up your EFS certificate before relying on EFS for important data. Losing the only usable private key can make encrypted data inaccessible.
```

## Start here

New users should begin with [Getting Started](getting-started.md), then review [EFS Concepts & Limitations](efs-concepts.md) before encrypting important files.

```{toctree}
:maxdepth: 2
:caption: Documentation

getting-started
using-encryptit
efs-concepts
certificate-backup
network-shares
screenshots
building-from-source
security
contributing
```

# Building from Source

The repository contains the Visual Studio solution `EncrypIT.sln` and the C# project under `EncrypIT/`.

## Typical workflow

1. Clone the repository.
2. Open `EncrypIT.sln` in a compatible Visual Studio installation on Windows.
3. Restore any required project dependencies.
4. Select the desired build configuration.
5. Build the solution and test EFS operations only against disposable test data first.

```bash
git clone https://github.com/OsbornePro/EncrypIT.git
cd EncrypIT
```

Because EncrypIT interacts with Windows EFS, meaningful functional testing should be performed on a Windows system configured for the EFS scenarios being tested.

## Source layout

- `EncrypIT/` — Windows application source and assets.
- `TrustComputersForDelegation.ps1` — administrative helper related to delegation scenarios.
- `docs/` — this documentation site.
- `.readthedocs.yaml` — Read the Docs build configuration.

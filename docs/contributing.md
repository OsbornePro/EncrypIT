# Contributing

Contributions that improve reliability, usability, documentation, testing, or maintainability are welcome.

## Before submitting changes

1. Read the repository's [CONTRIBUTING.md](https://github.com/OsbornePro/EncrypIT/blob/main/CONTRIBUTING.md).
2. Keep changes focused and explain the problem they solve.
3. Test application changes on Windows using disposable data.
4. Never commit real private keys, PFX files, passwords, certificates containing sensitive material, or production data.
5. Update documentation when behavior or user-facing workflows change.

## Documentation changes

The documentation uses Sphinx with MyST Markdown and the Furo theme. Install the documentation requirements and build locally with:

```bash
python -m pip install -r docs/requirements.txt
sphinx-build -b html docs docs/_build/html
```

Then open `docs/_build/html/index.html` in a browser and check navigation, code blocks, screenshots, and links before committing.

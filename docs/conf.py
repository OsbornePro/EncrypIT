import os

project = "EncrypIT"
author = "Robert H. Osborne"
copyright = "2026, Robert H. Osborne"

extensions = [
    "myst_parser",
    "sphinx_copybutton",
]

source_suffix = {
    ".rst": "restructuredtext",
    ".md": "markdown",
}
master_doc = "index"
exclude_patterns = ["_build", "Thumbs.db", ".DS_Store"]

html_theme = "furo"
html_title = "EncrypIT Documentation"
html_logo = "img/LogoSymbol.png"
html_favicon = "img/LogoSymbol.png"
html_static_path = ["_static"]
html_css_files = ["custom.css"]
html_baseurl = os.environ.get("READTHEDOCS_CANONICAL_URL", "")

html_theme_options = {
    "source_repository": "https://github.com/OsbornePro/EncrypIT/",
    "source_branch": "main",
    "source_directory": "docs/",
}

html_context = {
    "display_github": True,
    "github_user": "OsbornePro",
    "github_repo": "EncrypIT",
    "github_version": "main",
    "conf_py_path": "/docs/",
}

myst_enable_extensions = ["colon_fence", "deflist", "fieldlist"]
copybutton_prompt_text = r">>> |\.\.\. |\$ |PS> "
copybutton_prompt_is_regexp = True

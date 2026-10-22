#!/bin/sh
# ---------------------------------------------------------------------------------------------
# Factorio C# Modder - Linux and macOS installer
#
# Installs the tree `factorio-mod dist` assembles, puts a `factorio-mod` launcher on PATH, and
# offers to add the "Factorio mod (C#)" project template to `dotnet new` - which is the list
# Rider's New Solution dialog shows.
#
#   ./install.sh                          # asks before touching PATH and the template list
#   ./install.sh --prefix ~/.local        # no sudo, everything under your home
#   ./install.sh --yes                    # answers yes to both questions (for CI or a script)
#   ./install.sh --no-template --no-path  # just the files
#   ./install.sh --uninstall
#
# The payload is the folder this script is in, or `--from DIR`; it must contain VERSION and
# bin/factorio-mod, so run `factorio-mod dist --runtime linux-x64` (or osx-arm64) first.
# ---------------------------------------------------------------------------------------------
set -eu

program=factorio-mod
version=0.0.0-dev
payload=""
prefix=""
template=ask
path=ask
uninstall=no

usage() {
    cat <<'EOF'
usage: install.sh [options]

  --prefix DIR      install under DIR (default: /usr/local as root, ~/.local otherwise)
  --from DIR        the dist folder to install (default: this script's folder)
  --template        install the project template without asking
  --no-template     do not install the project template
  --path            add the launcher's folder to PATH without asking
  --no-path         do not touch PATH
  --yes             --template and --path
  --uninstall       remove an installation made by this script
  -h, --help        this text
EOF
}

die() { printf 'install: %s\n' "$*" >&2; exit 1; }
say() { printf '%s\n' "$*"; }

# Reads an answer; when there is no terminal (a CI job, a pipe) the default is used, because a
# release build must never hang waiting for a keypress.
confirm() {
    question=$1
    default=$2
    if [ ! -t 0 ]; then
        say "$question (no terminal: ${default})"
        [ "$default" = yes ]
        return
    fi
    printf '%s [%s] ' "$question" "$([ "$default" = yes ] && echo Y/n || echo y/N)"
    read -r answer || answer=""
    case "$answer" in
        "") [ "$default" = yes ] ;;
        y|Y|yes|YES) return 0 ;;
        *) return 1 ;;
    esac
}

while [ $# -gt 0 ]; do
    case "$1" in
        --prefix) [ $# -ge 2 ] || die "--prefix needs a folder"; prefix=$2; shift 2 ;;
        --prefix=*) prefix=${1#*=}; shift ;;
        --from) [ $# -ge 2 ] || die "--from needs a folder"; payload=$2; shift 2 ;;
        --from=*) payload=${1#*=}; shift ;;
        --template) template=yes; shift ;;
        --no-template) template=no; shift ;;
        --path) path=yes; shift ;;
        --no-path) path=no; shift ;;
        --yes|-y) template=yes; path=yes; shift ;;
        --uninstall) uninstall=yes; shift ;;
        -h|--help) usage; exit 0 ;;
        *) die "unknown option '$1' (try --help)" ;;
    esac
done

if [ -z "$prefix" ]; then
    if [ "$(id -u)" -eq 0 ]; then prefix=/usr/local; else prefix=$HOME/.local; fi
fi
# Absolute, so the launcher script can be written with a fixed path in it.
case "$prefix" in
    /*) ;;
    *) prefix="$(pwd)/$prefix" ;;
esac
lib="$prefix/lib/$program"
launcher="$prefix/bin/$program"

remove_template() {
    if ! command -v dotnet >/dev/null 2>&1; then
        say "dotnet is not on PATH; nothing to remove from its template list."
        return 0
    fi
    dotnet new uninstall "$lib/templates/dotnet/factorio-mod" >/dev/null 2>&1 &&
        say "Removed the project template." || say "The project template was not installed."
}

if [ "$uninstall" = yes ]; then
    [ -d "$lib" ] || die "nothing installed at $lib"
    remove_template
    rm -rf "$lib"
    rm -f "$launcher"
    say "Removed $lib and $launcher."
    say "PATH entries and shell profile lines were left alone; remove them by hand if you added them."
    exit 0
fi

payload=${payload:-$(cd "$(dirname "$0")" && pwd)}
[ -f "$payload/VERSION" ] || die "'$payload' is not a dist folder: no VERSION file.
Run 'factorio-mod dist --output dist --runtime linux-x64' first, or pass --from DIR."
[ -x "$payload/bin/$program" ] || die "'$payload' has no executable bin/$program.
The dist was built for another platform; rebuild it with --runtime linux-x64 or osx-arm64."
version=$(tr -d '[:space:]' <"$payload/VERSION")

say "$program $version"
say "from    $payload"
say "to      $lib"

mkdir -p "$lib" "$prefix/bin"
# The payload is the repository's tree, so the copy is the same layout the tool expects to find
# its root in. `build/` is output from a previous run; it does not belong in an installation.
rm -rf "$lib/api" "$lib/bin" "$lib/config" "$lib/docs" "$lib/mods" "$lib/src" "$lib/templates" "$lib/tools" "$lib/vendor"
cp -R "$payload/." "$lib/"
rm -rf "$lib/build" "$lib/plugin"

# A small wrapper rather than a symlink: the path is absolute and quoted, so a prefix with a space
# in it still works, and `factorio-mod` stays a plain file that any PATH scan will find.
cat >"$launcher" <<EOF
#!/bin/sh
# Written by the Factorio C# Modder installer.
exec "$lib/bin/$program" "\$@"
EOF
chmod 755 "$launcher"
say "wrote   $launcher"

if ! "$launcher" --version >/dev/null 2>&1; then
    die "the launcher does not run. Check that the .NET 8 runtime is installed:
    $lib/bin/$program --version"
fi

if [ "$path" = ask ]; then
    if confirm "Add $prefix/bin to PATH?" yes; then path=yes; else path=no; fi
fi
if [ "$path" = yes ]; then
    case ":$PATH:" in
        *":$prefix/bin:"*) say "PATH already contains $prefix/bin." ;;
        *)
            profile=""
            case "${SHELL:-}" in
                */zsh) profile="$HOME/.zshrc" ;;
                */bash) profile="$HOME/.bashrc" ;;
            esac
            [ -n "$profile" ] || profile="$HOME/.profile"
            marker="# factorio-mod (added by install.sh)"
            if [ -f "$profile" ] && grep -Fq "$marker" "$profile"; then
                say "PATH line already in $profile."
            else
                {
                    printf '\n%s\n' "$marker"
                    printf 'case ":$PATH:" in *":%s:"*) ;; *) PATH="%s:$PATH" ;; esac\n' "$prefix/bin" "$prefix/bin"
                    printf 'export PATH\n'
                } >>"$profile"
                say "Added $prefix/bin to PATH in $profile (open a new terminal to pick it up)."
            fi
            ;;
    esac
fi

if [ "$template" = ask ]; then
    if command -v dotnet >/dev/null 2>&1; then
        say ""
        say "The project template makes \"Factorio mod (C#)\" appear in Rider's New Solution dialog"
        say "and adds \`dotnet new factorio-mod\`."
        if confirm "Add the template to Rider / dotnet new?" yes; then template=yes; else template=no; fi
    else
        say ""
        say "The .NET 8 SDK is not on PATH, so the project template was skipped."
        say "Mods are compiled with it, so install it from https://dotnet.microsoft.com/download/dotnet/8.0"
        say "and then run: $program template"
        template=no
    fi
fi
if [ "$template" = yes ]; then
    # Run from / so the tool picks its own installation as the root. It prefers the current
    # directory when that looks like a checkout, which is right for a developer and wrong here.
    if (cd / && "$launcher" template); then
        say "Template installed."
    else
        say "The template could not be installed; run '$program template' to try again." >&2
    fi
fi

say ""
say "Done. Next:"
say "  $program doctor            # what the tool found on this machine"
say "  $program new MyMod         # a mod project, then Build in Rider or:"
say "  $program build MyMod       # -> build/mods/MyMod_0.1.0.zip"
say "  $program --help            # everything else"

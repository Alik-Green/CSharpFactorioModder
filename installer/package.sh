#!/bin/sh
# ---------------------------------------------------------------------------------------------
# Packs the Linux and macOS release archives: the dist tree with install.sh at its root, so a
# download is one file that unpacks and installs.
#
#   ./installer/package.sh 1.2.3 linux-x64
#   ./installer/package.sh 1.2.3 osx-arm64
#
# Output: release/FactorioCSharpModder-<version>-<rid>.tar.gz
# CI runs this; see docs/RELEASING.md. Run from the repository root, or anywhere - the script
# finds the root itself.
# ---------------------------------------------------------------------------------------------
set -eu

version=${1:-}
runtime=${2:-linux-x64}
[ -n "$version" ] || { echo "usage: package.sh <version> [linux-x64|osx-arm64]" >&2; exit 2; }
version=${version#v}

root=$(cd "$(dirname "$0")/.." && pwd)
out="$root/release"
work="$root/build/package-$runtime"
name="FactorioCSharpModder-$version-$runtime"

cd "$root"

# The Rider plugin is optional: without plugin/, the tool and the template still do everything.
plugin=""
if [ -d "$root/plugin" ]; then plugin="--plugin plugin"; fi

echo "== dist =="
if [ -x "$root/tools/Factorio.ModTool/bin/Release/net8.0/factorio-mod" ]; then
    "$root/tools/Factorio.ModTool/bin/Release/net8.0/factorio-mod" dist --output dist --version "$version" --runtime "$runtime" $plugin
else
    dotnet "$root/tools/Factorio.ModTool/bin/Release/net8.0/factorio-mod.dll" dist --output dist --version "$version" --runtime "$runtime" $plugin
fi

echo "== package =="
rm -rf "$work"
mkdir -p "$work/$name" "$out"
# The payload, then the installer next to the folder it installs.
cp -R "$root/dist/." "$work/$name/"
rm -rf "$work/$name/build"
cp "$root/installer/install.sh" "$work/$name/install.sh"
chmod 755 "$work/$name/install.sh"
printf 'Factorio C# Modder %s (%s)\n\nInstall:\n  ./install.sh\n\nIt asks before adding itself to PATH and before adding the project template to\nRider / dotnet new. --help lists the options.\n' \
    "$version" "$runtime" >"$work/$name/INSTALL.txt"

tar -czf "$out/$name.tar.gz" -C "$work" "$name"
echo "wrote   $out/$name.tar.gz"
echo "size    $(du -h "$out/$name.tar.gz" | cut -f1)"

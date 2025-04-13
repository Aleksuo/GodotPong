#!/bin/bash

export_name="godotpong"
targets=("$@")

if [ ${#targets[@]} -eq 0 ]; then
    echo "No parameters given, defaulting to all platforms"
    targets=("windows" "linux")
fi


echo "Building docker image..."
docker build -t godotbuild:latest .

echo "Starting container and mounting project volume..."
docker run --name godotbuild -t -d --rm -v ${PWD}:/usr/src/project -w /usr/src/project godotbuild:latest

echo "Running export..."

for param in "${targets[@]}"; do
  if [ $param == "windows" ]; then
    mkdir -p ./.build/windows
    echo "Running export for Windows"
    windows_file_path="./.build/windows/${export_name}.exe"
    docker exec godotbuild  godot --headless --verbose --export-release "Windows Desktop" ${windows_file_path}
  fi
  if [ $param == "linux" ]; then
    mkdir -p ./.build/linux
    echo "Running export for Linux"
    linux_file_path="./.build/linux/${export_name}"
    docker exec godotbuild  godot --headless --verbose --export-release "Linux/X11" ${linux_file_path}
  fi
done

echo "Shutting down the build container..."
docker stop godotbuild
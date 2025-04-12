FROM ubuntu:24.10

# Install all tools
RUN apt-get update && apt-get install -y git \
    dotnet-sdk-8.0 \
    wget \
    unzip

# Download and install Godot and export templates
RUN wget https://github.com/godotengine/godot/releases/download/4.4.1-stable/Godot_v4.4.1-stable_mono_linux_x86_64.zip && \
    wget https://github.com/godotengine/godot/releases/download/4.4.1-stable/Godot_v4.4.1-stable_mono_export_templates.tpz && \
    unzip Godot_v4.4.1-stable_mono_linux_x86_64.zip && \
    unzip Godot_v4.4.1-stable_mono_export_templates.tpz && \
    mv Godot_v4.4.1-stable_mono_linux_x86_64/Godot_v4.4.1-stable_mono_linux.x86_64 /usr/local/bin/godot && \
    mv Godot_v4.4.1-stable_mono_linux_x86_64/GodotSharp /usr/local/bin/GodotSharp && \
    mkdir -p /root/.local/share/godot/export_templates/4.4.1.stable.mono && \
    mv templates/* /root/.local/share/godot/export_templates/4.4.1.stable.mono && \
    rm Godot_v4.4.1-stable_mono_linux_x86_64.zip && \
    rm Godot_v4.4.1-stable_mono_export_templates.tpz

CMD ["/bin/bash"]
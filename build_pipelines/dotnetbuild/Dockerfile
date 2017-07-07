# This builder will ensure that the .NET Core project that was
# published to the /workspace directory has a Dockerfile. If one
# exists it will be used, otherwise a new one will be created.
FROM gcr.io/google_appengine/debian8

# We need python to run the builder script.
RUN apt-get update \
    && apt-get install -y \
        ca-certificates \
        curl \
        libc6 \
        libcurl3 \
        libgcc1 \
        libicu52 \
        liblttng-ust0 \
        libssl1.0.0 \
        libstdc++6 \
        libtinfo5 \
        libunwind8 \
        libuuid1 \
        python \
        python-pip \
        zlib1g \
    && apt-get clean

# Install the PyYaml library.
RUN pip install PyYaml

# Add the prepare build script.
RUN mkdir -p /builder
ADD . /builder

# Install the following SDKs:
# + 1.0.3 with SDK Preview 2 build 3156, to support .NET Core 1.0.5 with project.json solutions.
# + 1.0.4, which supporst .csproj projects for .NET Core 1.0.5 and 1.1.2.
RUN mkdir -p /usr/share/dotnet && \
    curl -sL https://download.microsoft.com/download/E/7/8/E782433E-7737-4E6C-BFBF-290A0A81C3D7/dotnet-dev-debian-x64.1.0.4.tar.gz | tar -xz -C /usr/share/dotnet/ && \
    curl -sL https://go.microsoft.com/fwlink/?LinkID=836302 | tar -xz -C /usr/share/dotnet && \
    ln -s /usr/share/dotnet/dotnet /usr/bin/dotnet

# Add the configs directory and invoke dotnet.
ADD ./configs /configs
RUN cd /configs/config-1.0.0/ && dotnet new && \
    cd /configs/config-1.0.4/ && dotnet new && \
    rm -rf /configs

WORKDIR /workspace
ENTRYPOINT [ "/builder/prepare_project.py" ]

FROM mcr.microsoft.com/dotnet/sdk:5.0
WORKDIR /app
COPY Source/Model/ src/Model/
COPY Source/ModelTests/ src/ModelTests/

# Restore projects.
RUN dotnet restore src/Model/
RUN dotnet restore src/ModelTests/

# Build projects.
RUN dotnet build --nologo --no-restore src/Model/
RUN dotnet build --nologo --no-restore src/ModelTests/

# Run ModelTests.
ENTRYPOINT dotnet test --no-build --nologo -v quiet src/ModelTests/
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS sdk

# Expose port and listen it in ASP.NET Core.
EXPOSE 80
ENV ASPNETCORE_URLS=http://+:80

WORKDIR /app
COPY Source/Model/ src/Model/
COPY Source/WebAPI/ src/WebAPI/

# Restore projects.
RUN dotnet restore src/Model/
RUN dotnet restore src/WebAPI/

# Build projects.
RUN dotnet build -c Release --nologo --no-restore src/Model/
RUN dotnet build -c Release --nologo --no-restore src/WebAPI/

# Publish WebAPI.
RUN dotnet publish -o publish/ -c Release --no-build --nologo src/WebAPI/

# Use single ASP.NET Core runtime to reduce image size.
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=sdk app/publish/ publish/

# Run WebAPI.
ENTRYPOINT dotnet publish/WebAPI.dll
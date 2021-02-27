FROM mcr.microsoft.com/dotnet/sdk:5.0 AS sdk
WORKDIR /app
COPY Source/ src/

# Restore and build entire solution.
RUN dotnet restore src/
RUN dotnet build -c Release --nologo --no-restore src/
RUN dotnet build --nologo --no-restore src/ModelTests/

# Run tests.
RUN dotnet test --no-build --nologo -v quiet src/ModelTests/

# Publish WebAPI.
RUN dotnet publish -o publish/ -c Release --no-build --nologo src/WebAPI/

# Use single ASP.NET Core runtime to reduce image size.
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=sdk app/publish/ publish/

# Run WebAPI.
# Using CMD instead of ENTRYPOINT and such ASPNETCORE_URLS requested from Heroku Dyno.
CMD ASPNETCORE_URLS=http://*:$PORT dotnet publish/WebAPI.dll
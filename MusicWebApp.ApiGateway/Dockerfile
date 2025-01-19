#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy the project file
COPY ["MusicWebApp.ApiGateway/MusicWebApp.ApiGateway.csproj", "MusicWebApp.ApiGateway/"]
RUN dotnet restore "./MusicWebApp.ApiGateway/MusicWebApp.ApiGateway.csproj"

# Copy the rest of the source code
COPY . .

WORKDIR "/src/MusicWebApp.ApiGateway"
RUN dotnet build "./MusicWebApp.ApiGateway.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MusicWebApp.ApiGateway.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app

# Copy the published output
COPY --from=publish /app/publish .

# Copy ocelot.json configuration into the container
COPY ["MusicWebApp.ApiGateway/ocelot.json", "./"]

ENTRYPOINT ["dotnet", "MusicWebApp.ApiGateway.dll"]

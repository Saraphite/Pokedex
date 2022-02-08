# syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY /src/ ./src
COPY /tests/ ./tests
RUN dotnet restore "src/Pokedex.sln"

# # Copy everything else and build
RUN dotnet publish -c Release -o out "src/Pokedex.sln"

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "Pokedex.dll"]
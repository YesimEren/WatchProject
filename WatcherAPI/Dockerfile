# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copy the project files
COPY ./WatcherAPI/WatcherAPI.csproj .

WORKDIR /src
# Restore dependencies
RUN dotnet restore 

# Copy the rest of the application code
COPY . .

# Build the project
RUN dotnet build ./WatcherAPI/WatcherAPI.csproj -c Release -o /app/build

# Stage 2: Publish the application
FROM build AS publish
RUN dotnet publish ./WatcherAPI/WatcherAPI.csproj -c Release -o /app/publish/WatcherApi /p:UseAppHost=false

# Stage 3: Create the final image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final
WORKDIR /app
COPY --from=publish /app/publish/WatcherApi .

# Expose the port
EXPOSE 80

# Entry point
ENTRYPOINT ["dotnet", "WatcherAPI.dll"]

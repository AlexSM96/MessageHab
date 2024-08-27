FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8000
EXPOSE 8001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["MessageHub.Clients/MessageHub.Clients.csproj", "MessageHub.Clients/"]
RUN dotnet restore "./MessageHub.Clients/MessageHub.Clients.csproj"
COPY . .
WORKDIR "/src/MessageHub.Clients"
RUN dotnet build "./MessageHub.Clients.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MessageHub.Clients.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MessageHub.Clients.dll"]
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["MessageHub.API/MessageHub.API.csproj", "MessageHub.API/"]
COPY ["MessageHub.AppService/MessageHub.AppService.csproj", "MessageHub.AppService/"]
COPY ["MessageHub.DAL/MessageHub.DAL.csproj", "MessageHub.DAL/"]
COPY ["MessageHub.Models/MessageHub.Models.csproj", "MessageHub.Models/"]
RUN dotnet restore "./MessageHub.API/MessageHub.API.csproj"
COPY . .
WORKDIR "/src/MessageHub.API"
RUN dotnet build "./MessageHub.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MessageHub.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MessageHub.API.dll"]
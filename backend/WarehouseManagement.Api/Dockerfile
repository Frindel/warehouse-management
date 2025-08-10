FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["WarehouseManagement.Api/WarehouseManagement.Api.csproj", "WarehouseManagement.Api/"]
RUN dotnet restore "WarehouseManagement.Api/WarehouseManagement.Api.csproj"
COPY . .
WORKDIR "/src/WarehouseManagement.Api"
RUN dotnet build "WarehouseManagement.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "WarehouseManagement.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WarehouseManagement.Api.dll"]

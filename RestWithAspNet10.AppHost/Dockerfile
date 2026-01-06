FROM mcr.microsoft.com/dotnet/runtime:10.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["RestWithAspNet10.AppHost/RestWithAspNet10.AppHost.csproj", "RestWithAspNet10.AppHost/"]
COPY ["RestWithAspNet10Scaffold/RestWithAspNet10Scaffold.csproj", "RestWithAspNet10Scaffold/"]
COPY ["RestWithAspNet10.ServiceDefaults/RestWithAspNet10.ServiceDefaults.csproj", "RestWithAspNet10.ServiceDefaults/"]
RUN dotnet restore "RestWithAspNet10.AppHost/RestWithAspNet10.AppHost.csproj"
COPY . .
WORKDIR "/src/RestWithAspNet10.AppHost"
RUN dotnet build "./RestWithAspNet10.AppHost.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./RestWithAspNet10.AppHost.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RestWithAspNet10.AppHost.dll"]

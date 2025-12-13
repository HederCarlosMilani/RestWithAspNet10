FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["RestWithAspNet10Scaffold/RestWithAspNet10Scaffold.csproj", "RestWithAspNet10Scaffold/"]
COPY ["RestWithAspNet10.ServiceDefaults/RestWithAspNet10.ServiceDefaults.csproj", "RestWithAspNet10.ServiceDefaults/"]
RUN dotnet restore "RestWithAspNet10Scaffold/RestWithAspNet10Scaffold.csproj"
COPY . .
WORKDIR "/src/RestWithAspNet10Scaffold"
RUN dotnet build "./RestWithAspNet10Scaffold.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./RestWithAspNet10Scaffold.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RestWithAspNet10Scaffold.dll"]

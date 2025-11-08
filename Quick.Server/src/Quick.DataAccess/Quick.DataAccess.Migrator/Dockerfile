FROM cr.yandex/crp86jhg5a3u4uoh3il4/mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM cr.yandex/crp86jhg5a3u4uoh3il4/mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Quick.DataAccess/Quick.DataAccess.Migrator/Quick.DataAccess.Migrator.csproj", "Quick.DataAccess/Quick.DataAccess.Migrator/"]
COPY ["Quick.DataAccess/Quick.DataAccess.Context/Quick.DataAccess.Context.csproj", "Quick.DataAccess/Quick.DataAccess.Context/"]
COPY ["Quick.Common/Quick.Common.DataAccess.Abstraction/Quick.Common.DataAccess.Abstraction.csproj", "Quick.Common/Quick.Common.DataAccess.Abstraction/"]
COPY ["Quick.DataAccess/Quick.DataAccess.Models/Quick.DataAccess.Models.csproj", "Quick.DataAccess/Quick.DataAccess.Models/"]
RUN dotnet restore "./Quick.DataAccess/Quick.DataAccess.Migrator/Quick.DataAccess.Migrator.csproj"
COPY . .
WORKDIR "/src/Quick.DataAccess/Quick.DataAccess.Migrator"
RUN dotnet build "./Quick.DataAccess.Migrator.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Quick.DataAccess.Migrator.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Quick.DataAccess.Migrator.dll"]
FROM cr.yandex/crp86jhg5a3u4uoh3il4/mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM cr.yandex/crp86jhg5a3u4uoh3il4/mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Quick.Gateway/Quick.API/Quick.API.csproj", "Quick.Gateway/Quick.API/"]
COPY ["Quick.BusinessLogic/Quick.BusinessLogic.Contracts/Quick.BusinessLogic.Contracts.csproj", "Quick.BusinessLogic/Quick.BusinessLogic.Contracts/"]
COPY ["Quick.DataAccess/Quick.DataAccess.Models/Quick.DataAccess.Models.csproj", "Quick.DataAccess/Quick.DataAccess.Models/"]
COPY ["Quick.Common/Quick.Common.DataAccess.Abstraction/Quick.Common.DataAccess.Abstraction.csproj", "Quick.Common/Quick.Common.DataAccess.Abstraction/"]
COPY ["Quick.BusinessLogic/Quick.BusinessLogic.Repositories.Abstractions/Quick.BusinessLogic.Repositories.Abstractions.csproj", "Quick.BusinessLogic/Quick.BusinessLogic.Repositories.Abstractions/"]
COPY ["Quick.BusinessLogic/Quick.BusinessLogic.Repositories.Implementations/Quick.BusinessLogic.Repositories.Implementations.csproj", "Quick.BusinessLogic/Quick.BusinessLogic.Repositories.Implementations/"]
COPY ["Quick.DataAccess/Quick.DataAccess.Context/Quick.DataAccess.Context.csproj", "Quick.DataAccess/Quick.DataAccess.Context/"]
COPY ["Quick.BusinessLogic/Quick.BusinessLogic.Services.Abstractions/Quick.BusinessLogic.Services.Abstractions.csproj", "Quick.BusinessLogic/Quick.BusinessLogic.Services.Abstractions/"]
COPY ["Quick.BusinessLogic/Quick.BusinessLogic.Services.Implementations/Quick.BusinessLogic.Services.Implementations.csproj", "Quick.BusinessLogic/Quick.BusinessLogic.Services.Implementations/"]
COPY ["Quick.Common.Helpers/Quick.Common.Helpers.csproj", "Quick.Common.Helpers/"]
COPY ["Quick.Common/Quick.Common.Configuration/Quick.Common.Configuration.csproj", "Quick.Common/Quick.Common.Configuration/"]
COPY ["Quick.Common.Context/Quick.Common.Context.csproj", "Quick.Common.Context/"]
RUN dotnet restore "./Quick.Gateway/Quick.API/Quick.API.csproj"
COPY . .
WORKDIR "/src/Quick.Gateway/Quick.API"
RUN dotnet build "./Quick.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Quick.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Quick.API.dll"]
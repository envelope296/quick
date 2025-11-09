# См. статью по ссылке https://aka.ms/customizecontainer, чтобы узнать как настроить контейнер отладки и как Visual Studio использует этот Dockerfile для создания образов для ускорения отладки.

# Этот этап используется при запуске из VS в быстром режиме (по умолчанию для конфигурации отладки)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# Этот этап используется для сборки проекта службы
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Quick.Gateway/Quick.API/Quick.API.csproj", "Quick.Gateway/Quick.API/"]
COPY ["Quick.BusinessLogic/Quick.BusinessLogic.Contracts/Quick.BusinessLogic.Contracts.csproj", "Quick.BusinessLogic/Quick.BusinessLogic.Contracts/"]
COPY ["Quick.BusinessLogic/Quick.BusinessLogic.Repositories.Abstractions/Quick.BusinessLogic.Repositories.Abstractions.csproj", "Quick.BusinessLogic/Quick.BusinessLogic.Repositories.Abstractions/"]
COPY ["Quick.Common/Quick.Common.DataAccess.Abstraction/Quick.Common.DataAccess.Abstraction.csproj", "Quick.Common/Quick.Common.DataAccess.Abstraction/"]
COPY ["Quick.DataAccess/Quick.DataAccess.Models/Quick.DataAccess.Models.csproj", "Quick.DataAccess/Quick.DataAccess.Models/"]
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

# Этот этап используется для публикации проекта службы, который будет скопирован на последний этап
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Quick.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Этот этап используется в рабочей среде или при запуске из VS в обычном режиме (по умолчанию, когда конфигурация отладки не используется)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Quick.API.dll"]
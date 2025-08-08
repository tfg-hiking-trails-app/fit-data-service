# Consulte https://aka.ms/customizecontainer para aprender a personalizar su contenedor de depuración y cómo Visual Studio usa este Dockerfile para compilar sus imágenes para una depuración más rápida.

# Esta fase se usa cuando se ejecuta desde VS en modo rápido (valor predeterminado para la configuración de depuración)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080

# Esta fase se usa para compilar el proyecto de servicio
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
# Fit Data Service
COPY ["fit-data-service/FitDataService.API/FitDataService.API.csproj", "fit-data-service/FitDataService.API/"]
COPY ["fit-data-service/FitDataService.Application/FitDataService.Application.csproj", "fit-data-service/FitDataService.Application/"]
COPY ["fit-data-service/FitDataService.Domain/FitDataService.Domain.csproj", "fit-data-service/FitDataService.Domain/"]
COPY ["fit-data-service/FitDataService.Infrastructure/FitDataService.Infrastructure.csproj", "fit-data-service/FitDataService.Infrastructure/"]
# Common Library
COPY ["hiking-trail-common/Common.API/Common.API.csproj", "hiking-trail-common/Common.API/"]
COPY ["hiking-trail-common/Common.Application/Common.Application.csproj", "hiking-trail-common/Common.Application/"]
COPY ["hiking-trail-common/Common.Domain/Common.Domain.csproj", "hiking-trail-common/Common.Domain/"]
COPY ["hiking-trail-common/Common.Infrastructure/Common.Infrastructure.csproj", "hiking-trail-common/Common.Infrastructure/"]
RUN dotnet restore "./fit-data-service/FitDataService.API/FitDataService.API.csproj"
COPY . .
WORKDIR "/src/fit-data-service/FitDataService.API"
RUN dotnet build "./FitDataService.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Esta fase se usa para publicar el proyecto de servicio que se copiará en la fase final.
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./FitDataService.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Esta fase se usa en producción o cuando se ejecuta desde VS en modo normal (valor predeterminado cuando no se usa la configuración de depuración)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FitDataService.API.dll"]
# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Bootstrapper/EshopModulith.Api/EshopModulith.Api.csproj", "Bootstrapper/EshopModulith.Api/"]
COPY ["Modules/Basket/EshopModulith.Basket/EshopModulith.Basket.csproj", "Modules/Basket/EshopModulith.Basket/"]
COPY ["Shared/EshopModulith.Shared/EshopModulith.Shared.csproj", "Shared/EshopModulith.Shared/"]
COPY ["Modules/Catalog/EshopModulith.Catalog/EshopModulith.Catalog.csproj", "Modules/Catalog/EshopModulith.Catalog/"]
COPY ["Modules/Ordering/EshopModulith.Ordering/EshopModulith.Ordering.csproj", "Modules/Ordering/EshopModulith.Ordering/"]
RUN dotnet restore "./Bootstrapper/EshopModulith.Api/EshopModulith.Api.csproj"
COPY . .
WORKDIR "/src/Bootstrapper/EshopModulith.Api"
RUN dotnet build "./EshopModulith.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./EshopModulith.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EshopModulith.Api.dll"]
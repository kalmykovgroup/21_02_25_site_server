﻿FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base

# Установим gosu
RUN apt-get update && \
    apt-get install -y gosu && \
    rm -rf /var/lib/apt/lists/*

# Copy entrypoint
COPY ./Api/entrypoint.sh /entrypoint.sh
RUN chmod +x /entrypoint.sh

# Удаляем BOM из entrypoint.sh
RUN sed -i '1s/^\xEF\xBB\xBF//' /entrypoint.sh

WORKDIR /app 

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Api/Api.csproj", "Api/"]
COPY ["Application/Application.csproj", "Application/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
RUN dotnet restore "Api/Api.csproj"
COPY . .
WORKDIR "/src/Api"
RUN dotnet build "Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
 
RUN chown -R $APP_UID:$APP_UID /app
 

# Укажи новый entrypoint
ENTRYPOINT ["/entrypoint.sh"]
CMD ["dotnet", "Api.dll"]
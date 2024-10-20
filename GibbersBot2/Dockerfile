FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["GibbersBot2/GibbersBot2.csproj", "GibbersBot2/"]
RUN dotnet restore "GibbersBot2/GibbersBot2.csproj"
COPY . .
WORKDIR "/src/GibbersBot2"
RUN dotnet build "GibbersBot2.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "GibbersBot2.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GibbersBot2.dll"]

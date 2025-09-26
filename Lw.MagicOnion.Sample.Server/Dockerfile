FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Lw.MagicOnion.Sample.Server/Lw.MagicOnion.Sample.Server.csproj", "Lw.MagicOnion.Sample.Server/"]
RUN dotnet restore "Lw.MagicOnion.Sample.Server/Lw.MagicOnion.Sample.Server.csproj"
COPY . .
WORKDIR "/src/Lw.MagicOnion.Sample.Server"
RUN dotnet build "Lw.MagicOnion.Sample.Server.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Lw.MagicOnion.Sample.Server.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Lw.MagicOnion.Sample.Server.dll"]

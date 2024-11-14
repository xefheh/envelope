FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS="http://+:8080"

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["services/AuthService/AuthService.API/AuthService.API.csproj", "service/src/AuthService.API/"]
COPY ["services/AuthService/AuthService.Persistence/AuthService.Persistence.csproj", "service/srcAuthService.Persistence/"]
COPY ["services/AuthService/AuthService.Domain/AuthService.Domain.csproj", "service/src/AuthService.Domain/"]
COPY ["services/AuthService/AuthService.Application/AuthService.Application.csproj", "service/src/AuthService.Application/"]
COPY ["infrastructure/Envelope.Common/Envelope.Common.csproj", "infrastructure/Envelope.Common/"]
COPY ["infrastructure/Envelope.Integration/Envelope.Integration.csproj", "infrastructure/Envelope.Integration/"]

RUN dotnet restore "service/src/AuthService.API/AuthService.API.csproj"

COPY services/AuthService/AuthService.API/. service/src/AuthService.API/
COPY services/AuthService/AuthService.Application/. service/src/AuthService.Application/
COPY services/AuthService/AuthService.Domain/. service/src/AuthService.Domain/
COPY services/AuthService/AuthService.Persistence/. service/src/AuthService.Persistence/
COPY infrastructure/Envelope.Common/. infrastructure/Envelope.Common/
COPY infrastructure/Envelope.Integration/. infrastructure/Envelope.Integration/

WORKDIR "/src/service/src/AuthService.API"
RUN dotnet build "AuthService.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "AuthService.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AuthService.API.dll"]

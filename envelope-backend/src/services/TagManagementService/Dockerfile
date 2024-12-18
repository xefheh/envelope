FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8086
ENV ASPNETCORE_URLS="http://+:8086"

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["services/TagManagementService/TagManagement.OuterService/TagManagement.OuterService.csproj", "service/src/TagManagement.OuterService/"]
COPY ["services/TagManagementService/TagManagement.Persistence/TagManagement.Persistence.csproj", "service/src/TagManagement.Persistence/"]
COPY ["services/TagManagementService/TagManagement.Domain/TagManagement.Domain.csproj", "service/src/TagManagement.Domain/"]
COPY ["services/TagManagementService/TagManagement.Application/TagManagement.Application.csproj", "service/src/TagManagement.Application/"]
COPY ["infrastructure/Envelope.Common/Envelope.Common.csproj", "infrastructure/Envelope.Common/"]
COPY ["infrastructure/Envelope.Integration/Envelope.Integration.csproj", "infrastructure/Envelope.Integration/"]

RUN dotnet restore "service/src/TagManagement.OuterService/TagManagement.OuterService.csproj"

COPY services/TagManagementService/TagManagement.OuterService/. service/src/TagManagement.OuterService/
COPY services/TagManagementService/TagManagement.Application/. service/src/TagManagement.Application/
COPY services/TagManagementService/TagManagement.Domain/. service/src/TagManagement.Domain/
COPY services/TagManagementService/TagManagement.Persistence/. service/src/TagManagement.Persistence/
COPY infrastructure/Envelope.Common/. infrastructure/Envelope.Common/
COPY infrastructure/Envelope.Integration/. infrastructure/Envelope.Integration/

WORKDIR "/src/service/src/TagManagement.OuterService"
RUN dotnet build "TagManagement.OuterService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "TagManagement.OuterService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TagManagement.OuterService.dll"]

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8081
ENV ASPNETCORE_URLS="http://+:8081"

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["services/TaskService/TaskService.API/TaskService.API.csproj", "service/src/TaskService.API/"]
COPY ["services/TaskService/TaskService.Persistence/TaskService.Persistence.csproj", "service/src/TaskService.Persistence/"]
COPY ["services/TaskService/TaskService.Domain/TaskService.Domain.csproj", "service/src/TaskService.Domain/"]
COPY ["services/TaskService/TaskService.Application/TaskService.Application.csproj", "service/src/TaskService.Application/"]
COPY ["infrastructure/Envelope.Common/Envelope.Common.csproj", "infrastructure/Envelope.Common/"]
COPY ["infrastructure/Envelope.Integration/Envelope.Integration.csproj", "infrastructure/Envelope.Integration/"]

RUN dotnet restore "service/src/TaskService.API/TaskService.API.csproj"

COPY services/TaskService/TaskService.API/. service/src/TaskService.API/
COPY services/TaskService/TaskService.Application/. service/src/TaskService.Application/
COPY services/TaskService/TaskService.Domain/. service/src/TaskService.Domain/
COPY services/TaskService/TaskService.Persistence/. service/src/TaskService.Persistence/
COPY infrastructure/Envelope.Common/. infrastructure/Envelope.Common/
COPY infrastructure/Envelope.Integration/. infrastructure/Envelope.Integration/

WORKDIR "/src/service/src/TaskService.API"
RUN dotnet build "TaskService.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "TaskService.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TaskService.API.dll"]

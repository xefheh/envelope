FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8082
ENV ASPNETCORE_URLS="http://+:8082"

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["services/CoursesService/CoursesService.API/CoursesService.API.csproj", "service/src/CoursesService.API/"]
COPY ["services/CoursesService/CoursesService.Persistence/CoursesService.Persistence.csproj", "service/src/CoursesService.Persistence/"]
COPY ["services/CoursesService/CoursesService.Domain/CoursesService.Domain.csproj", "service/src/CoursesService.Domain/"]
COPY ["services/CoursesService/CoursesService.Application/CoursesService.Application.csproj", "service/src/CoursesService.Application/"]
COPY ["infrastructure/Envelope.Common/Envelope.Common.csproj", "infrastructure/Envelope.Common/"]
COPY ["infrastructure/Envelope.Integration/Envelope.Integration.csproj", "infrastructure/Envelope.Integration/"]

RUN dotnet restore "service/src/CoursesService.API/CoursesService.API.csproj"

COPY services/CoursesService/CoursesService.API/. service/src/CoursesService.API/
COPY services/CoursesService/CoursesService.Application/. service/src/CoursesService.Application/
COPY services/CoursesService/CoursesService.Domain/. service/src/CoursesService.Domain/
COPY services/CoursesService/CoursesService.Persistence/. service/src/CoursesService.Persistence/
COPY infrastructure/Envelope.Common/. infrastructure/Envelope.Common/
COPY infrastructure/Envelope.Integration/. infrastructure/Envelope.Integration/

WORKDIR "/src/service/src/CoursesService.API"
RUN dotnet build "CoursesService.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "CoursesService.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CoursesService.API.dll"]

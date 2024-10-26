FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8082
ENV ASPNETCORE_URLS="http://+:8082"

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["CoursesService.API/CoursesService.API.csproj", "CoursesService.API/"]
COPY ["CoursesService.Persistence/CoursesService.Persistence.csproj", "CoursesService.Persistence/"]
COPY ["CoursesService.Domain/CoursesService.Domain.csproj", "CoursesService.Domain/"]
COPY ["CoursesService.Application/CoursesService.Application.csproj", "CoursesService.Application/"]
RUN dotnet restore "CoursesService.API/CoursesService.API.csproj"

COPY CoursesService.API/. CoursesService.API/
COPY CoursesService.Application/. CoursesService.Application/
COPY CoursesService.Domain/. CoursesService.Domain/
COPY CoursesService.Persistence/. CoursesService.Persistence/

WORKDIR "/src/CoursesService.API"
RUN dotnet build "CoursesService.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "CoursesService.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CoursesService.API.dll"]

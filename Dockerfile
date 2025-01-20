FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine AS base
USER $APP_UID
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["student-final/student-final.csproj", "student-final/"]
RUN dotnet restore "student-final/student-final.csproj" --verbosity detailed
COPY . .
WORKDIR "/src/student-final"
RUN dotnet build "student-final.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "student-final.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "student-final.dll"]
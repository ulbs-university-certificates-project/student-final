FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["student-crud/student-crud.csproj", "student-crud/"]
RUN dotnet restore "student-crud/student-crud.csproj"
COPY . .
WORKDIR "/src/student-crud"
RUN dotnet build "student-crud.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "student-crud.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "student-crud.dll"]

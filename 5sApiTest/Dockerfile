FROM mcr.microsoft.com/dotnet/aspnet:6.0-nanoserver-1809 AS base
WORKDIR /app
EXPOSE 1433

ENV ASPNETCORE_URLS=http://+:1433

FROM mcr.microsoft.com/dotnet/sdk:6.0-nanoserver-1809 AS build
ARG configuration=Release
WORKDIR /src
COPY ["backend/5s_backend/5sApiTest/5sApiTest.csproj", "backend/5s_backend/5sApiTest/"]
RUN dotnet restore "backend\5s_backend\5sApiTest\5sApiTest.csproj"
COPY . .
WORKDIR "/src/backend/5s_backend/5sApiTest"
RUN dotnet build "5sApiTest.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "5sApiTest.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "5sApiTest.dll"]

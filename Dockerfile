FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 7124

ENV ASPNETCORE_URLS=http://+:7124
ENV ASPNETCORE_ENVIRONMENT=Development

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG configuration=Release
WORKDIR /src

COPY ["5s/5s.csproj", "./"]

RUN dotnet restore "./5s.csproj"
COPY . .
WORKDIR "/src/."

# Specify a different output directory for the build artifacts
RUN dotnet build "5s.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release

RUN dotnet publish "5s.csproj" -c $configuration -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "5s.dll"]

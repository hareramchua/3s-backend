FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 7124

ENV ASPNETCORE_URLS=http://+:7124
ENV ASPNETCORE_ENVIRONMENT=Development

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG configuration=Release
WORKDIR /src

# Copy the project file
COPY ["5s/5s.csproj", "./"]

# Restore dependencies
RUN dotnet restore "./5s.csproj"

# Copy the entire project (this includes the .cs files)
COPY . .

# Build the project
RUN dotnet build "5s.csproj" -c $configuration -o /app

FROM build AS publish
ARG configuration=Release

# Publish the project
RUN dotnet publish "5s.csproj" -c $configuration -o /app --no-restore

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "5s.dll"]

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj files and restore
COPY ["MindFit.API/MindFit.API.csproj", "MindFit.API/"]
COPY ["MindFit.Application/MindFit.Application.csproj", "MindFit.Application/"]
COPY ["MindFit.Domain/MindFit.Domain.csproj", "MindFit.Domain/"]
COPY ["MindFit.Infrastructure/MindFit.Infrastructure.csproj", "MindFit.Infrastructure/"]
RUN dotnet restore "MindFit.API/MindFit.API.csproj"

# Copy everything else and build
COPY . .
WORKDIR "/src/MindFit.API"
RUN dotnet build "MindFit.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MindFit.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MindFit.API.dll"]

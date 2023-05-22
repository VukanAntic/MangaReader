#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/MangaCatalog/MangaCatalog.API/MangaCatalog.API.csproj", "Services/MangaCatalog/MangaCatalog.API/"]
COPY ["Services/MangaCatalog/MangaCatalog.Common/MangaCatalog.Common.csproj", "Services/MangaCatalog/MangaCatalog.Common/"]
RUN dotnet restore "Services/MangaCatalog/MangaCatalog.API/MangaCatalog.API.csproj"
COPY . .
WORKDIR "/src/Services/MangaCatalog/MangaCatalog.API"
RUN dotnet build "MangaCatalog.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MangaCatalog.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MangaCatalog.API.dll"]
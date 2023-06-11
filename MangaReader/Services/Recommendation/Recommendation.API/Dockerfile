#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Recommendation/Recommendation.API/Recommendation.API.csproj", "Services/Recommendation/Recommendation.API/"]
COPY ["Services/MangaCatalog/MangaCatalog.Common/MangaCatalog.Common.csproj", "Services/MangaCatalog/MangaCatalog.Common/"]
RUN dotnet restore "Services/Recommendation/Recommendation.API/Recommendation.API.csproj"
COPY . .
WORKDIR "/src/Services/Recommendation/Recommendation.API"
RUN dotnet build "Recommendation.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Recommendation.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Recommendation.API.dll"]
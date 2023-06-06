#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/MangaCatalog/MangaCatalog.GRPC/MangaCatalog.GRPC.csproj", "Services/MangaCatalog/MangaCatalog.GRPC/"]
COPY ["Services/MangaCatalog/MangaCatalog.Common/MangaCatalog.Common.csproj", "Services/MangaCatalog/MangaCatalog.Common/"]
RUN dotnet restore "Services/MangaCatalog/MangaCatalog.GRPC/MangaCatalog.GRPC.csproj"
COPY . .
WORKDIR "/src/Services/MangaCatalog/MangaCatalog.GRPC"
RUN dotnet build "MangaCatalog.GRPC.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MangaCatalog.GRPC.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MangaCatalog.GRPC.dll"]
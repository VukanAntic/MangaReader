#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/UserInfo/UserInfo.API/UserInfo.API.csproj", "Services/UserInfo/UserInfo.API/"]
COPY ["Common/EventBus.Messages/EventBus.Messages.csproj", "Common/EventBus.Messages/"]
COPY ["Services/UserInfo/UserInfo.Common/UserInfo.Common.csproj", "Services/UserInfo/UserInfo.Common/"]
RUN dotnet restore "Services/UserInfo/UserInfo.API/UserInfo.API.csproj"
COPY . .
WORKDIR "/src/Services/UserInfo/UserInfo.API"
RUN dotnet build "UserInfo.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UserInfo.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UserInfo.API.dll"]
#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/UserInfo/UserInfo.GRPC/UserInfo.GRPC.csproj", "Services/UserInfo/UserInfo.GRPC/"]
COPY ["Services/UserInfo/UserInfo.Common/UserInfo.Common.csproj", "Services/UserInfo/UserInfo.Common/"]
COPY ["Common/EventBus.Messages/EventBus.Messages.csproj", "Common/EventBus.Messages/"]
RUN dotnet restore "Services/UserInfo/UserInfo.GRPC/UserInfo.GRPC.csproj"
COPY . .
WORKDIR "/src/Services/UserInfo/UserInfo.GRPC"
RUN dotnet build "UserInfo.GRPC.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UserInfo.GRPC.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UserInfo.GRPC.dll"]

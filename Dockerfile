#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["API_Gateway.csproj", "API_Gateway/"]
RUN dotnet restore "API_Gateway/API_Gateway.csproj"
COPY . .
WORKDIR "/src/API_Gateway"
RUN dotnet build "API_Gateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "API_Gateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "API_Gateway.dll"]
#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["API_Gateway_Service/API_Gateway_Service.csproj", "API_Gateway_Service/"]
RUN dotnet restore "API_Gateway_Service/API_Gateway_Service.csproj"
COPY . .
WORKDIR "/src/API_Gateway_Service"
RUN dotnet build "API_Gateway_Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "API_Gateway_Service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "API_Gateway_Service.dll"]
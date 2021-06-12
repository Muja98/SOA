#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Analytics_Service/Analytics_Service.csproj", "Analytics_Service/"]
RUN dotnet restore "Analytics_Service/Analytics_Service.csproj"
COPY . .
WORKDIR "/src/Analytics_Service"
RUN dotnet build "Analytics_Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Analytics_Service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Analytics_Service.dll"]
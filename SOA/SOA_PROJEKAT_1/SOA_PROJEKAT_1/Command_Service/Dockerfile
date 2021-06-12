#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Command_Service/Command_Service.csproj", "Command_Service/"]
RUN dotnet restore "Command_Service/Command_Service.csproj"
COPY . .
WORKDIR "/src/Command_Service"
RUN dotnet build "Command_Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Command_Service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Command_Service.dll"]
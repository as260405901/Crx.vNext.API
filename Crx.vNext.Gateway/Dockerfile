#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:latest AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:latest AS build
WORKDIR /src
COPY ["Crx.vNext.Gateway/Crx.vNext.Gateway.csproj", "Crx.vNext.Gateway/"]
RUN dotnet restore "Crx.vNext.Gateway/Crx.vNext.Gateway.csproj"
COPY . .
WORKDIR "/src/Crx.vNext.Gateway"
RUN dotnet build "Crx.vNext.Gateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Crx.vNext.Gateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Crx.vNext.Gateway.dll"]
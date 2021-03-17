#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:latest AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:latest AS build
WORKDIR /src
COPY ["Crx.vNext.Authentication/Crx.vNext.Authentication.csproj", "Crx.vNext.Authentication/"]
RUN dotnet restore "Crx.vNext.Authentication/Crx.vNext.Authentication.csproj"
COPY . .
WORKDIR "/src/Crx.vNext.Authentication"
RUN dotnet build "Crx.vNext.Authentication.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Crx.vNext.Authentication.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Crx.vNext.Authentication.dll"]
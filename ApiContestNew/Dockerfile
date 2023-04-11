#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ApiContestNew/ApiContestNew.csproj", "ApiContestNew/"]
COPY ["ApiContestNew.Application/ApiContestNew.Application.csproj", "ApiContestNew.Application/"]
COPY ["ApiContestNew.Core/ApiContestNew.Core.csproj", "ApiContestNew.Core/"]
COPY ["ApiContestNew.Infrastructure/ApiContestNew.Infrastructure.csproj", "ApiContestNew.Infrastructure/"]
RUN dotnet restore "ApiContestNew/ApiContestNew.csproj"
COPY . .
WORKDIR "/src/ApiContestNew"
RUN dotnet build "ApiContestNew.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ApiContestNew.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENV ASPNETCORE_URLS="http://0.0.0.0:8080"

ENTRYPOINT ["dotnet", "ApiContestNew.dll"]
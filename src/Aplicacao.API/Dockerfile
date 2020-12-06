#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Aplicacao.API/Aplicacao.API.csproj", "Aplicacao.API/"]
COPY ["Aplicacao.Domain/Aplicacao.Domain.csproj", "Aplicacao.Domain/"]
COPY ["Aplicacao.Domain.Shared/Aplicacao.Domain.Shared.csproj", "Aplicacao.Domain.Shared/"]
COPY ["Aplicacao.Application/Aplicacao.Application.csproj", "Aplicacao.Application/"]
COPY ["Aplicacao.Infra.CrossCutting/Aplicacao.Infra.CrossCutting.csproj", "Aplicacao.Infra.CrossCutting/"]
COPY ["Aplicacao.Infra.MessageBroker/Aplicacao.Infra.MessageBroker.csproj", "Aplicacao.Infra.MessageBroker/"]
COPY ["Aplicacao.Infra.DataAccess/Aplicacao.Infra.DataAccess.csproj", "Aplicacao.Infra.DataAccess/"]
RUN dotnet restore "Aplicacao.API/Aplicacao.API.csproj"
COPY . .
WORKDIR "/src/Aplicacao.API"
RUN dotnet build "Aplicacao.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Aplicacao.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Aplicacao.API.dll"]
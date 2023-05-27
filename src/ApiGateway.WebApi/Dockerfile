FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /SpacexdataApiIntegrationApiGateway
COPY ["src/ApiGateway.WebApi/ApiGateway.WebApi.csproj", "src/ApiGateway.WebApi/"]
COPY ["src/ApiGateway.Application/ApiGateway.Application.csproj", "src/ApiGateway.Application/ApiGateway.Application/"]
COPY ["src/ApiGateway.Infrastructure/ApiGateway.Infrastructure.csproj", "src/ApiGateway.Infrastructure/ApiGateway.Infrastructure/"]
RUN dotnet restore "src/ApiGateway.WebApi/ApiGateway.WebApi.csproj"
COPY . .
WORKDIR "/SpacexdataApiIntegrationApiGateway/src/ApiGateway.WebApi/"
RUN dotnet build "ApiGateway.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ApiGateway.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ApiGateway.WebApi.dll"]
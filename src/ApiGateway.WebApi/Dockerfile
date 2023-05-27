FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/ApiGateway.WebApi/ApiGateway.WebApi.csproj", "/ApiGateway.WebApi/"]
COPY ["src/ApiGateway.WebApi/ApiGateway.Application.csproj", "/ApiGateway.WebApi/ApiGateway.Application/"]
COPY ["src/ApiGateway.WebApi/ApiGateway.Infrastructure.csproj", "/ApiGateway.WebApi/ApiGateway.Infrastructure/"]
RUN dotnet restore "src/ApiGateway.WebApi/ApiGateway.WebApi.csproj"
COPY . .
WORKDIR "/src/ApiGateway.WebApi"
RUN dotnet build "ApiGateway.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ApiGateway.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ApiGateway.WebApi.dll"]